using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMessageObj : MonoBehaviour
{
    
    [Header("メッセージが変わる条件")]
    public bool afterSelect;  //選択後
    public bool forFlag;      //フラグによって

    [Header("メッセージを変える場所")]
    public bool select;  //選択
    public bool mainText; //本文

    [SerializeField, Header("このフラグがtrueならメッセージを変える")]
    private string flagName;

    [SerializeField, Header("変更後のセレクト文")]
    private string selectMessage;

    [SerializeField, Header("変更後の本文")]
    private string[] changeMessages;


    [SerializeField, Header("FlagNameがtrueならもうテキストを表示しない")]
    private bool deleteTextObj;

    public string[] ChangeMessages { get => changeMessages; set => changeMessages = value; }

    StageFlagManager flagManager;

    void Start()
    {
        flagManager = GameObject.FindGameObjectWithTag("FlagManager").GetComponent<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// フラグよってメッセージを変える
    /// </summary>
    public void ChangeMessage_Flag()
    {
        //フラグによって
        if(forFlag)
        {
            //フラグがtrueなら
            if (flagManager.flags[flagName])
            {
                if (select)
                {
                    GetComponent<PlacedObj>().SelectMessage = selectMessage;
                }

                if (mainText)
                {
                    GetComponent<PlacedObj>().Messages = changeMessages;
                }
            }
        }
        
    }

    public void ChangeLoopMessage()
    {
        if (afterSelect)
        {
            var po = GetComponent<PlacedObj>();
            if (select)
            {
                string temporaryMessage = po.SelectMessage;
                po.SelectMessage = selectMessage;
                selectMessage = temporaryMessage;
            }

            if (mainText)
            {
                string[] temporaryMessages = po.Messages;
                po.Messages = changeMessages;
                changeMessages = temporaryMessages;
            }
        }
    }

    public void LockText()
    {
        if (deleteTextObj && flagManager.flags[flagName])
        {
            //GetComponent<PlacedObjParameter>().TalkObj = false;
            GetComponent<PlacedObj>().IsSelect = false;
        }
    }

    public void DeleteMainText()
    {
        GetComponent<PlacedObj>().Messages = null;
    }
}
