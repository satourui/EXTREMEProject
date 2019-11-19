using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjParameter : MonoBehaviour
{

    [SerializeField, Header("ゴール用のオブジェクト(アタッチスクリプトなし)")]
    private bool goalObj = false;

    [SerializeField, Header("自動ドアオブジェ")]
    private bool automaticDoorObj = false;

    [SerializeField, Header("ループするメッセージが変わるオブジェ")]
    private bool changeMessageObj = false;  //メッセージが変わるオブジェ

    [SerializeField, Header("アイテムオブジェ")]
    private bool itemObj = false;    //選択するとアイテムをドロップするオブジェクトならtrue

    [SerializeField, Header("フラグを変更するオブジェ")]
    private bool flagChangeObj = false;  //選択するとフラグが変更されるオブジェクトならtrue

    [SerializeField, Header("開け閉めできるオブジェ")]
    private bool openAndCloseObj = false;  //開け閉めするオブジェクトならtrue

    [SerializeField, Header("隠されているオブジェ")]
    private bool hiddenObj;  //隠されているオブジェクトならtrue

    [SerializeField,Header("オブジェクトを生成するオブジェ")]
    private bool objectSpawnObj;  //オブジェクトを生成するオブジェクトならtrue


    //public bool OnceObj { get => onceObj; set => onceObj = value; }
    public bool ChangeMessageObj { get => changeMessageObj; set => changeMessageObj = value; }
    public bool ItemObj { get => itemObj; set => itemObj = value; }
    public bool FlagChangeObj { get => flagChangeObj; set => flagChangeObj = value; }
    public bool OpenAndCloseObj { get => openAndCloseObj; set => openAndCloseObj = value; }
    public bool HiddenObj { get => hiddenObj; set => hiddenObj = value; }
    public bool GoalObj { get => goalObj; set => goalObj = value; }
    public bool AutomaticDoorObj { get => automaticDoorObj; set => automaticDoorObj = value; }
    public bool ObjectSpawnObj { get => objectSpawnObj; set => objectSpawnObj = value; }
}
