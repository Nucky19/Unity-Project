using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 3;
    public float enemyDirection = 1;
    public bool triggered = false;

    private bool canShoot = true;
    public float timer;
    public float rateOfFire = 1;


    private Rigidbody2D rBody;
    private AudioSource source;
    public BoxCollider2D boxCollider;
    public Animator anim;
    public AudioSource audio;
    public AudioClip spectralSound;
    public Transform enemyBulletSpawn;
    public GameObject enemyBulletPrefab;
    // public AudioClip deathSound;
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        rBody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start(){
        StartCoroutine(EnemyMovement());
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public IEnumerator EnemyMovement()
    {
        while (!triggered)
        {
            if(enemyDirection==1)transform.rotation = Quaternion.Euler(0, 180, 0);
            else if(enemyDirection==-1)transform.rotation = Quaternion.Euler(0, 0, 0);

            rBody.velocity = new Vector2(enemyDirection * enemySpeed, rBody.velocity.y);
            yield return new WaitForSeconds(1);

            rBody.velocity = Vector2.zero; //Detiene el movimiento del enemigo
            yield return new WaitForSeconds(1);
            
            enemyDirection *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D target){     
        // if(target.gameObject.tag!="Bullet") audio.PlayOneShot(spectralSound);
    }

   

    void Shoot(){
        if(!canShoot){
            timer+=Time.deltaTime;
            if(timer>=rateOfFire){
                canShoot=true;
                timer=0;
            }
        }
        if((triggered) && (canShoot)){
            canShoot = false; 
            Instantiate(enemyBulletPrefab, enemyBulletSpawn.position,enemyBulletSpawn.rotation);
        }
    }

}
