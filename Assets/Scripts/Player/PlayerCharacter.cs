#region Namespaces/Directives

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class PlayerCharacter : MonoBehaviour
{
    #region Declarations

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    [Header("My References")]
    private Rigidbody _rigidBody;
    private BaseGun _equippedGun;
    private UIManager _uiManager;
    private GameManager _gameManager;

    private float hp = 100;
    private int _jumpsLeft;
    private int _maxJumps = 1;

    public static event Action OnPlayerDeath;

    public BaseGun EquippedGun { get => _equippedGun; }
    public int MaxJumps { get => _maxJumps; set => _maxJumps = value; }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _equippedGun = GetComponentInChildren<BaseGun>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _uiManager = UIManager.instance;    
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            _jumpsLeft = _maxJumps;
        }

        if (GameManager.instance.IsPaused == false) 
        {
            PlayerInput();
            MoveInDirection(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }

    #endregion

    private void PlayerInput()
    {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                FireGun();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }
  
    }

    private void FireGun()
    {
        _equippedGun?.Fire();
    }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode.Impulse);
        }
        else
        {
            if (_jumpsLeft > 0)
            {
                _rigidBody.velocity = Vector3.zero;
                _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode.Impulse);
                _jumpsLeft = _jumpsLeft - 1;
            }
        }
    }

    private void MoveInDirection(Vector2 direction)
    {
        Vector3 finalVelocity = (direction.x * transform.right + direction.y * transform.forward).normalized * _movementSpeed;

        finalVelocity.y = _rigidBody.velocity.y;
        _rigidBody.velocity = finalVelocity;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, 100);

        _uiManager.UpdateHp(hp, 100);

        if (hp <= 0)
        {
            OnPlayerDeath?.Invoke();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       ICollectable collectable = other.gameObject.GetComponent<ICollectable>();

        if (collectable != null) 
        {
            collectable.Collect(this);
        }
    }

    public void GainHealth(int value)
    {
        hp += value;
        hp = Mathf.Clamp(hp, 0, 100);
;        _uiManager.UpdateHp(hp, 100);
    }

}
