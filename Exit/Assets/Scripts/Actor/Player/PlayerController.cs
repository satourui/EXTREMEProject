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

    //テキスト処理関連
    //string[] messages = new string[0];  //オブジェクトの文字情報を保存するための配列
    TalkText text;
    GameObject selectObj;  //プレイヤーが選択しているオブジェクト

    private bool menuFlag;  //ポーズメニューが出てるかどうか。

    //アイテムリスト関連
    public List<GameObject> itemList = new List<GameObject>();  //持っているアイテムリスト
    private int itemNum = 0;  //何番目のアイテムか
    private int itemQuantity = 0;  //アイテムの所持数


    public Transform mainCamera;   //メインカメラ
    public GameObject flashLight;  //懐中電灯

    //↓かんが追加したバギギメデカマラ美少年たち
    public AudioClip[] audioClips = new AudioClip[4];
    public bool isWalk;
    private Rigidbody rigid;

    public bool isDead;//playerが死んだ
    //↑
    
    //↓るいが追加した
    [SerializeField, Header("ゲームマネージャー")]
    public GameObject gameManager;
    [SerializeField, Header("ポーズスクリプトが入っているゲームマネージャー")]
    public PauseScript pauseScript;

    //↑
    public PlayerState State { get => state; set => state = value; }
    public GameObject SelectObj { get => selectObj; set => selectObj = value; }
    public List<GameObject> ItemList { get => itemList; set => itemList = value; }
    public int ItemNum { get => itemNum; set => itemNum = value; }
    public int ItemQuantity { get => itemQuantity; }

    public enum PlayerState
    {
        Normal,
        Talk,
        Menu,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = PlayerState.Normal;
        text = GameObject.Find("GamePlayUI").GetComponent<TalkText>();
        //音声ファイルをコンポーネントして変数に格納する
        sound = GetComponent<AudioSource>();

        sound.clip = audioClips[0];
        isWalk = false;  //最初は歩いていない
        isDead = false;  //死んだかどうか
        rigid = GetComponent<Rigidbody>();

        pauseScript = gameManager.GetComponent<PauseScript>();
    }


    void Update()
    {

        if (isDead)
        {
            rigid.angularDrag = 100;  //敵とぶつかったら無理やりとめる

            return;
        }

        itemQuantity = itemList.Count;  //アイテムの数を取得
        //ポーズ関連
        //ポーズメニュー時は動けないようにする
        if (state != PlayerState.Menu)
        {
            PlayerMove();
            PlayerRotate();
            FlashLightSwicthing();
            if (!pauseScript.GetPlayerflag())
            {
                state = PlayerState.Menu;
                Cursor.visible = true;
            }
        }
        else if (pauseScript.GetPlayerflag())
        {
            state = PlayerState.Normal;
            Cursor.visible = false;
        }


        if (state == PlayerState.Normal)
        {

            SelectObject();
            ItemChange();
            UseItem();
        }

        else if (state == PlayerState.Talk)
        {

        }

    }

    void PlayerMove()
    {
        isWalk = false;
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

            isWalk = true;
        }


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

            isWalk = true;
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
            /*Input.GetKeyDown(KeyCode.Space)*/
            Input.GetMouseButtonDown(0))
        {

            if (selectObj == null)
                return;

            //if (selectObj.GetComponent<PlacedObjParameter>().OpenAndCloseObj)
            //{
            //    selectObj.GetComponent<OpenAndCloseObj>().LoopAnimation();
            //}

            text.MainMessages = selectObj.GetComponent<PlacedObj>().Messages;

            //開け閉めするオブジェクトなら
            if (selectObj.GetComponent<PlacedObjParameter>().OpenAndCloseObj)
            {
                selectObj.GetComponent<OpenAndCloseObj>().LoopAnimation();
                selectObj.GetComponent<OpenAndCloseObj>().ChangeSelectMessage();
            }

            else if (text.MainMessages.Length != 0)
            {
                state = PlayerState.Talk;
                text.IsTalk = true;
                text.TextInvisible();
            }

        }

    }



    void SoundOn()
    {
        //sound.PlayOneShot(audioClips[0]);        
    }

    void ItemChange()
    {
        //アイテム所持数が0ならreturn
        if (itemQuantity == 0)
            return;

        //マウスホイールの大きさ取得
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");


        if (mouseScrollWheel < 0 ||
            Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            ItemNumDown();
        }

        else if (mouseScrollWheel > 0 ||
                 Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            ItemNumUp();
        }
    }

    void UseItem()
    {
        //アイテム所持数が0ならreturn
        if (itemQuantity == 0)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            itemList[itemNum].GetComponent<ItemObj>().UseItem();
        }
    }

    public void ItemDelete(int num)
    {
        itemList.RemoveAt(num);
        itemNum = 0;
    }

    public void ItemNumUp()
    {
        if (itemNum == itemQuantity - 1)
        {
            itemNum = 0;
            return;
        }
        itemNum++;
    }

    public void ItemNumDown()
    {
        if (itemNum == 0)
        {
            itemNum = itemQuantity - 1;
            return;
        }
        itemNum--;
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            isDead = true;
        }
    }
}
