using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjParameter : MonoBehaviour
{
    [SerializeField]
    private bool talkObj = false;        //選択するとテキストが表示されるだけのオブジェクトならtrue
    [SerializeField]
    private bool itemDropObj = false;    //選択するとアイテムをドロップするオブジェクトならtrue
    [SerializeField]
    private bool flagChangeObj = false;  //選択するとフラグが変更されるオブジェクトならtrue
    [SerializeField]
    private bool changeMessage_Flag = false;     //特定のフラグでメッセージが変わるオブジェクトならtrue
    [SerializeField]
    private bool animationObj = false;   //アニメーションをするオブジェクトならtrue

    public bool TalkObj { get => talkObj; set => talkObj = value; }
    public bool ItemDropObj { get => itemDropObj; set => itemDropObj = value; }
    public bool FlagChangeObj { get => flagChangeObj; set => flagChangeObj = value; }
    public bool ChangeMessage_Flag { get => changeMessage_Flag; set => changeMessage_Flag = value; }
    public bool AnimationObj { get => animationObj; set => animationObj = value; }
}
