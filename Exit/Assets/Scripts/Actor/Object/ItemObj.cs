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
    
    private float iceCountDownTimer;

    private bool isTimerStart;
    




    private bool isUse; //アイテムが使えるならtrue;
    GamePlayManager gameManager;
    TalkTextUI talkText;

    public Texture2D ItemIcon { get => itemIcon; set => itemIcon = value; }

    void Start()
    {
        Initialize();
        
    }

    public void Initialize()
    {
        gameManager = GamePlayManager.instance;
        isUse = false;
        talkText = gameManager.TalkText;

        isMelt = false;
        isTimerStart = false;
        iceCountDownTimer = meltTimer;
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

        if (isTimerStart)
        {
            iceCountDownTimer -= Time.deltaTime;
        }

        if (iceCountDownTimer < 0 && !isMelt)
        {
            isTimerStart = false;
            isMelt = true;
            meltObj.SetActive(true);
            gameObject.SetActive(false);
            iceObj = false;
            targetObj.GetComponent<PlacedObj>().IsSelect = true;
        }
    }

    public void ItemGet()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ItemList.Add(this.gameObject);

        if (iceObj)
        {
            targetObj.GetComponent<PlacedObj>().IsSelect = true;
        }
        
        else
        {
            GetComponent<PlacedObjParameter>().ItemDropObj = false;
        }
    }

    public void UseItem()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

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
                    player.ItemDelete(player.ItemNum);
                    targetObj.GetComponent<PlacedObj>().IsSelect = false;
                }

                else
                {
                    isUse = true;
                    targetObj.GetComponent<ChangeMessageObj>().IsMessageChange = true;

                    if (targetObj.GetComponent<PlacedObjParameter>().AutomaticDoorObj)
                    {
                        targetObj.GetComponent<PlacedObj>().SelectMessage = "";
                        targetObj.GetComponentInChildren<Door>().Unlock();
                    }
                }
            }
        }

        //使えるなら
        if (isUse)
        {
            talkText.MainMessages = itemMessages;
            talkText.SelectObj = this.gameObject;
            
            GamePlayManager.instance.State = GamePlayManager.GameState.Talk;
            player.ItemDelete(player.ItemNum);
            talkText.TextInvisible();
            
            talkText.IsTalk = true;
            
        }

        
    }

    public void DeleteObject()
    {
        if (isDeleteObject )
        {
            gameObject.SetActive(false);
        }
    }
}
