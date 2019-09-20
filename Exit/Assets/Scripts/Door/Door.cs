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

        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door_R.inDoor||!door_L.inDoor)// && chaild.rL == DoorChaild.RL.R)
        {
           // Debug.Log("R");
            //animator.SetBool("OpenR",true);
        }

        else if (door_L.inDoor||!door_R.inDoor)// && chaild.rL == DoorChaild.RL.R)
        {
           // Debug.Log("L");
            //animator.SetBool("OpenL", true);
        }
    }
}
