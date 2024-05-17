using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseProwl : MonoBehaviour
{
    
    // private GameManager gameManager;
    private Rigidbody2D rBody;
    public CapsuleCollider2D capsuleCollider;
    public SpriteRenderer sprite;
    private AudioSource audio;
    public AudioClip deathSound;
    public float enemySpeed = 3;
    public float enemyDirection = 1;
    
    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        // gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

   void FixedUpdate(){
        rBody.velocity = new Vector2(enemyDirection * enemySpeed, rBody.velocity.y);
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag =="CorpseProwler" || collision.gameObject.tag =="Pared"){
            if(enemyDirection==1) {
                enemyDirection =-1;
                sprite.flipX = true;
            }
            else if(enemyDirection==-1){
                enemyDirection=1;
                sprite.flipX = false;

            }
        }

        if(collision.gameObject.tag == "Player"){
            Character player = GameObject.FindObjectOfType<Character>();
            if(player.isDeath == false) player.StartCoroutine("Death");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag =="SwapSensor"){
            if(enemyDirection==1) {
                enemyDirection =-1;
                sprite.flipX = true;
            }
            else if(enemyDirection==-1){
                enemyDirection=1;
                sprite.flipX = false;

            }
        }
    }

    public void Death(){
        audio.PlayOneShot(deathSound);
        rBody.bodyType = RigidbodyType2D.Static;
        capsuleCollider.enabled=false;
        rBody.gravityScale = 0;
        enemyDirection = 0;
        Destroy(gameObject, 0.5f);
    }

}
