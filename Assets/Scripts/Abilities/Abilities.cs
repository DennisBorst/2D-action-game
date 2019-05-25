using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {

    public Transform player;
    public LevelSystem levelSystem;
    public Player playerScript;

    [Space]
    [SerializeField]
    private float[] lerpSpeed;
    public float[] coolDown;
    public int[] manaCost;
    [Space]

    //Ability 1
    [Header("Multiple Shots")]
    public GameObject multipleBallPrt;
    public Image multipleBallImage;
    public Transform[] shotPoint;
    private float shotTimeMultipleBalls;

    //Ability 2
    [Header("Shockwave")]
    public GameObject shockWavePrt;
    public Image shockWaveImage;
    private float shotTimeShockWave;

    //Ability 3
    [Header("attack speed + selfspeed")]
    public Image buffImage;
    public float buffTime;
    public int increaseSpeed;
    public float decreaseCooldown = 0.5f;
    private float shotTimeBuff;
    private bool usedBuff = false;

    //Ability 4
    [Header("Meteorite")]
    public GameObject meteoritePrt;
    public Image meteoriteImage;
    private float shotTimeMeteorite;


    private void Start()
    {
        multipleBallImage.fillAmount = 0;
        shockWaveImage.fillAmount = 0;
        buffImage.fillAmount = 0;
        meteoriteImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update () {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;


        //multiple shots
        if (levelSystem.currentLevel >= 2)
        {
            MultipleShots();
        }

        //shockwave
        if (levelSystem.currentLevel >= 3)
        {
            ShockWave();
        }

        //Buff
        if (levelSystem.currentLevel >= 4)
        {
            Buff();
        }

        //Meteorite
        if (levelSystem.currentLevel >= 5)
        {
            Meteorite();
        }
        
    }

    void MultipleShots()
    {
        multipleBallImage.fillAmount = Mathf.Lerp(multipleBallImage.fillAmount, coolDown[0], Time.deltaTime * lerpSpeed[0]);

        if (Input.GetMouseButton(1) && playerScript.currentMana >= manaCost[0])
        {
            if (Time.time >= shotTimeMultipleBalls)
            {
                playerScript.ManaCost(manaCost[0]);
                multipleBallImage.fillAmount = 0;
                Instantiate(multipleBallPrt, shotPoint[0].position, shotPoint[0].rotation);
                Instantiate(multipleBallPrt, shotPoint[1].position, transform.rotation);
                Instantiate(multipleBallPrt, shotPoint[2].position, shotPoint[2].rotation);
                shotTimeMultipleBalls = Time.time + coolDown[0];
            }
        }
    }

    void ShockWave()
    {
        shockWaveImage.fillAmount = Mathf.Lerp(shockWaveImage.fillAmount, coolDown[1], Time.deltaTime * lerpSpeed[1]);

        if (Input.GetKeyDown(KeyCode.E) && playerScript.currentMana >= manaCost[1])
        {
            if (Time.time >= shotTimeShockWave)
            {
                playerScript.ManaCost(manaCost[1]);
                shockWaveImage.fillAmount = 0;
                Instantiate(shockWavePrt, player.transform);
                shotTimeShockWave = Time.time + coolDown[1];
            }
        }
    }


    void Buff()
    {
        if (!usedBuff)
        {
            buffImage.fillAmount = Mathf.Lerp(buffImage.fillAmount, coolDown[2], Time.deltaTime * lerpSpeed[2]);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerScript.currentMana >= manaCost[2])
        {
            if (Time.time >= shotTimeBuff && !usedBuff)
            {
                playerScript.ManaCost(manaCost[2]);
                usedBuff = true;
                buffImage.fillAmount = 0;

                playerScript.speed += increaseSpeed;
                shotTimeShockWave *= decreaseCooldown;
                shotTimeMultipleBalls *= decreaseCooldown;
                StartCoroutine(Delay());
                shotTimeBuff = Time.time + coolDown[2] + buffTime;
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(buffTime);
        playerScript.speed -= increaseSpeed;
        usedBuff = false;
    }

    
    void Meteorite()
    {
        meteoriteImage.fillAmount = Mathf.Lerp(meteoriteImage.fillAmount, coolDown[3], Time.deltaTime * lerpSpeed[3]);

        if (Input.GetKeyDown(KeyCode.Q) && playerScript.currentMana >= manaCost[3])
        {
            if(Time.time >= shotTimeMeteorite)
            {
                playerScript.ManaCost(manaCost[3]);
                meteoriteImage.fillAmount = 0;

                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                Instantiate(meteoritePrt, castPoint.origin, Quaternion.identity);
                shotTimeMeteorite = Time.time + coolDown[3];
            }
        }
    }
    
}
