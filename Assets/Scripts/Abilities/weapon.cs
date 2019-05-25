using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour {

    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] shootSounds;

    private float shotTime;

	// Update is called once per frame
	void Update () {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                source.clip = shootSounds[Random.Range(0, shootSounds.Length)];
                source.Play();
                GameObject Projectile = Instantiate(projectile, shotPoint.position, transform.rotation);
                Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), GetComponentInParent<Collider2D>());
                shotTime = Time.time + timeBetweenShots;
            }
        }
	}
}
