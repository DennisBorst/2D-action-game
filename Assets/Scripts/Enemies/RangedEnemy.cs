using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {

    public GameObject enemyBullet;
    public Transform leftHand;
    public Transform rightHand;
    public float stopDistance;

    private float attackTime;
    private Animator anim;

    private void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            if(Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
	}

    public void RangedAttack()
    {
        if(player != null)
        {
            Vector2 direction = player.position - leftHand.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            leftHand.rotation = rotation;

            GameObject EnemyProjectile1 = Instantiate(enemyBullet, leftHand.position, leftHand.rotation);
            GameObject EnemyProjectile2 = Instantiate(enemyBullet, rightHand.position, leftHand.rotation);

            Physics2D.IgnoreCollision(EnemyProjectile1.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(EnemyProjectile2.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
