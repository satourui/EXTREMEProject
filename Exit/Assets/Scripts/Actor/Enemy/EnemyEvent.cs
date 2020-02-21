using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObj = null;

    private EnemyMovement em;

    [SerializeField, Header("出現イベントならTrue")]
    private bool isAppearedEvent = false;

    //[SerializeField ,Header("触れたら消える場所")]
    //private GameObject lostPoint = null;

    private bool isEvent;

    private bool isAppeared;

    [SerializeField, Header("消えるまでの時間")]
    private float lostTime = 0.0f;

    private float currentTimer;

    void Start()
    {
        em = enemyObj.GetComponent<EnemyMovement>();
        isEvent = false;
        isAppeared = false;
        currentTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEvent)
        {
            if (isAppearedEvent)
            {
                AppearEvent();
                currentTimer += Time.deltaTime;
                if (currentTimer >= lostTime)
                {
                    enemyObj.SetActive(false);
                }
            }
        }
    }

    private void AppearEvent()
    {
        if (isAppeared)
            return;

        enemyObj.SetActive(true);

        isAppeared = true;
    }

    public void EventStart()
    {
        isEvent = true;
    }

    
}
