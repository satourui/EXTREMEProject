using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJudge : MonoBehaviour
{
    GameObject currentObj;

    GameObject textObj;

    PlayerController pc;

    public GameObject m_camera;

    void Start()
    {
        textObj = GameObject.Find("UI");
        pc = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.rotation = m_camera.transform.transform.rotation;
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

            if (po.GetComponent<PlacedObjParameter>().ChangeMessageObj)
            {
                po.GetComponent<ChangeMessageObj>().ChangeMessage_Flag();
            }
        }
    }

    void TalkReset()
    {
        if (pc.State == PlayerController.PlayerState.Normal)
        {
            var tt = textObj.GetComponent<TalkText>();
            //tt.ShowFlag = false;
            tt.TextClose();
            currentObj = null;
            pc.SelectObj = currentObj;
            tt.CurrentMessagePage = 0;
            //pc.State = PlayerController.PlayerState.Normal;
        }

    }
    

    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        var objParameter = obj.GetComponent<PlacedObjParameter>();
        if (objParameter == null)
            return;

        currentObj = obj;

        //if (objParameter.TalkObj)
        //選択できる状態かつ隠されていないオブジェなら
        if (obj.GetComponent<PlacedObj>().IsSelect)
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
