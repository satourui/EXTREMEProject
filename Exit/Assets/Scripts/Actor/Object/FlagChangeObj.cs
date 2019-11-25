using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeObj : MonoBehaviour
{
    [SerializeField, Header("変更するフラグの名前")]
    private string changeFlagName = "";

    [Header("フラグ変更条件")]
    public bool none = false;
    public bool forFlag = false;

    [SerializeField,Header("このフラグがtrueなら変更できる")]
    private string conditionFlagName = "";

    [SerializeField, Header("フラグを変更してお役御免ならtrue")]
    private bool isDead = false;

    [SerializeField,Header("クリアフラグを変更するオブジェクトならtrue")]
    private bool isClearFlagChange = false;  //クリアフラグをチェンジするオブジェクトならtrue


    private bool isChangeFlag = false;  //フラグが変更できるならtrue

    GamePlayManager gameManager;
    

    void Start()
    {
        gameManager = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();

        if (none)
        {
            isChangeFlag = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!forFlag)
        {
            return;
        }

        if (GamePlayManager.instance.CurrentStageFlags[conditionFlagName])
        {
            isChangeFlag = true;
        }
    }

    public void FlagOn()
    {
        if (isChangeFlag)
        {
            if (isDead)
            {
                Destroy(gameObject);
            }

            if (isClearFlagChange)
            {
                GamePlayManager.instance.IsStageClearFlag = true;
                isClearFlagChange = false;
                return;
            }

            gameManager.FlagOn(changeFlagName);

            
        }
    }
}
