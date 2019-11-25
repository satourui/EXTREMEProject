using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemObj : MonoBehaviour
{
    [SerializeField, Header("アイテムの名前")]
    private string dropItemName = "";

    [SerializeField, Header("アイテムのアイコン")]
    private Texture2D itemIcon = null;

    [SerializeField, Header("アイテムを使用したときの本文")]
    private string[] itemMessages = new string[0];

    [Header("アイテムを使用できる条件")]
    public bool anytime = false;  //いつでも
    public bool forFlag = false;  //フラグが建っている
    public bool frontObj = false;  //特定のオブジェクトの前

    [SerializeField, Header("フラグが必要ならそのフラグの名前")]
    private string flagName = "";

    [SerializeField, Header("どのオブジェクトに使えるか")]
    private GameObject targetObj = null;

    [SerializeField, Header("アイテム獲得後オブジェクトを消すならtrue")]
    private bool isDeleteObject = false;

    
    [SerializeField,Header("アイスオブジェクトならtrue")]
    private bool iceObj = false;

    private bool isMelt;  //溶けたらtrue

    [SerializeField, Header("溶けるまでの時間")]
    private float meltTimer = 0.0f;

    [SerializeField, Header("溶けた時に現れるオブジェクト")]
    private GameObject meltObj = null;


    [SerializeField, Header("特定のオブジェクトに置いたとき通常の何倍で溶けるか")]
    private float doubleTimes = 0;

    [SerializeField]
    private float iceCountDownTimer;

    private bool isTimerStart;

    private bool ishad;  //道具として持っていれば


    private bool isDrop;  //選択したときにアイテムを落とすならtrue


    private bool isUse; //アイテムが使えるならtrue;
    GamePlayManager gameManager;
    //TalkTextUI talkText;
    //PlayerController pc;

    private bool isFlagChange;  //フラグを変更できるならtrue 


    public Texture2D ItemIcon { get => itemIcon; set => itemIcon = value; }

    void Start()
    {
        Initialize();
        
    }

    public void Initialize()
    {
        gameManager = GamePlayManager.instance;
        isUse = false;
        //talkText = gameManager.TalkText;

        isMelt = false;
        isTimerStart = false;
        iceCountDownTimer = meltTimer;

        ishad = false;
        isDrop = true;

        if (GetComponent<PlacedObjParameter>().FlagChangeObj)
        {
            isFlagChange = true;
        }

        //pc = GamePlayManager.instance.PC;
    }


    // Update is called once per frame
    void Update()
    {
        if (anytime)
        {
            isUse = true;
        }

        if (forFlag)
        {
            if (gameManager.CurrentStageFlags[flagName])
            {
                isUse = true;
            }
        }

        
        MeltIce_Hand();
        MeltIce_Obj();
    }

    public void ItemGet()
    {
        if (!isDrop)
            return;



        GamePlayManager.instance.PC.ItemList.Add(gameObject);

        if (iceObj)
        {
            targetObj.GetComponent<PlacedObj>().IsSelect = true;
            ishad = true;
        }
        
        else
        {
            //GetComponent<PlacedObjParameter>().ItemObj = false;
            isDrop = false;
        }

        DeleteObject();
    }

    public void UseItem()
    {
        var talkText = GamePlayManager.instance.TalkText;
        var pc = GamePlayManager.instance.PC;

        if (frontObj)
        {
            var selectObj = talkText.SelectObj;
            
            if (selectObj == targetObj)
            {

                if (iceObj)
                {
                    gameObject.transform.position = meltObj.transform.position;
                    gameObject.SetActive(true);
                    isTimerStart = true;
                    pc.ItemDelete(pc.ItemNum);
                    targetObj.GetComponent<PlacedObj>().IsSelect = false;
                    ishad = false;
                }

                else
                {
                    isUse = true;

                    if (targetObj.GetComponent<PlacedObjParameter>().ChangeMessageObj)
                    {
                        targetObj.GetComponent<ChangeMessageObj>().IsMessageChange = true;
                    }
                    

                    if (targetObj.GetComponent<PlacedObjParameter>().AutomaticDoorObj)
                    {
                        targetObj.GetComponent<PlacedObj>().SelectMessage = "";
                        targetObj.GetComponentInChildren<Door>().Unlock();
                    }

                    if (targetObj.GetComponent<PlacedObjParameter>().ObjectSpawnObj)
                    {
                        var oso = targetObj.GetComponent<ObjectSpawnObj>();
                        oso.SpawnObject_Item();
                        oso.IsSpwan = true;
                        isDrop = true;
                    }

                    if (isFlagChange)
                    {
                        GetComponent<FlagChangeObj>().FlagOn();
                    }

                }
            }
        }

        //使えるなら
        if (isUse)
        {
            talkText.MainMessages = itemMessages;
            //talkText.SelectObj = this.gameObject;
            
            GamePlayManager.instance.State = GamePlayManager.GameState.Talk;
            pc.ItemDelete(pc.ItemNum);
            talkText.TextInvisible();
            
            talkText.IsTalk = true;

            
            
            //ItemGet();

            //DeleteObject();
            
        
        }

        
    }

    void DeleteObject()
    {
        if (isDeleteObject )
        {
            gameObject.SetActive(false);
        }
    }

    public void MeltIce_Obj()
    {
        if (iceObj)
        {
            if (isTimerStart && !ishad)
            {
                
                iceCountDownTimer -= Time.deltaTime * doubleTimes;
                
            }

            if (iceCountDownTimer < 0 && !isMelt)
            {
                isTimerStart = false;
                isMelt = true;
                iceObj = false;
                targetObj.GetComponent<PlacedObj>().IsSelect = true;
                meltObj.SetActive(true);
                gameObject.SetActive(false);
                
            }
        }
    }


    public void MeltIce_Hand()
    {
        if (!iceObj)
            return;

        var pc = GamePlayManager.instance.PC;

        if (pc.ItemList.Count == 0)
            return;

        var currentItem = pc.ItemList[pc.ItemNum];


        if (iceObj &&ishad)
        {

            if (currentItem == gameObject)
            {
                isTimerStart = true;
            }

            else
            {
                isTimerStart = false;
            }
            


            if (isTimerStart)
            {
                //if (!ishad)
                //{
                //    iceCountDownTimer -= Time.deltaTime * doubleTimes;
                //}

                //else
                {
                    iceCountDownTimer -= Time.deltaTime;
                }
            }

            if (iceCountDownTimer < 0 && !isMelt)
            {
                isTimerStart = false;
                isMelt = true;
                iceObj = false;
                targetObj.GetComponent<PlacedObj>().IsSelect = true;
                pc.ItemDelete(pc.ItemNum);
                pc.ItemList.Insert(0, meltObj);
                ishad = false;

                //if (ishad)
                //{

                //    pc.ItemList.Insert(0, targetObj);
                //    pc.ItemDelete(pc.ItemNum);
                //}

                //else
                //{
                //    meltObj.SetActive(true);
                //    gameObject.SetActive(false);
                //}
            }
        }

        
    }
}
