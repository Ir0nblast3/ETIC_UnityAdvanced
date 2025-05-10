using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _lostScreen;
    [SerializeField] private GameObject _store;
    [SerializeField] private Text _playerCoinText;
    [SerializeField] private Text _playerBullets;
    [SerializeField] private Text _playerMaxBullets;

    public static UIManager instance;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void UpdateHp(float hp, float maxHp)
    {
        _healthBar.fillAmount = hp / maxHp;
    }

    public void TogglePause(bool active)
    {
        _pauseMenu.SetActive(active);
    }

    public void UpdateCoins(float points)
    {
        _playerCoinText.text = points.ToString();
    }

    public void BulletsText(int bullets, int maxBullets)
    {
        _playerBullets.text  = bullets.ToString();
        _playerMaxBullets.text = maxBullets.ToString();
    }

    public void Store(bool open)
    {
        _store.SetActive(open);
    }

    public void LostScreen(bool active)
    {
        _lostScreen.SetActive(active);
    }
}
