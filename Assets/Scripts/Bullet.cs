using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float bulletSpeed;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // if(collider.gameObject.tag == "Player") return;
        // if(collider.gameObject.tag == "EnemyTrigger") return;
        // if(collider.gameObject.tag == "EnemyTrigger") return;
        if(collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Pared") BulletImpact();
        else if(collider.gameObject.tag != "Enemy") return;
        else Destroy(collider.gameObject);
        BulletImpact();
    }

    void BulletImpact(){
        Destroy(gameObject);
    }
}
