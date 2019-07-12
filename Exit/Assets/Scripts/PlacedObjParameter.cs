using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjParameter : MonoBehaviour
{
    [SerializeField]
    private bool talkObj;       //選択するとテキストが表示されるだけのオブジェクトならtrue
    [SerializeField]
    private bool itemDropObj;   //選択するとアイテムをドロップするオブジェクトならtrue

    public bool TalkObj { get => talkObj;}
    public bool ItemDropObj { get => itemDropObj; set => itemDropObj = value; }
}
