#region Namespaces/Directives

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion


public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _hideCursor;
    private bool _isPaused;

    private UIManager _uiManager;

    public static GameManager instance;

    private float _playerCoinNumber;

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public bool HideCursor { get => _hideCursor; set => _hideCursor = value; }
    public float PlayerCoinNumber { get => _playerCoinNumber; set => _playerCoinNumber = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        _uiManager = UIManager.instance;
        
        _hideCursor = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (_hideCursor == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnEnable()
    {
        PlayerCharacter.OnPlayerDeath += LostGame;
    }

    private void OnDesable()
    {
        PlayerCharacter.OnPlayerDeath -= LostGame;
    }
    public void PauseGame()
    {
        _uiManager.TogglePause(true);
        Time.timeScale = 0;
        _isPaused = true;
        _hideCursor = false;    
    }

    public void Resume()
    {
        _uiManager.TogglePause(false);
        Time.timeScale = 1.0f;
        _isPaused = false;
        _hideCursor = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddCoin(int value)
    {
        _playerCoinNumber += value;
        _uiManager.UpdateCoins(_playerCoinNumber);
    }

    public void RemoveCoin(int value)
    {
        _playerCoinNumber -= value;
        _uiManager.UpdateCoins(_playerCoinNumber);
    }

    public void OpenStore()
    {
        _isPaused = true;
        _hideCursor = false;
        _uiManager.Store(true);
    }

    public void LostGame()
    {
        Time.timeScale = 0;
        _isPaused = true;
        _hideCursor = false;
        _uiManager.LostScreen(true);
    }
}

