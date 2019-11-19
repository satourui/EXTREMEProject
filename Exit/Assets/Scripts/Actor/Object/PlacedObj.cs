using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObj : MonoBehaviour
{
    [SerializeField, Header("選択前に表示されるメッセージ")]
    private string selectMessage;

    [SerializeField , Header("選択中に表示されるメッセージ")]
    private string[] messages;

    //[SerializeField, Header("選択後に永続的に表示されるメッセージ")]
    //private string[] afterMessages;

    public string SelectMessage { get => selectMessage; set => selectMessage = value; }
    public string[] Messages { get => messages; set => messages = value; }
    //public string[] AfterMessages { get => afterMessages; }
    public bool IsSelect { get => isSelect; set => isSelect = value; }

    private bool isSelect = true;  //選択できる状態ならtrue

    void Start()
    {
        isSelect = true;
    }

    
    void Update()
    {

    }

    public void ChangeEndMessage()
    {
        //messages = afterMessages;
    }

    public void MessageDelete()
    {
        messages = new string[1] { "" };
    }

    
    
}
