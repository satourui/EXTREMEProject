using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMessageObj : MonoBehaviour
{

    [Header("文が変わる条件")]
    public bool afterSelect;  //選択後
    public bool forFlag;      //フラグによって

    //[Header("文を変える場所")]
    //public bool select;  //選択
    //public bool mainText; //本文

    [SerializeField, Header("このフラグがtrueなら文を変える")]
    private string flagName = "";

    [SerializeField, Header("フラグがtrueのときに1度だけ表示される本文")]
    private string[] changeOnceMessages;

    [SerializeField, Header("フラグ変更後の文が出たらもう文を表示しない")]
    private bool deleteMessage_Flag;

    [SerializeField, Header("フラグ変更後、文を繰り返す")]
    private bool loopMessageEndless;

    //[SerializeField, Header("変更後のセレクト文")]
    //private string selectMessage = "";


    [SerializeField, Header("本文の交換を繰り返すかどうか(例:電源を入れる/切る)")]
    private bool endlessSwiching;

    [SerializeField, Header("変更後のループする本文")]
    private string[] changeLoopMessages;




    //public string[] ChangeLoopMessages { get => changeLoopMessages; set => changeLoopMessages = value; }
    //public bool LoopMessageEndless { get => loopMessageEndless; set => loopMessageEndless = value; }

    //public bool DeleteMessage_Flag { get => deleteMessage_Flag; set => deleteMessage_Flag = value; }
    //public bool EndlessSwiching { get => endlessSwiching; set => endlessSwiching = value; }

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
        if (forFlag)
        {
            //フラグがtrueなら
            if (flagManager.flags[flagName])
            {
                //if (select)
                //{
                //    GetComponent<PlacedObj>().SelectMessage = selectMessage;
                //}

                //if (mainText)
                {
                    GetComponent<PlacedObj>().Messages = changeOnceMessages;
                }
                forFlag = false;
                //afterSelect = true;

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
                var po = GetComponent<PlacedObj>();
                string[] temporaryMessages = po.Messages;
                po.Messages = changeLoopMessages;
                changeLoopMessages = temporaryMessages;
            }
        }
    }

    public void DeleteMessage()
    {
        if (deleteMessage_Flag)
        {
            if (flagManager.flags[flagName])
            {
                GetComponent<PlacedObj>().IsSelect = false;
            }
        }
    }

    public void ChangeLoopMessage_Flag()
    {
        if (loopMessageEndless)
        {
            if (flagManager.flags[flagName])
            {
                var po = GetComponent<PlacedObj>();
                po.Messages = changeLoopMessages;
            }
        }
        
    }
}
