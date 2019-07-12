using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    string[] messages;  //表示する文字配列
    public Text text;  //表示するtext
    bool showFlag;    //テキストを表示できる状態ならtrue
    [SerializeField]
    private float nextMessageCount;  //次のテキストに強制的に切り替わる時間
    float currentMessageCount;  //現在のテキストが表示されてからの時間

    public string[] Messages { get => messages; set => messages = value; }
    public bool ShowFlag { get => showFlag; set => showFlag = value; }
    public float CurrentMessageCount { get => currentMessageCount; set => currentMessageCount = value; }
    public float NextMessageCount { get => nextMessageCount;}

    void Start()
    {
        messages = new string[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextShow()
    {
        //if (ShowFlag)
        {
            text.gameObject.SetActive(true);
            //GameObject.Find("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Talk;
        }
    }

    public void TextClose()
    {
        text.gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().State = PlayerController.PlayerState.Normal;
    }

    public void TextChange(int index)
    {
        text.text = messages[index];
    }
}
