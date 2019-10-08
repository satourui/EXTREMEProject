using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObj : MonoBehaviour
{
    [SerializeField, Header("覆っているオブジェクト")]
    public GameObject coveringObj; //覆っているオブジェクト

    private bool isHidden = false;  //隠れていたらtrue

    public bool IsHidden { get => isHidden; set => isHidden = value; }

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
            //GetComponent<PlacedObjParameter>().HiddenObj = false;
            GetComponent<PlacedObj>().IsSelect = true;
            isHidden = false;
        }

        else
        {
            //GetComponent<PlacedObjParameter>().HiddenObj = true;
            GetComponent<PlacedObj>().IsSelect = false;
            isHidden = true;
        }
    }

    public void StopHiding()
    {
        if (!isHidden)
        {
            GetComponent<PlacedObjParameter>().HiddenObj = false;
        }
    }
}
