using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChaild : MonoBehaviour
{
    public enum RL
    {
        R,L
    };

    public RL rL;

    public bool b;
    // Start is called before the first frame update
    void Start()
    {
        b = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider o)
    {
        if(o.tag=="Enemy")
        {
            b = true;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Enemy")
        {
            b = false;
        }
    }
}
