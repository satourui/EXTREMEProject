using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySE : MonoBehaviour
{
    private AudioSource audioSource;     //　AudioSource
    public AudioClip[] se;				//　効果音の配列
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = se[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Step()
    {
        audioSource.Play();//流す
    }

    void Stop()
    {
        audioSource.Stop();//止める
    }
}
