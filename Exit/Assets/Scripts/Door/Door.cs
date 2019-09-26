using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Door_R door_R;
    private Door_L door_L;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        door_R = GetComponentInChildren<Door_R>();

        door_L = GetComponentInChildren<Door_L>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door_R.inDoor&&!door_L.inDoor)
        {
            animator.SetBool("Open1",true);
        }
        else if (door_L.inDoor&&!door_R.inDoor)
        {
            animator.SetBool("Open2", true);
        }
    }
}
