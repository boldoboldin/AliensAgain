using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menu;

    public void ShowCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void HideCredits()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
