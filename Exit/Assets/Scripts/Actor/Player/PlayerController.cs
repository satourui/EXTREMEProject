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

    //テキスト処理関連
    //string[] messages = new string[0];  //オブジェクトの文字情報を保存するための配列
    TalkTextUI text;
    GameObject selectObj;  //プレイヤーが選択しているオブジェクト

    //アイテムリスト関連
    [SerializeField]
    private List<GameObject> itemList = new List<GameObject>();  //持っているアイテムリスト
    private int itemNum = 0;  //何番目のアイテムか
    private int itemQuantity = 0;  //アイテムの所持数


    private Transform mainCamera;   //メインカメラ
    private FlashLightController flashLight;  //懐中電灯

    public AudioClip[] audioClips = new AudioClip[4];
    //↓かんが追加
    private AudioSource audioSource;
    public bool isWalk;

    public bool isDead;//playerが死んだ
                       //↑

    [SerializeField]
    private Vector3 debugPos = Vector3.zero;
    
    
    public GameObject SelectObj { get => selectObj; set => selectObj = value; }
    public List<GameObject> ItemList { get => itemList; set => itemList = value; }
    public int ItemNum { get => itemNum; set => itemNum = value; }
    public Transform MainCamera { get => mainCamera; set => mainCamera = value; }
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        text = GameObject.Find("GamePlayUI").GetComponent<TalkTextUI>();
        flashLight = GetComponentInChildren<FlashLightController>();

        if (mainCamera == null)
        {
            var m_camera = (GameObject)Resources.Load("Prefabs/Camera");
            MainCamera = Instantiate(m_camera, transform.position, Quaternion.identity).transform;
        }

        //音声ファイルをコンポーネントして変数に格納する
        sound = GetComponent<AudioSource>();

        sound.clip = audioClips[0];
        audioSource = GetComponent<AudioSource>();
        

        Initialize();

    }

    public void Initialize()
    {
        //mainCamera.gameObject.GetComponent<CameraController>().RotateInitialize();

        itemNum = 0;
        
        isWalk = false;  //最初は歩いていない
        isDead = false;  //死んだかどうか
        
        flashLight.SwitchOff();
    }

    void Update()
    {
        var state = GamePlayManager.instance.State;

        if (isDead)
        {
            rb.angularDrag = 100;  //敵とぶつかったら無理やりとめる

            return;
        }

        itemQuantity = itemList.Count;  //アイテムの数を取得


        if (state == GamePlayManager.GameState.Play)
        {
            PlayerMove();
            PlayerRotate();
            FlashLightSwicthing();
            SelectObject();
            ItemChange();
            UseItem();
            
        }

        else if (state == GamePlayManager.GameState.Talk)
        {
            text.MessageReading();
        }

        else if (state == GamePlayManager.GameState.Pause)
        {

        }


        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.M))
        {
            transform.position = debugPos;
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
            //velocity.Set(mouse_x, -rigid.velocity.y, mouse_z);
            velocity.Set(mouse_x, 0, mouse_z);
            velocity = velocity.normalized * speed * Time.deltaTime;
            velocity = transform.rotation * velocity;
            rb.MovePosition(transform.position + velocity);

            SoundWalk();

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
        transform.rotation = Quaternion.Euler(0.0f, MainCamera.transform.localEulerAngles.y, 0.0f);
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
            flashLight.LightSwitching();

            audioSource.clip = audioClips[0];

            sound.PlayOneShot(sound.clip);
        }

        //パッドの十字キーが上下のどちらかに入力されているか取得(上なら+,下なら-)
        float switchingNum = Input.GetAxisRaw("LightSwitch");

        //上なら
        if (switchingNum > 0)
            flashLight.SwitchOn();

        //下なら
        else if (switchingNum < 0)
            flashLight.SwitchOff();
    }

    void SelectObject()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
            Input.GetMouseButtonDown(0))
        {
            //選択できるオブジェクトがなかったらreturn
            if (selectObj == null)
                return;

            //選択できない状態だったらreturn
            if (!selectObj.GetComponent<PlacedObj>().IsSelect)
                return;

            if (selectObj.GetComponent<PlacedObjParameter>().GoalObj)
            {
                if (GamePlayManager.instance.IsStageClearFlag)
                {
                    GamePlayManager.instance.GoalCheck();
                    return;
                }
            }


            text.MainMessages = selectObj.GetComponent<PlacedObj>().Messages;

            //開け閉めするオブジェクトなら
            if (selectObj.GetComponent<PlacedObjParameter>().OpenAndCloseObj)
            {
                selectObj.GetComponent<OpenAndCloseObj>().LoopAnimation();
                selectObj.GetComponent<OpenAndCloseObj>().ChangeSelectMessage();
            }
            

            else if (text.MainMessages.Length != 0)
            {
                //state = PlayerState.Talk;
                GamePlayManager.instance.State = GamePlayManager.GameState.Talk;
                text.IsTalk = true;
                text.TextInvisible();
                selectObj = null;
            }

        }

    }



    void SoundWalk()
    {
        //audioSource.clip = audioClips[1]; 
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
            var item = itemList[itemNum];

            if (item.GetComponent<PlacedObjParameter>().ItemObj)
            {
                item.GetComponent<ItemObj>().UseItem();
                itemQuantity = itemList.Count;
                return;
            }
            
        }

        var currentItem = itemList[itemNum];
        var io = currentItem.GetComponent<ItemObj>();

        if (io == null)
            return;

        io.MeltIce_Hand();
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
    

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            //isDead = true;
            GamePlayManager.instance.State = GamePlayManager.GameState.GameOver;
        }

        if (col.gameObject.tag == "GoalZone")
        {
            GamePlayManager.instance.State = GamePlayManager.GameState.StageClear;
        }
    }

    
    
}
