﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJudge : MonoBehaviour
{
    GameObject currentObj;

    List<GameObject> currentObjList;

    GameObject textObj;

    TalkTextUI talkUI;

    PlayerController pc;

    private GameObject m_camera;

    void Start()
    {
        //textObj = GameObject.Find("GamePlayUI");
        //talkUI = textObj.GetComponent<TalkTextUI>();
        talkUI = GamePlayManager.instance.TalkText;
        pc = GetComponentInParent<PlayerController>();
        m_camera = GetComponentInParent<PlayerController>().MainCamera.gameObject;
        currentObjList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.rotation = m_camera.transform.transform.rotation;
    }

    

    void TalkPreparation()
    {
        if (/*pc.State == PlayerController.PlayerState.Normal*/
            GamePlayManager.instance.State == GamePlayManager.GameState.Play)
        {
            var po = currentObj.GetComponent<PlacedObj>();
            //var tt = textObj.GetComponent<TalkTextUI>();
            talkUI.SelectMessage = po.SelectMessage;
            talkUI.ShowSelectMessage();
            talkUI.SelectObj = currentObj;
            pc.SelectObj = currentObj;

            if (po.GetComponent<PlacedObjParameter>().ChangeMessageObj)
            {
                po.GetComponent<ChangeMessageObj>().ChangeMessage_Flag();
            }
        }
    }

    void TalkReset()
    {
        if (GamePlayManager.instance.State==GamePlayManager.GameState.Play)
        {
            //var tt = textObj.GetComponent<TalkTextUI>();
            talkUI.TextClose();
            currentObj = null;
            pc.SelectObj = currentObj;
            talkUI.CurrentMessagePage = 0;
        }

    }
    

    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;

        currentObjList.Insert(0, obj);

        if (currentObjList.Count == 0)
            return;
        currentObj = currentObjList[0];

        //var objParameter = obj.GetComponent<PlacedObjParameter>();
        //if (objParameter == null)
        //    return;

        var objParameter = currentObj.GetComponent<PlacedObjParameter>();
        if (objParameter == null)
            return;

        ////if (objParameter.TalkObj)
        //選択できる状態かつ隠されていないオブジェなら
        if (obj.GetComponent<PlacedObj>().IsSelect)
        {
            TalkPreparation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;

        if (currentObjList.Count == 0)
            return;
        currentObjList.Remove(obj);

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
