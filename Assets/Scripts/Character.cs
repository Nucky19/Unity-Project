using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Character : MonoBehaviour
{
    //Variables
    private float inputHorizontal;
    public float movementSpeed=6;
    public float jumpForce=13;

    private bool canShoot = true;
    public float timer;
    public float rateOfFire = 1;
    public bool isDeath = false;
    private bool isShooting = false;

    //Components
    public Rigidbody2D rBody;
    public SpriteRenderer sprite;
    public Animator anim;
    public AudioSource audio;
    public GroundSensor sensor;
    public UnityEngine.Rendering.Universal.Light2D glow; 
   
    //Audios
    public AudioClip deathSound;

    public Transform bulletSpawn;
    public GameObject bulletPrefab;
  
    void Awake(){
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rBody = GetComponent<Rigidbody2D>();
        glow = transform.Find("PlayerGlow").GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        rBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    void Update(){
        if (!isShooting)  
        {
            FaceOrientation();
            Jump();
        }
        if (sensor.isGrounded) Shoot();
    }

    void FixedUpdate(){
        if (!isShooting)  
        {
            Run();
        }
    }


    //Functions
    void FaceOrientation(){
        inputHorizontal = Input.GetAxis("Horizontal");
        if(inputHorizontal<0){
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (sensor.isGrounded) anim.SetBool("isRunning",true);
            // Debug.Log("Mirando Izquierda");
        }
        else if(inputHorizontal>0){
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (sensor.isGrounded) anim.SetBool("isRunning",true);
            // Debug.Log("Mirando Derecha");
        }else anim.SetBool("isRunning",false);

    }

    void Run(){
        rBody.velocity = new Vector2(inputHorizontal *  movementSpeed, rBody.velocity.y);
    }

    void Jump(){
        if(Input.GetButtonDown("Jump") && sensor.isGrounded){
            rBody.AddForce(new Vector2(0,1) * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJumping",true);
            StartCoroutine(InAir());
            Debug.Log("Saltando");
        }
    }

    IEnumerator InAir(){
        yield return new WaitForSeconds(1);
        if (!sensor.isGrounded) anim.SetBool("inAir",true);
    }

    void Shoot(){
        if(!canShoot){
            timer+=Time.deltaTime;
            if(timer>=rateOfFire){
                canShoot=true;
                timer=0;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && canShoot){
            isShooting = true;
            anim.SetBool("isRunning",false);
            anim.SetBool("isShooting",true);
            canShoot = false; 
            StartCoroutine(IsShootting());
            StartCoroutine(IsShoottingEnd());
        }
    }

    IEnumerator IsShootting(){
        rBody.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.7f);
        Instantiate(bulletPrefab, bulletSpawn.position,bulletSpawn.rotation);
        
    }
    IEnumerator IsShoottingEnd(){
        yield return new WaitForSeconds(1);
        anim.SetBool("isShooting",false);
        rBody.bodyType = RigidbodyType2D.Dynamic;
        isShooting = false;
    }
    
    void OnTriggerEnter2D(Collider2D target){     
        if(target.gameObject.tag=="LightTrigger") glow.intensity=0;
        if(target.gameObject.tag=="Void") StartCoroutine(Death ());
    }

    void OnTriggerExit2D(Collider2D target){    
        if(target.gameObject.tag=="LightTrigger") glow.intensity=1.1f;
    }

    public IEnumerator Death(){
        // Time.timeScale = 0.5; Tocar el tiempo, mas rápido o mas lento
        Debug.Log("Muerto");
        isDeath = true;
        anim.SetBool("isDeath",true);
        rBody.bodyType = RigidbodyType2D.Static;
        audio.PlayOneShot(deathSound);

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null) boxCollider.enabled = false;
        rBody.gravityScale = 0;

        Transform groundSensorTransform = transform.Find("GroundSensor");
        if (groundSensorTransform != null) Destroy(groundSensorTransform.gameObject);
    
        yield return new WaitForSeconds(1);
        sprite.enabled=false;
        yield return new WaitForSeconds(1);
        // SceneManager.LoadScene("Game Over");
        Debug.Log("ObjectDestroyed");
        Destroy(gameObject);
    }

}
