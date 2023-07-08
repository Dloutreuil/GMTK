using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int killCount;
    public int elapsedTime;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    private void Update()
    {
        elapsedTime = (int)Time.deltaTime;
        UiManager.Instance.UpdateTime(elapsedTime);
    }

    public void ChangeScore()
    {
        killCount++;
        UiManager.Instance.UpdateScore(killCount);
    }


    public void GameLost()
    {
        Time.timeScale = 0;
        UiManager.Instance.ShowLoseScreen();
    }
}
