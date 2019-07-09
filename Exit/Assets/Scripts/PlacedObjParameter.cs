using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjParameter : MonoBehaviour
{
    [SerializeField]
    private bool talkObj;       //選択するとテキストが表示されるだけのオブジェクトならtrue
    [SerializeField]
    private bool itemDropObj;   //選択するとアイテムをドロップするオブジェクトならtrue
    [SerializeField]
    private bool itemDropFlag;  //アイテムをドロップできる状態ならtrue;
    [SerializeField]
    private bool choiceObj;     //選択肢テキストが出るオブジェクトならtrue

    public bool TalkObj { get => talkObj;}
    public bool ItemDropObj { get => itemDropObj; set => itemDropObj = value; }
    public bool ItemDropFlag { get => itemDropFlag; set => itemDropFlag = value; }
    public bool ChoiceObj { get => choiceObj; set => choiceObj = value; }
}
