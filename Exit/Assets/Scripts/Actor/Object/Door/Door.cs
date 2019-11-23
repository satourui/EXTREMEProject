using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Door_R door_R;
    private Door_L door_L;

    private Animator animator;

    private AudioSource audioSource;

    public AudioClip[] audioClip = new AudioClip[2];

    private float time;

    public GameObject[] door = new GameObject[2];


    //ナリが追加
    [SerializeField]
    private bool isLock = false;  //鍵がかかっているならtrue
    //


    // Start is called before the first frame update
    void Start()
    {
        door_R = door[0].GetComponent<Door_R>();

        door_L = door[1].GetComponent<Door_L>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLock)
        {
            if (door_R.inDoor && !door_L.inDoor)
            {
                animator.SetBool("Open1", true);
            }
            else if (door_L.inDoor && !door_R.inDoor)
            {
                animator.SetBool("Open2", true);
            }

            if (!door_R.inDoor && !door_L.inDoor)
            {
                animator.SetBool("Open1", false);
                animator.SetBool("Open2", false);

            }
        }
        //Debug.Log(": R :" + door_R.inDoor + ": L :" + door_L.inDoor);
    }

    public void ChangeSound()
    {
        if(audioSource.clip != audioClip[0])
        {
            audioSource.clip = audioClip[0];
        }
        else if(audioSource.clip != audioClip[1])
        {
            audioSource.clip = audioClip[1];
        }
    }

    public void PlayOnSound()
    {
        audioSource.Play();
    }

    public void Unlock()
    {
        isLock = false;
    }
}
