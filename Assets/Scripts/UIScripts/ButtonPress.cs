using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    [SerializeField]
    GameObject startGame, credits;
    public void OpenCredits()
    {
        startGame.SetActive(false);
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        startGame.SetActive(true);
        credits.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
