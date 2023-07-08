using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance { get { return instance; } }

    public int currentScore;
    public int currentTime;

    public int currentHealth; //change to slider

    public Sprite currentSpell;
    public Sprite noSpell;

    public GameObject loseScreen;
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

    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
    }

    public void UpdateTime(int newTime)
    {
        currentScore = newTime;
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;
    }

    public void UpdateSpell(Sprite newSpell)
    {
        currentSpell = newSpell;
    }

    public void NoSpell()
    {
        currentSpell = noSpell;
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
