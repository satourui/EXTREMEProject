using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkTextUI : MonoBehaviour
{
    string[] mainMessages = new string[0];  //表示する本文字配列
    string selectMessage;  //選択できるときに表示されるメッセージ
    public Text text;  //表示するtextUI
    int currentMessagePage;  //現在のメッセージページ
    int maxMessagePage;  //メッセージの最大ページ数

    bool isMessageFade;  //本文のフェードが終わっていたらtrue
    bool isTalk;  //本文を読んでいるならtrue

    Color textColor;
    

    [SerializeField, Header("文字がフェードインするスピード")]
    private float messageFadeSpeed = 0;


    GameObject selectObj;  //表示しているテキストの情報を持っているオブジェクト


    public string[] MainMessages { get => mainMessages; set => mainMessages = value; }
    public int CurrentMessagePage { get => currentMessagePage; set => currentMessagePage = value; }
    public int MaxMessagePage { get => maxMessagePage; set => maxMessagePage = value; }
    public string SelectMessage { get => selectMessage; set => selectMessage = value; }
    public GameObject SelectObj { get => selectObj; set => selectObj = value; }
    public bool IsTalk { get => isTalk; set => isTalk = value; }

    void Start()
    {
        //mainMessages = new string[0];
        CurrentMessagePage = 0;
        textColor = text.GetComponent<Text>().color;
        textColor.a = 0;
        isMessageFade = false;
        IsTalk = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSelectMessage()
    {
        if (selectMessage == null)
            return;

        text.text = selectMessage;
        text.gameObject.SetActive(true);
    }

    public void TextActive()
    {
        if (!text.gameObject.activeSelf)
        {
            text.gameObject.SetActive(true);
        }
    }

    public void TextClose()
    {
        text.text = "";
        mainMessages = null;
        selectMessage = null;
        selectObj = null;
        text.gameObject.SetActive(false);
    }

    public void TextChange(int index)
    {
        text.text = mainMessages[index];
    }
    

    public void MessageReading()
    {
        if (!IsTalk)
        {
            return;
        }

        TextChange(CurrentMessagePage);
        TextActive();
        maxMessagePage = mainMessages.Length;
        text.color = textColor;
        

        //まだ文がフェード中
        if (!isMessageFade)
        {
            textColor.a += messageFadeSpeed;

            
            if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
                Input.GetMouseButtonDown(0))
            {
                isMessageFade = true;
            }

            
        }

        //フェードが修了
        else
        {
            textColor.a = 0;
            CurrentMessagePage++;
            isMessageFade = false;

            //本文を全て読んだら
            if (CurrentMessagePage > maxMessagePage - 1)
            {
                Start();

                //var objParameter = selectObj.GetComponent<PlacedObjParameter>();

                if (selectObj!=null)
                {
                    var objParameter = selectObj.GetComponent<PlacedObjParameter>();

                    //選んだオブジェクトがアイテムオブジェクトなら
                    if (objParameter.ItemObj)
                    {
                        var io = selectObj.GetComponent<ItemObj>();
                        io.ItemGet();
                        //io.DeleteObject();
                    }

                    //フラグを変更するオブジェクトなら
                    if (objParameter.FlagChangeObj)
                    {
                        selectObj.GetComponent<FlagChangeObj>().FlagOn();
                    }

                    //メッセージを変更するオブジェなら
                    if (objParameter.ChangeMessageObj)
                    {
                        var cmObj = selectObj.GetComponent<ChangeMessageObj>();

                        cmObj.MessageSwicting();
                        cmObj.ChangeLoopMessage_Flag();
                    }

                    //隠されているオブジェクトなら
                    if (objParameter.HiddenObj)
                    {
                        selectObj.GetComponent<HiddenObj>().StopHiding();
                    }

                    if (objParameter.ObjectSpawnObj)
                    {
                        var oso = selectObj.GetComponent<ObjectSpawnObj>();
                        oso.SpawnObject();
                        oso.GetSpawnItem();


                    }
                }

                TextClose();

                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Normal;
                //GamePlayManager.instance.Player.GetComponent<PlayerController>().SelectObj = null;
                GamePlayManager.instance.State = GamePlayManager.GameState.Play;
            }
            
        }
    }

    public void TextInvisible()
    {
        textColor.a = 0;
    }
}
