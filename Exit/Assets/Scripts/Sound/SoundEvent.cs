using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    private AudioClip clip = null;

    private bool isEventStart;


    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        isEventStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SoundOn()
    {
        if (!isEventStart)
        {
            source.Play();
            isEventStart = true;
        }
    }

    public void EventStart()
    {
        SoundOn();
    }

    
}
