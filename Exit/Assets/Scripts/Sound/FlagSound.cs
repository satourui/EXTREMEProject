using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSound : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    private AudioClip clip;

    [SerializeField,Header("音楽がかかるフラグ名")]
    private string flagName;

    bool isStart;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        isStart = false;
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
