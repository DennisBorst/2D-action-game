using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    public GameObject explosion;

    public int damage;

    // Use this for initialization
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().DamageTaken(damage);
            DestroyProjectile();
        }

        if (collision.tag == "Wall")
        {
            DestroyProjectile();
        }

        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().DamageTaken(damage);
            DestroyProjectile();
        }
    }

}