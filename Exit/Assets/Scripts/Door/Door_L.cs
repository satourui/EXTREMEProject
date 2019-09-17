using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_L : MonoBehaviour
{
    public bool inDoor;
    // Start is called before the first frame update
    void Start()
    {
        inDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Debug.Log("当たっている");
            inDoor = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy")
        {
            inDoor = false;
        }
    }
}
