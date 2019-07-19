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
            var po = currentObj.GetComponent<PlacedObj>();
            //pi.HitFlag = true;
            var tt = textObj.GetComponent<TalkText>();
            //tt.Messages = pi.Messages;
            //tt.text.text = po.SelectMessage;
            tt.SelectMessage = po.SelectMessage;
            //tt.ShowFlag = true;
            tt.TextShow();
            tt.SelectObj = currentObj;
            pc.SelectObj = currentObj;
        }
    }

    void TalkReset()
    {
        var tt = textObj.GetComponent<TalkText>();
        //tt.ShowFlag = false;
        tt.TextClose();
        currentObj = null;
        pc.SelectObj = currentObj;
        tt.CurrentMessagePage = 0;
        pc.State = PlayerController.PlayerState.Normal;
    }
    

    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        var objParameter = obj.GetComponent<PlacedObjParameter>();
        if (objParameter == null)
            return;

        currentObj = obj;

        //if (objParameter.TalkObj)
        {
            TalkPreparation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;

        var objParameter = obj.GetComponent<PlacedObjParameter>();
        if (objParameter == null)
            return;

        //if (itemParameter.TalkObj)
        {
            TalkReset();
        }
        //currentObj = null;
    }
}
