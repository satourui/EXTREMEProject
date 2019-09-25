using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    public AudioClip sound1;
    private AudioSource audioSource;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = sound1;

        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerController.isWalk)
        {
            audioSource.PlayOneShot(sound1);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
