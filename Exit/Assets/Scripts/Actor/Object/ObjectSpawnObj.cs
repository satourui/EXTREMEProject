using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnObj : MonoBehaviour
{
    [SerializeField, Header("生成オブジェクト")]
    private GameObject spawnObject = null;

    [SerializeField, Header("生成にフラグが必要ならtrue")]
    private bool forFlag = false;

    [SerializeField,Header("そのフラグ名")]
    private string flagName = "";

    private bool isSpwan;  //生成したらtrue



    void Start()
    {
        isSpwan = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
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
}
