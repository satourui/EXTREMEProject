using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMessageObj : MonoBehaviour
{

    [Header("文が変わる条件")]
    public bool afterSelect = false;  //選択後
    public bool forFlag = false;      //フラグによって

    //[Header("文を変える場所")]
    //public bool select;  //選択
    //public bool mainText; //本文

    [SerializeField, Header("このフラグがtrueなら文を変える")]
    private string flagName = "";

    [SerializeField, Header("フラグ変更時1度だけ表示される本文")]
    private string[] changeOnceMessages = new string[0];

    //[SerializeField, Header("フラグ変更後の文が出たらもう文を表示しない")]
    //private bool deleteMessage_Flag = false;

    [SerializeField, Header("フラグ変更後、文を繰り返す")]
    private bool loopMessageEndless = false;

    //[SerializeField, Header("変更後のセレクト文")]
    //private string selectMessage = "";


    [SerializeField, Header("本文の交換を繰り返すかどうか(例:電源を入れる/切る)")]
    private bool endlessSwiching = false;


    

    [SerializeField, Header("変更後のループする本文")]
    private string[] changeLoopMessages = new string[0];


    private bool isMessageChange = false;  //メッセージが変更できるならtrue

    //private bool isOnceMessageChange;  //1度だけ変更するメッセージが変更されたら
    

    GamePlayManager gameManager;
    PlacedObj placedObj;

    public bool IsMessageChange { get => isMessageChange; set => isMessageChange = value; }

    void Start()
    {
        gameManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
        placedObj = GetComponent<PlacedObj>();

        //isOnceMessageChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        MessageChange();
    }

    /// <summary>
    /// フラグよってメッセージを変える
    /// </summary>
    public void ChangeMessage_Flag()
    {
        if (forFlag && afterSelect)
        {
            if (gameManager.CurrentStageFlags[flagName])
            {

                placedObj.Messages = changeOnceMessages;

                forFlag = false;

            }
        }


        //フラグによって
        if (forFlag && !afterSelect)
        {
            //フラグがtrueなら
            if (gameManager.CurrentStageFlags[flagName])
            {
                //if (!isOnceMessageChange)
                //{
                //    placedObj.Messages = changeOnceMessages;
                //    isOnceMessageChange = true;
                //    return;
                //}

                //else
                {
                    placedObj.Messages = changeLoopMessages;
                    forFlag = false;
                }
                

            }
        }

    }

    //public void ChangeLoopMessage()
    //{
    //    if (afterSelect)
    //    {
    //        var po = GetComponent<PlacedObj>();
    //        if (select)
    //        {
    //            string temporaryMessage = po.SelectMessage;
    //            po.SelectMessage = selectMessage;
    //            selectMessage = temporaryMessage;
    //        }

    //        if (mainText)
    //        {
    //            string[] temporaryMessages = po.Messages;
    //            po.Messages = changeLoopMessages;
    //            changeLoopMessages = temporaryMessages;
    //        }
    //    }
    //}

    public void MessageSwicting()
    {
        if (afterSelect)
        {
            if (endlessSwiching)
            {
                string[] temporaryMessages = placedObj.Messages;
                placedObj.Messages = changeLoopMessages;
                changeLoopMessages = temporaryMessages;
            }

            else if (forFlag)
            {
                if (gameManager.CurrentStageFlags[flagName])
                {
                    isMessageChange = true;
                }
            }

            else
            {
                isMessageChange = true;
            }
        }
    }

    public void DeleteMessage()
    {
        //if (deleteMessage_Flag)
        {
            if (gameManager.CurrentStageFlags[flagName])
            {
                GetComponent<PlacedObj>().IsSelect = false;
            }
        }
    }

    public void ChangeLoopMessage_Flag()
    {
        if (loopMessageEndless)
        {
            if (gameManager.CurrentStageFlags[flagName])
            {
                //placedObj.Messages = changeLoopMessages;
                isMessageChange = true;
            }
        }
        
    }

    public void MessageChange()
    {
        

        if (isMessageChange)
        {
            placedObj.Messages = changeLoopMessages;
        }
    }
}
