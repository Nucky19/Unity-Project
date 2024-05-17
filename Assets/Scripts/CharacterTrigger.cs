using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Coroutine stopAttack;
    Enemy spectre;

    void Awake(){
        boxCollider = GetComponent<BoxCollider2D>();
        spectre = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D target){     
        if(target.gameObject.tag=="Player")  {
            spectre.anim.SetBool("IsTriggered",true);
            // StartCoroutine(StartAttack());
            stopAttack=StartCoroutine(StartAttack());
        } 
    }

    void OnTriggerExit2D(Collider2D target){    
        if(target.gameObject.tag=="Player")  {
            StopCoroutine(stopAttack);
            spectre.triggered=false;
            spectre.anim.SetBool("IsTriggered",false);
        } 
    }

    IEnumerator StartAttack(){
        yield return new WaitForSeconds(1);
        spectre.triggered=true;
    }
}
