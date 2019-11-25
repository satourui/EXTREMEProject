using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnObj : MonoBehaviour
{
    [SerializeField, Header("生成オブジェクト")]
    private GameObject spawnObject = null;

    [SerializeField, Header("生成してそのままゲットするならtrue")]
    private bool isItemGet = false;

    [SerializeField,Header("このオブジェにアイテムを使用して生成されるならtrue")]
    private bool isItem = false;  //アイテムを使用して生成されるならtrue

    [SerializeField, Header("生成にフラグが必要ならtrue")]
    private bool forFlag = false;

    [SerializeField,Header("そのフラグ名")]
    private string flagName = "";

    [SerializeField, Header("生成した後に消すならチェック")]
    private bool isDeadflag;

    private bool isSpwan;  //生成したらtrue

    public bool IsSpwan { get => isSpwan; set => isSpwan = value; }

    void Start()
    {
        isSpwan = false;

        //if (GetComponent<PlacedObjParameter>().ItemObj)
        //{
        //    isItem = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        if (isItem ||isItemGet)
            return;

        if (forFlag)
        {
            if (!GamePlayManager.instance.CurrentStageFlags[flagName])
                return;
        }

        

        if (!isSpwan)
        {
            spawnObject.SetActive(true);
            isSpwan = true;
        }
    }

    public void SpawnObject_Item()
    {
        if (!isItem ||isItemGet)
            return;
        
        if (forFlag)
        {
            if (!GamePlayManager.instance.CurrentStageFlags[flagName])
                return;
        }

        if (!isSpwan)
        {
            spawnObject.SetActive(true);
            if (isDeadflag)
            {
                gameObject.SetActive(false);
            }
            isSpwan = true;
        }
    }

    public void GetSpawnItem()
    {
        
        if (isItemGet && isSpwan)
        {
            GamePlayManager.instance.PC.ItemList.Add(spawnObject);
            isItemGet = false;
        }
    }
}
