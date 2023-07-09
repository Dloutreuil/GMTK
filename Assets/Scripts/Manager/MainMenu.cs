using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Player;
    public GameObject credit;

    private void Start()
    {
        QuitCredit();
    }
    public void LoadGame()
    {
        Leaderboard.Instance.holder.SetActive(false);
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredit()
    {
        credit.SetActive(true);
        Player.SetActive(false);
    }
    public void QuitCredit()
    {
        if(credit != null)
        {
            credit.SetActive(false);
            Player.SetActive(true);


        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        Leaderboard.Instance.holder.active = true;
        SceneManager.LoadScene("MM");

    }

}
