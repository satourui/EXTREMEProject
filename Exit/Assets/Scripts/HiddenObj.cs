using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObj : MonoBehaviour
{
    [SerializeField, Header("覆っているオブジェクト")]
    public GameObject coveringObj; //覆っているオブジェクト
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ocObj = coveringObj.GetComponent<OpenAndCloseObj>();
        if (ocObj == null)
            return;

        if (ocObj.IsOpen)
        {
            GetComponent<PlacedObjParameter>().HiddenObj = false;
        }

        else
        {
            GetComponent<PlacedObjParameter>().HiddenObj = true;
        }
    }
}
