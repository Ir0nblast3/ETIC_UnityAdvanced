using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlayPanel;
    public void PlayGame()
    { 
        SceneManager.LoadScene("DemoLevel");
    }

    public void HowToPlay()
    {
        _howToPlayPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        _howToPlayPanel.SetActive(false);
    }



}
