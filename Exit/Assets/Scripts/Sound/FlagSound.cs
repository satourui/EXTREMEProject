using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSound : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    private AudioClip clip = null;

    [SerializeField, Header("音楽がかかるフラグ名")]
    private string flagName = null;

    bool isStart;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        isStart = false;
        //source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePlayManager.instance.CurrentStageFlags[flagName])
        {
            SoundOn();
        }
    }

    void SoundOn()
    {
        if (!isStart)
        {
            source.Play();
            isStart = true;
        }
    }
}
