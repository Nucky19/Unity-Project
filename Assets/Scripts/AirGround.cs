using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGround : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rBody;
    public CharacterGroundSensor sensor;
    public bool isFalling = false;
    public float timeBeforeFalling = 2f;
    public float fallSpeed = 1f;


    void Awake(){
        rBody = GetComponent<Rigidbody2D>();
        rBody.bodyType = RigidbodyType2D.Static;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // if (isFalling)
    }

    public void PlayerDetected()
    {
        StartCoroutine(StartFallingCoroutine());
    }

    IEnumerator StartFallingCoroutine()
    {
        yield return new WaitForSeconds(timeBeforeFalling);
        rBody.bodyType = RigidbodyType2D.Dynamic;
        isFalling = true;
        anim.SetBool("isFalling", true);

        rBody.velocity = new Vector2(0f, -fallSpeed);
    }

    public void ReactivateCharacterRigidbody(Rigidbody2D characterRigidbody)
    {
        characterRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
