using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJudge : MonoBehaviour
{
    GameObject currentObj;

    GameObject textObj;

    PlayerController pc;

    void Start()
    {
        textObj = GameObject.Find("TextUI");
        pc = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    void TalkPreparation()
    {
        if (pc.State == PlayerController.PlayerState.Normal)
        {
            var pi = currentObj.GetComponent<PlacedObj>();
            //pi.HitFlag = true;
            var tt = textObj.GetComponent<TalkText>();
            //tt.Messages = pi.Messages;
            tt.text.text = pi.SelectMessage;
            //tt.ShowFlag = true;
            tt.TextShow();
            pc.SelectObj = currentObj;
        }
    }

    void TalkReset()
    {
        
        var tt = textObj.GetComponent<TalkText>();
        tt.ShowFlag = false;
        tt.TextClose();
        currentObj = null;
        pc.SelectObj = currentObj;
        //pc.State = PlayerController.PlayerState.Normal;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        var itemParameter = obj.GetComponent<PlacedObjParameter>();
        if (itemParameter == null)
            return;

        currentObj = obj;

        //if (itemParameter.TalkObj)
        {
            TalkPreparation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;

        var itemParameter = obj.GetComponent<PlacedObjParameter>();
        if (itemParameter == null)
            return;

        //if (itemParameter.TalkObj)
        {
            TalkReset();
        }
        //currentObj = null;
    }
}
