using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform playerTransform;

    public float bulletSpeed;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.LookAt(playerTransform);
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 playerCenter = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, playerTransform.position.z);

        var bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        bullet.GetComponentInChildren<Rigidbody>().AddForce((playerCenter - transform.position) * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }
}
