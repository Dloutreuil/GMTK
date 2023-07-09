using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance { get { return instance; } }

    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI currentTime;

    public int currentHealth; //change to slider

    public SpriteRenderer currentSpell;
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

    private void Start()
    {
        NoSpell();
    }

    public void UpdateScore(int newScore)
    {
        currentScore.text = newScore.ToString();
    }

    public void UpdateTime(int newTime)
    {
        currentTime.text = newTime.ToString();
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;
    }

    public void UpdateSpell(Sprite newSpell)
    {
        currentSpell.sprite = newSpell;
    }

    public void NoSpell()
    {
        currentSpell.sprite = noSpell;
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}
