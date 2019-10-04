using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemObj : MonoBehaviour
{
    [SerializeField, Header("アイテムの名前")]
    private string dropItemName;

    [SerializeField, Header("アイテムのアイコン")]
    private Texture2D itemIcon;

    StageFlagManager flagManager;

    public Texture2D ItemIcon { get => itemIcon; set => itemIcon = value; }

    void Start()
    {
        flagManager = GameObject.FindGameObjectWithTag("FlagManager").GetComponent<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemGet()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().ItemList.Add(this.gameObject);
    }
}
