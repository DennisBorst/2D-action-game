using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

    public CircleCollider2D collider;
    [SerializeField]
    private float radiusSpeed = 0.1f;
    [SerializeField]
    private float maxRadius= 1f;

    public int damage;


    private void Start()
    {
        collider.radius = 0;
    }
    // Update is called once per frame
    void Update () {
        collider.radius += radiusSpeed;

        if(collider.radius >= maxRadius)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().DamageTaken(damage);
        }
    }
}
