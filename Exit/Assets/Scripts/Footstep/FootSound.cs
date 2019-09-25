using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    AudioSource sounds;

    public AudioClip[] clips = new AudioClip[2];

    private PlayerController playerController;

    bool b;

    // Use this for initialization
    void Start()
    {
        sounds = GetComponent<AudioSource>();

        sounds.clip = clips[0];

        playerController = GetComponent<PlayerController>();

        sounds.Stop();

        b = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.isWalk)
        //{
        //    //sounds.Play();
        //    Debug.Log("なっている");
        //}
        //else
        //{
        //    //sounds.Stop();
        //    sounds.Play();
        //} 
    }
}
