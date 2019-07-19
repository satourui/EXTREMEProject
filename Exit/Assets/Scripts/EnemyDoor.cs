using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    public float speed = 0.05f;

    private Vector3 originPos;

    public Vector3 target;

    private bool b;
    // Start is called before the first frame update
    void Awake()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(b)
        {
            //var v = (originPos - transform.position).normalized;

            //transform.position += v * speed;

            Vector3 angle = new Vector3(0, -1, 0);

            if (transform.localEulerAngles.y > 270)
            {
                
                return;
            }

            transform.localEulerAngles += angle;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
  
            //var v = (target - transform.position).normalized;

            //transform.position += v * speed;

            Vector3 angle = new Vector3(0, 1, 0);

            if(transform.localEulerAngles.y > 90)
            {
                return;
            }

            transform.localEulerAngles += angle;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            b = true;
        }
    }
}
