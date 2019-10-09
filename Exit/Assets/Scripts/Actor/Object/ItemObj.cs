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


    private bool isUse; //アイテムが使えるならtrue;
    GamePlayManager gameManager;
    TalkText talkText;

    public Texture2D ItemIcon { get => itemIcon; set => itemIcon = value; }

    void Start()
    {
        gameManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();
        isUse = false;
        talkText = GameObject.Find("GamePlayUI").GetComponent<TalkText>();
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
            if (gameManager.flags[flagName])
            {
                isUse = true;
            }
        }
        
    }

    public void ItemGet()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().ItemList.Add(this.gameObject);
        GetComponent<PlacedObjParameter>().ItemDropObj = false;
    }

    public void UseItem()
    {
        if (frontObj)
        {
            if (talkText.SelectObj == targetObj)
            {
                isUse = true;
                targetObj.GetComponent<ChangeMessageObj>().IsMessageChange = true;
            }
        }

        //使えるなら
        if (isUse)
        {
            talkText.MainMessages = itemMessages;
            var player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.State = PlayerController.PlayerState.Talk;
            player.ItemDelete(player.ItemNum);
            talkText.TextActive();
            talkText.TextInvisible();
            talkText.IsTalk = true;
        }

        
    }

    public void DeleteObject()
    {
        if (isDeleteObject)
        {
            gameObject.SetActive(false);
        }
    }
}
