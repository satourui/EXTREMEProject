using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    string[] messages;  //表示する文字配列
    public Text text;  //表示するtextUI
    bool showFlag;    //テキストを表示できる状態ならtrue
    int currentMessagePage;  //現在のメッセージページ
    int maxMessagePage;  //メッセージの最大ページ数
    string selectMessage;  //選択できるときに表示されるメッセージ

    GameObject selectObj;  //表示しているテキストの情報を持っているオブジェクト

    [SerializeField]
    private float nextMessageCount = 0;  //次のテキストに強制的に切り替わる時間
    float currentMessageCount;  //現在のテキストが表示されてからの時間

    public string[] Messages { get => messages; set => messages = value; }
    public bool ShowFlag { get => showFlag; set => showFlag = value; }
    public float CurrentMessageCount { get => currentMessageCount; set => currentMessageCount = value; }
    public float NextMessageCount { get => nextMessageCount;}
    public int CurrentMessagePage { get => currentMessagePage; set => currentMessagePage = value; }
    public int MaxMessagePage { get => maxMessagePage; set => maxMessagePage = value; }
    public string SelectMessage { get => selectMessage; set => selectMessage = value; }
    public GameObject SelectObj { get => selectObj; set => selectObj = value; }

    void Start()
    {
        messages = new string[0];
        CurrentMessagePage = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextShow()
    {
        if (selectMessage == null)
            return;

        //if (ShowFlag)
        {
            text.text = selectMessage;
            text.gameObject.SetActive(true);
            //GameObject.Find("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Talk;
        }
    }

    public void TextClose()
    {
        text.gameObject.SetActive(false);
        //GameObject.Find("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Normal;
    }

    public void TextChange(int index)
    {
        text.text = messages[index];
    }

    public void TextReading()
    {
        //if (selectObj.GetComponent<PlacedObjParameter>().TalkObj)
        {
            //messages = selectObj.GetComponent<PlacedObj>().Messages;
            maxMessagePage = messages.Length;
            if (CurrentMessagePage < MaxMessagePage - 1 )
            {
                CurrentMessagePage++;
                TextChange(CurrentMessagePage);
            }

            //メッセージを読み終わった後の処理
            else
            {
                CurrentMessagePage = 0;

                var objParameter = selectObj.GetComponent<PlacedObjParameter>();

                //選んだオブジェクトが1度しか選択できないオブジェクトなら
                if (objParameter.OnceObj)
                {
                    selectObj.GetComponent<PlacedObj>().IsSelect = false;
                }

                //選んだオブジェクトがフラグによってメッセージが変化するオブジェクトではなかったら
                //if (!objParameter.ChangeMessage_Flag)
                {
                    //selectObj.GetComponent<PlacedObj>().ChangeEndMessage();

                }
                //選んだオブジェクトがフラグによってメッセージが変化するオブジェクトなら
                //if (objParameter.ChangeMessageObj)
                //{
                //    selectObj.GetComponent<ChangeMessageObj>().LockText();
                //}

                //選んだオブジェクトがフラグによってメッセージが変化しないなら
                //if (!objParameter.ChangeMessage_Flag)
                {
                    //selectObj.GetComponent<PlacedObj>().MessageDelete();
                    //selectObj.GetComponent<PlacedObjParameter>().TalkObj = false;
                }

                //選んだオブジェクトがフラグを変更するオブジェクトなら
                if (objParameter.FlagChangeObj)
                {
                    selectObj.GetComponent<FlagChangeObj>().FlagOn();
                }

                if (objParameter.ChangeMessageObj)
                {
                    if (selectObj.GetComponent<ChangeMessageObj>().afterSelect)
                    {
                        selectObj.GetComponent<ChangeMessageObj>().ChangeLoopMessage();
                    }
                }

                TextClose();

                GameObject.Find("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Normal;



            }
        }
    }
}
