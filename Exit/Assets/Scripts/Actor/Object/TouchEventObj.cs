using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEventObj : MonoBehaviour
{
    [SerializeField, Header("このフラグがtrueならイベントstart")]
    private string eventStartFlag = null;
    
    private bool eventFlag;  //フラグが建っていればtrue

    private bool isEventEnd;  //eventが行われたらtrue

    private bool isPlayerEvent;

    private bool isEnemyEvent;

    private bool isSoundEvent;

    void Start()
    {
        eventFlag = false;
        isEventEnd = false;

        if (GetComponent<PlayerEvent>())
            isPlayerEvent = true;

        if (GetComponent<EnemyEvent>())
            isEnemyEvent = true;

        if (GetComponent<SoundEvent>())
            isSoundEvent = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventFlag)
        {
            EventStart();
        }
    }

    private void EventStart()
    {
        //すでにイベントが行われていたらreturn
        if (isEventEnd)
            return;

        if (isPlayerEvent)
        {
            GetComponent<PlayerEvent>().EventStart();
        }

        if (isEnemyEvent)
        {
            GetComponent<EnemyEvent>().EventStart();
        }

        if (isSoundEvent)
        {
            GetComponent<SoundEvent>().EventStart();
        }


        isEventEnd = true;
    }

    public void FlagOn()
    {
        if (!GamePlayManager.instance.CurrentStageFlags[eventStartFlag]||
            eventFlag)
            return;

        eventFlag = true;
    }
    
}
