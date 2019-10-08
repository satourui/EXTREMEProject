using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_L : MonoBehaviour
{
    public bool inDoor;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        inDoor = false;

        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy")
        {
            inDoor = false;
            animator.SetBool("Open2", false);
        }
    }
}
