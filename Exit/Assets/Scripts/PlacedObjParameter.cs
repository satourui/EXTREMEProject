﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjParameter : MonoBehaviour
{
    [SerializeField, Header("1度しか選択できないオブジェ")]
    private bool onceObj = false;  //一度しか選択できないオブジェクトならtrue

    [SerializeField, Header("ループするメッセージが変わるオブジェ")]
    private bool changeMessageObj = false;  //メッセージが変わるオブジェ

    [SerializeField, Header("アイテムを獲得するオブジェ")]
    private bool itemDropObj = false;    //選択するとアイテムをドロップするオブジェクトならtrue

    [SerializeField, Header("フラグを変更するオブジェ")]
    private bool flagChangeObj = false;  //選択するとフラグが変更されるオブジェクトならtrue
    

    [SerializeField, Header("アニメーションをするオブジェ")]
    private bool animationObj = false;   //アニメーションをするオブジェクトならtrue


    public bool OnceObj { get => onceObj; set => onceObj = value; }
    public bool ChangeMessageObj { get => changeMessageObj; set => changeMessageObj = value; }
    public bool ItemDropObj { get => itemDropObj; set => itemDropObj = value; }
    public bool FlagChangeObj { get => flagChangeObj; set => flagChangeObj = value; }
    public bool AnimationObj { get => animationObj; set => animationObj = value; }
    
}
