using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float bulletSpeed;
    public Transform playerPosition;
    public Vector3 playerDirection;
    

    void Awake(){
        playerPosition= GameObject.Find("Character").GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start(){
        playerDirection=playerPosition.position-transform.position;
        rigidbody2D.AddForce(playerDirection * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy") return;
        if(collider.gameObject.tag == "EnemyTrigger") return;
        if(collider.gameObject.tag == "Player") {
            Character player = GameObject.FindObjectOfType<Character>();
            //Character player = collider.gameObject.GetComponent<Character>();
            if(player.isDeath == false) player.StartCoroutine("Death");
        }// Destroy(gameObject);
    }
}
