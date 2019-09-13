using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    public float speed = 0.05f;

    private Vector3 originPos;

    public Vector3 target;

    private bool b;
    private DoorChaild chaild;

    public float leftRotation = 180;

    public enum Door
    {
        onR,onL,offR,offL
    }

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        chaild = GetComponentInChildren<DoorChaild>();

        originPos = transform.position;

        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if()koko8/03


        if (chaild.b)// && chaild.rL == DoorChaild.RL.R)
        {
            Debug.Log("入った");
            animator.SetBool("OpenR", !chaild.b);
            //animator.SetBool("OpenR", !animator.GetBool("Open"));
        }
        else if ( !chaild.b)// && chaild.rL == DoorChaild.RL.R)
        {
            Debug.Log("出た");
            animator.SetBool("OpenR", !chaild.b);
        }

        //if (chaild.b && chaild.rL == DoorChaild.RL.L)
        //{
        //    animator.SetBool("OpenL", true);
        //}
        //else if (!chaild.b && chaild.rL == DoorChaild.RL.L)
        //{
        //    animator.SetBool("OpenL", false);
        //}
    }

    //if(!chaild.b)
    //{

    //    //var v = (originPos - transform.position).normalized;

    //    //transform.position += v * speed;

    //    //Vector3 angle = new Vector3(0, -1, 0);

    //    //if (transform.localEulerAngles.y > 270)
    //    //{

    //    //    return;
    //    //}

    //    //transform.localEulerAngles += angle;



    //    if (Mathf.Abs(leftRotation - 90f) > 0.1f)
    //    {
    //        leftRotation += 5f;
    //        transform.eulerAngles += new Vector3(0f, -5f, 0f);
    //    }
    //}

    //if (chaild.b)
    //{
    //    Debug.Log("b");

    //    var v = (target - transform.position).normalized;

    //    transform.position += v * speed;

    //    Vector3 angle = new Vector3(0, 1, 0);

    //    if (transform.localEulerAngles.y > 90)
    //    {
    //        return;
    //    }

    //    transform.localEulerAngles += angle;
    //}
}
