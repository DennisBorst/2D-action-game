using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [HideInInspector]
    public Transform player;
    private LevelSystem levelSystem;
    private bool isDead = false;

    [Header("UI")]
    public Slider hpBar;

    [Header("Particles")]
    public GameObject deathParticles;

    [Header("main stats")]
    public float health;
    public float speed;
    public int experience;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public int damage;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelSystem = FindObjectOfType<LevelSystem>();
        hpBar.maxValue = health;
        hpBar.value = health;
    }

    public void DamageTaken(int damageAmount)
    {
        hpBar.value -= damageAmount;
        if (hpBar.value <= 0 && !isDead)
        {
            isDead = true;
            levelSystem.AddExperience(experience);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
