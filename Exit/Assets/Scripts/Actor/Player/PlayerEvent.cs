using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    //[SerializeField]
    //private bool movieEvent = false;

    [SerializeField,Header("プレイヤーの場所を移動させるイベントならtrue")]
    private bool playerMoveEvent = false;

    [SerializeField]
    private bool playerRotateEvent = false;

    [SerializeField,Header("移動させる場所")]
    private Vector3 movePoint = Vector3.zero;

    [SerializeField,Header("向かせたい方向")]
    private float roteDir = 0;

    [SerializeField,Header("向かせたい位置")]
    private Vector3 rotePos = Vector3.zero;

    [SerializeField]
    private float eventTime = 0;

    private float currentTime;

    private bool isEvent;

    void Start()
    {
        isEvent = false;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEvent)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= eventTime)
            {
                currentTime = 0;
                isEvent = false;
                GamePlayManager.instance.State = GamePlayManager.GameState.Play;
            }
        }
    }

    public void EventStart()
    {
        if (playerRotateEvent)
        {
            GamePlayManager.instance.PC.MainCamera.GetComponent<CameraController>().transform.localEulerAngles = new Vector3(0, roteDir, 0);
            GamePlayManager.instance.PC.MainCamera.GetComponent<CameraController>().Roteuler = new Vector3(0, roteDir, 0);

            //Vector3 targetDir = rotePos - GamePlayManager.instance.Player.transform.position;
            //targetDir.y = GamePlayManager.instance.Player.transform.position.y;
            //Vector3 dir = Vector3.RotateTowards(GamePlayManager.instance.Player.transform.forward, targetDir, 0.5f, 0);
            //GamePlayManager.instance.PC.MainCamera.rotation = Quaternion.LookRotation(dir);
            GamePlayManager.instance.PC.transform.eulerAngles = new Vector3(0, roteDir, 0);
            GamePlayManager.instance.State = GamePlayManager.GameState.Event;

        }

        if (playerMoveEvent)
        {
            GamePlayManager.instance.PC.transform.position = movePoint;
            GamePlayManager.instance.PC.MainCamera.position = GamePlayManager.instance.PC.transform.position + GamePlayManager.instance.PC.MainCamera.GetComponent<CameraController>().Offset;
        }


        isEvent = true;
    }
}
