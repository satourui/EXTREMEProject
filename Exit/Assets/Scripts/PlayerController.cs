using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //音声再生
    private AudioSource sound;

    public float speed = 0.0f;　　//速度
    private Rigidbody rb;         //Rigidbody
    Vector3 velocity = Vector3.zero;  //移動量
    PlayerState state;
    

    string[] messages;  //オブジェクトの文字情報を保存するための配列
    TalkText text;
    int currentMessageNum;  //現在読んでいるメッセージの(ページ?)番号

    GameObject selectObj;  //プレイヤーが選択しているオブジェクト

    private bool menuFlag;  //ポーズメニューが出てるかどうか。
    private PauseScript pauseScript; //ポーズメニュースクリプト

    public Transform mainCamera;   //メインカメラ
    public GameObject flashLight;  //懐中電灯

    //↓かんが追加
    public AudioClip[] audioClips = new AudioClip[4];
    //↑

    public PlayerState State { get => state; set => state = value; }
    public GameObject SelectObj { get => selectObj; set => selectObj = value; }

    public enum PlayerState
    {
        Normal,
        Talk,
        Menu
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = PlayerState.Normal;
        text = GameObject.Find("TextUI").GetComponent<TalkText>();
        currentMessageNum = 0;
        //音声ファイルをコンポーネントして変数に格納する
        sound = GetComponent<AudioSource>();

        sound.clip = audioClips[0];
        //isWalk = false;  //最初は歩いていない
    }


    void Update()
    {
        PlayerMove();
        PlayerRotate();
        FlashLightSwicthing();

        if (state == PlayerState.Normal)
        {
            
            SelectObject();
        }

        else if (state == PlayerState.Talk)
        {
            TextReading();
        }
        else if(state == PlayerState.Menu)
        {
            ChangeMenuFlag();
        }

    }

    void PlayerMove()
    {
        //キーボード移動
        float mouse_x = Input.GetAxisRaw("Horizontal");
        float mouse_z = Input.GetAxisRaw("Vertical");
        //XとZへの力がどちらも0でないとき
        if (mouse_x != 0 || mouse_z != 0)
        {
            //移動
            velocity.Set(mouse_x, 0, mouse_z);
            velocity = velocity.normalized * speed * Time.deltaTime;
            velocity = transform.rotation * velocity;
            rb.MovePosition(transform.position + velocity);

            SoundOn();
        }
        else
        {
            //
            
        }

        //if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W)||
        //    Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        //{
            
        //    AudioSource.
        //}
        //else
        //{
            
        //}

        //パッド移動
        float pad_X = Input.GetAxisRaw("L_Stick_Hori");
        float pad_Z = Input.GetAxisRaw("L_Stick_Verti");

        //XとZへの力がどちらも0でないとき
        if (pad_X != 0 || pad_Z != 0)
        {
            //移動
            velocity.Set(pad_X, 0, pad_Z);
            velocity = velocity.normalized * speed * Time.deltaTime;
            velocity = transform.rotation * velocity;
            rb.MovePosition(transform.position + velocity);
        }
    }


    void PlayerRotate()
    {
        transform.rotation = Quaternion.Euler(0.0f, mainCamera.transform.localEulerAngles.y, 0.0f);
    }

    void FlashLightSwicthing()
    {
        //懐中電灯を検索
        //GameObject flashLight = GameObject.Find("FlashLight");

        //無かったらreturn
        if (flashLight == null)
            return;


        //ライトのスイッチを変更する条件
        if (Input.GetKeyDown(KeyCode.Z)
            || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            flashLight.GetComponent<FlashLightController>().LightSwitching();
            sound.PlayOneShot(sound.clip);
        }

        //パッドの十字キーが上下のどちらかに入力されているか取得(上なら+,下なら-)
        float switchingNum = Input.GetAxisRaw("LightSwitch");

        //上なら
        if (switchingNum > 0)
            flashLight.GetComponent<FlashLightController>().SwitchOn();

        //下なら
        else if (switchingNum < 0)
            flashLight.GetComponent<FlashLightController>().SwitchOff();
    }

    void SelectObject()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
            Input.GetKeyDown(KeyCode.Space))
        {

            if (selectObj == null)
                return;

            if (selectObj.GetComponent<PlacedObjParameter>().OpenAndCloseObj)
            {
                //OpenAndCloseObjをもっているなら
                //if (selectObj.GetComponent<OpenAndCloseObj>())
                //{
                //    selectObj.GetComponent<AnimationObj>().LoopAnimation();
                //}
                selectObj.GetComponent<OpenAndCloseObj>().LoopAnimation();
                

            }


            //if (selectObj.GetComponent<PlacedObjParameter>().ChangeMessageObj)
            //{
            //    selectObj.GetComponent<ChangeMessageObj>().ChangeMessage();
            //}
            

            //text.Messages = selectObj.GetComponent<PlacedObj>().Messages;
            //text.TextChange(0);
            //if (selectObj.GetComponent<PlacedObjParameter>().TalkObj)
            //{
            //    if (selectObj.GetComponent<PlacedObjParameter>().ChangeMessage_Flag)
            //        selectObj.GetComponent<ChangeMessageObj>().ChangeMessage();

            //    text.Messages = selectObj.GetComponent<PlacedObj>().Messages;

            //    //text.TextShow();
            //    //currentMessageNum = 0;
            //    //text.TextChange(currentMessageNum);
            //    text.TextReading();
            //}
            

            text.Messages = selectObj.GetComponent<PlacedObj>().Messages;

            //開け閉めするオブジェクトなら
            if (selectObj.GetComponent<PlacedObjParameter>().OpenAndCloseObj)
            {
                selectObj.GetComponent<OpenAndCloseObj>().ChangeSelectMessage();
            }

            else if (text.Messages.Length != 0)
            {
                state = PlayerState.Talk;
                text.TextChange(0);
            }

            ////本文がない場合
            //else
            //{
            //    var cmObj = selectObj.GetComponent<ChangeMessageObj>();
            //    if (cmObj == null)
            //        return;

            //    //cmObj.ChangeLoopMessage();
            //}
        }
        
    }

    void TextReading()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            //text.Messages = selectObj.GetComponent<PlacedObj>().Messages;

            if (/*selectObj.GetComponent<PlacedObjParameter>().TalkObj*/
                selectObj.GetComponent<PlacedObj>().IsSelect)
            {
                //if (selectObj.GetComponent<PlacedObjParameter>().ChangeMessage_Flag)
                //{
                //    selectObj.GetComponent<ChangeMessageObj>().ChangeMessage();
                //}
                //text.TextShow();
                //currentMessageNum = 0;
                //text.TextChange(currentMessageNum);
                text.TextReading();
            }

            else
            {
                state = PlayerState.Normal;
            }
        }

        //テキストUIのメッセージ配列の大きさを取得
        //var messageLength = text.Messages.Length;
        //text.CurrentMessageCount++;
        //メッセージが最後の文でなければ
        ////if (currentMessageNum < messageLength - 1)
        //{
        //    if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
        //        Input.GetKeyDown(KeyCode.Space)
        //        //||
        //        //text.CurrentMessageCount > text.NextMessageCount
        //        )
        //    {
        //        //currentMessageNum++;
        //        //text.TextChange(currentMessageNum);
        //        //text.CurrentMessageCount = 0;
        //        //text.TextReading();
        //    }
        //}

        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
        //        Input.GetKeyDown(KeyCode.Space)||
        //        text.CurrentMessageCount > text.NextMessageCount)
        //    {
        //        text.text.text = selectObj.GetComponent<PlacedObj>().SelectMessage;
        //        State = PlayerState.Normal;
        //        currentMessageNum = 0;
        //        text.CurrentMessageCount = 0;
        //    }

        //    if (selectObj.GetComponent<PlacedObjParameter>().FlagChangeObj)
        //    {
        //        selectObj.GetComponent<FlagChangeObj>().FlagOn();
        //    }
        //}

        
    }

    void SoundOn()
    {
        //sound.PlayOneShot(audioClips[0]);
        
    }

    void ChangeMenuFlag()
    {

    }
}
