using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField, Header("フラグの名前(※英語)")]
    private List<string> flagNames = new List<string>();

    public Dictionary<string, bool> flags;  //フラグを管理するためのDictionary


    void Start()
    {
        //フラグ管理Dictionaryの初期化
        flags = new Dictionary<string, bool>();

        foreach (var flagName in flagNames)
        {
            flags.Add(flagName, false);
        }

        //マウスカーソルの削除
        Cursor.visible = false;

    }

    void Update()
    {

    }

    public void FlagOn(string flagName)
    {
        foreach (var name in flagNames)
        {
            if (name == flagName)
            {
                flags[name] = true;
            }
        }
    }
}
