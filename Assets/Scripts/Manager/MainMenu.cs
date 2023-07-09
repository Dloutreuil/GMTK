using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credit;

    private void Start()
    {
        QuitCredit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredit()
    {
        credit.SetActive(true);
    }
    public void QuitCredit()
    {
        if(credit != null)
        {
            credit.SetActive(false);

        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MM");

    }

}
