using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroundSensor : MonoBehaviour
{
    public bool triggered=false;
    public Animator anim;
    // public Rigidbody2D characterRigidbody;
    AirGround ground;

    void Awake(){
        anim = GetComponentInParent<Animator>();
        ground = GetComponentInParent<AirGround>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Debug.Log("PERSONAJE DETECTADO");
            triggered = true;
            ground.PlayerDetected();
            anim.SetBool("isFalling",true);

            // characterRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
    
}
