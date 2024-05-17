using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    AudioSource source;
    public AudioClip InGameMusic; 

    void Awake(){
        source= GetComponent<AudioSource>();
    }

    void Start()
    {
        source.clip = InGameMusic;
        source.Play();
    }
}
