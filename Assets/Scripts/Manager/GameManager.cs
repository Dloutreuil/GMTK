using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int killCount;
    public int elapsedTime = 0;

    private float timeAlive;
    private int score;
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
    private void Start()
    {
        elapsedTime = 0;
        // Initialize variables
        killCount = 0;
        timeAlive = 0f;
        score = 0;
    }


    private void Update()
    {
        StartCoroutine(UpdateTime());
    }



    private IEnumerator UpdateTime()
    {
        while (true)
        {
            elapsedTime = Mathf.FloorToInt(Time.timeSinceLevelLoad) ;
            UiManager.Instance.UpdateTime(elapsedTime);

            yield return new WaitForSeconds(1f);
        }
    }

    public void ChangeScore()
    {
        killCount++;
        UiManager.Instance.UpdateScore(killCount);

        // Calculate score based on enemiesKilled and timeAlive
        score = killCount * 10 + (int)timeAlive;
    }


    public void GameLost()
    {
        ChangeScore();
        Debug.Log(score);
        Leaderboard.Instance.StartScoreCoroutine(score);
        Time.timeScale = 0;
        UiManager.Instance.ShowLoseScreen();
    }
}
