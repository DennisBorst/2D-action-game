using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private GameManager gameManager;

    private Vector2 moveAmount;

    public float health;
    public float mana;
    public float manaOverTime = 0.5f;
    [HideInInspector]
    public float currentMana;

    [Header("UI")]
    public Slider hpBar;
    public Slider manaBar;
    [Space]
    public TextMeshProUGUI healthNumbers;
    public TextMeshProUGUI manaNumbers;

    [Header("Particles")]
    public GameObject deathParticles;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        hpBar.maxValue = health;
        hpBar.value = health;

        manaBar.maxValue = mana;
        manaBar.value = mana;
        currentMana = mana;
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        ManaManagment();
        playerUI();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void DamageTaken(int damageAmount)
    {
        hpBar.value -= damageAmount;

        if (hpBar.value <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            gameManager.ReloadScene();
        }
    }

    public void ManaCost(int manaCost)
    {
        currentMana -= manaCost;
        if(currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    void ManaManagment()
    {
        if (currentMana <= mana)
        {
            currentMana += manaOverTime;
        }
    }

    void playerUI()
    {
        manaBar.value = Mathf.RoundToInt(currentMana);
        healthNumbers.text = hpBar.value + " / " + health;
        manaNumbers.text = manaBar.value + " / " + mana;
    }
}
