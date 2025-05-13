#region Namespaces/Directives

using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

#endregion

public class BaseGun : MonoBehaviour
{
    #region Declarations

    [Header("Gun Settings")]
    [SerializeField] private float _fireRate;
    private float _timePassed; 
    private int _maxBullets = 20;
    private int _bullets;
    private float _gunDamage = 20;

    [SerializeField] private Transform _firePoint;

    public int Bullets { get => _bullets; set => _bullets = value; }
    public int MaxBullets { get => _maxBullets; }
    public float GunDamage { get => _gunDamage; set => _gunDamage = value; }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _timePassed = _fireRate;
    }

    private void Start()
    {
        _bullets = _maxBullets;
    }

    private void Update()
    {
        if (_timePassed <= _fireRate)
        {
            _timePassed += Time.deltaTime;
        }
       
        UIManager.instance.BulletsText(_bullets, _maxBullets);
    }

    #endregion

    public void Fire()
    {
        if (_timePassed >= _fireRate && _bullets > 0)
        {
            _bullets--;
            _timePassed = 0;

            BulletBehaviour bullet = PoolManager.Instance.GetBullet();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;

        }
    }
}
