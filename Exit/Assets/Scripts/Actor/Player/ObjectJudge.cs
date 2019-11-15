using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectJudge : MonoBehaviour
{
    GameObject currentObj;

    GameObject textObj;

    PlayerController pc;

    private GameObject m_camera;

    void Start()
    {
        textObj = GameObject.Find("GamePlayUI");
        pc = GetComponentInParent<PlayerController>();
        m_camera = GetComponentInParent<PlayerController>().MainCamera.gameObject;
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
            var tt = textObj.GetComponent<TalkTextUI>();
            tt.SelectMessage = po.SelectMessage;
            tt.ShowSelectMessage();
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
        if (/*pc.State == PlayerController.PlayerState.Normal*/
            GamePlayManager.instance.State==GamePlayManager.GameState.Play)
        {
            var tt = textObj.GetComponent<TalkTextUI>();
            tt.TextClose();
            currentObj = null;
            pc.SelectObj = currentObj;
            tt.CurrentMessagePage = 0;
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
