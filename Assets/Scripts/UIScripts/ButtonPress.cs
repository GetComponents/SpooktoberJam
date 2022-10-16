using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    public void OpenCredits()
    {

    }

    public void CloseCredits()
    {

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
