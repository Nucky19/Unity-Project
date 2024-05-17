using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;
    public Animator anim;
    Character player;


    void Awake(){
        anim = GetComponentInParent<Animator>();
        player = GetComponentInParent<Character>();
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Ground" || collider.gameObject.tag == "AirGround"){
            isGrounded = true;
            anim.SetBool("isJumping",false);
            anim.SetBool("inAir",false);
        }
        if(collider.gameObject.tag=="CorpseProwler") {

            player.rBody.AddForce(new Vector2(0,1) * player.jumpForce, ForceMode2D.Impulse);
            anim.SetBool("inAir",true);

            CorpseProwl prowler = collider.gameObject.GetComponent<CorpseProwl>(); 
            prowler.Death();
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag == "Ground" || collider.gameObject.tag == "AirGround"){
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Ground" || collider.gameObject.tag == "AirGround") {
            isGrounded = false;
            anim.SetBool("inAir",true);
            anim.SetBool("isRunning",false);
        }
    }
}
