using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public Player player;
    public ParticleSystem levelUpPrt;

    [Header("Audio")]
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip levelUpSound;

    [Header("UI")]
    public Slider expBar;
    public TextMeshProUGUI expNumbers;
    public TextMeshProUGUI levelUI;

    [Header("general")]
    public int maxExpAmount;
    public int increaseExp;
    public int healthIncrease;
    public int manaIncrease;

    //[HideInInspector]
    public int currentLevel = 1;

    // Use this for initialization
    void Start()
    {
        expBar.maxValue = maxExpAmount;
        expBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //UI
        expNumbers.text = expBar.value + " / " + maxExpAmount;
        levelUI.text = "Level " + currentLevel;

        if (expBar.value >= maxExpAmount)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        Instantiate(levelUpPrt, player.transform);
        source.clip = levelUpSound;
        source.Play();

        expBar.value = 0;
        maxExpAmount += increaseExp;
        expBar.maxValue = maxExpAmount;

        player.health += healthIncrease;
        player.hpBar.maxValue = player.health;
        player.hpBar.value = player.hpBar.maxValue;

        player.mana += manaIncrease;
        player.manaBar.maxValue = player.mana;
        player.currentMana = player.manaBar.maxValue;

        currentLevel += 1;
    }

    public void AddExperience(int experience)
    {
        expBar.value += experience;
    }
}