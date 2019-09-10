using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMessageObj : MonoBehaviour
{
    [SerializeField, Header("このフラグがtrueならメッセージを変える")]
    private string flagName;

    [SerializeField, Header("変更時に1度だけ現れるメッセージ")]
    private string[] changeMessages;

    [SerializeField, Header("FlagNameがtrueならもうテキストを表示しない")]
    private bool deleteTextObj;

    public string[] ChangeMessages { get => changeMessages; set => changeMessages = value; }

    StageFlagManager flagManager;

    void Start()
    {
        flagManager = GameObject.FindGameObjectWithTag("FlagManager").GetComponent<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMessage()
    {
        if (flagManager.flags[flagName])
        {
            GetComponent<PlacedObj>().Messages = changeMessages;
            GetComponent<PlacedObjParameter>().ChangeMessage_Flag = false;
        }
    }

    public void LockText()
    {
        if (deleteTextObj && flagManager.flags[flagName])
        {
            GetComponent<PlacedObjParameter>().TalkObj = false;
        }
    }
}
