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

    public SpriteRenderer currentSpell;
    [HideInInspector] public Sprite noSpell;

    public SpriteRenderer currentHealth;

    public Sprite health1;
    public Sprite health2;
    public Sprite health3;
    public Sprite health0;


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
        switch (health)
        {
            case 0:
                currentHealth.sprite = health0;
                break;
            case 1:
                currentHealth.sprite = health1;
                break;
            case 2:
                currentHealth.sprite = health2;

                break;
            case 3:
                currentHealth.sprite = health3;
                break;
        }
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
        currentScore.text = "";
        currentTime.text = "";

        loseScreen.SetActive(true);

        currentScore.text = "";
        currentTime.text = "0";
        currentTime.text = null;

    }
}
