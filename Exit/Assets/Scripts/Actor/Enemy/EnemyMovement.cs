﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField,Header("速さ")]
    public float speed = 1.0f;

    private Transform target;

    [SerializeField, Header("ついていく？")]
    public bool isActive = false;

    private NavMeshAgent agent;
    
    FlashLightController flashLight;

    public float chaceDistance = 0;  //追いかけてくる直線距離
    float targetDistance;  //ターゲットとの直線距離

    //2019/10/10追加
    private Animator animator;

    //2019/10/11
    private PlayerController playerController;//playerの操作
    public bool isPlayerDead;

    //2019/10/15
    private Vector3 currentTargetPos;

    //2019/10/18
    [SerializeField, Header("見失ったらとどまる時間")]
    public float idleTime = 5f;

     private float currrentTime;

    private GameObject player;

    public enum EnemyState
    {
        Idle,Walk
    }
    public EnemyState state;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");//player関連
        player = GamePlayManager.instance.Player;
        playerController = player.GetComponent<PlayerController>();//player関連
        target = player.transform;//player関連

        agent = GetComponent<NavMeshAgent>();
        flashLight = GameObject.Find("FlashLight").GetComponent<FlashLightController>();

        animator = GetComponent<Animator>();

        isPlayerDead = false;

        currentTargetPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isDead)
        {
            isPlayerDead = true;//enemyのスクリプト用
            return;//playerが死んだら何もしない
        }

        targetDistance = (transform.position - currentTargetPos).magnitude;
        StateAnimation();


        if (targetDistance < 0.7f)//playerとenemyが近づいたら  ↓から
        {
            state = EnemyState.Idle;
        }

        if(state == EnemyState.Idle)//EnemyState.Idleの間動きを止めてアニメーション
        {
            currrentTime += Time.deltaTime;
            agent.ResetPath();
            if (currrentTime >= idleTime)//EnemyState.Idleの間動きを止めてアニメーション
            {
                state = EnemyState.Walk;
                currrentTime = 0;
                currentTargetPos = target.transform.position;//ここがないと敵が変な動きになる
            }//        
        }

        isActive = flashLight.LightOnFlag;
        if (isActive)
        {
            currentTargetPos = target.transform.position;
        }

        targetDistance = (transform.position - currentTargetPos).magnitude;

        if (isActive && targetDistance < chaceDistance &&state == EnemyState.Walk)
        {
            agent.SetDestination(currentTargetPos);
        }

        if (isActive && targetDistance < chaceDistance)
        {
            state = EnemyState.Walk;
        }
    }

    private void StateAnimation()
    {

        if (state == EnemyState.Walk)
        {
            animator.SetBool("IsWalk", true);
        }
        else if (state == EnemyState.Idle)
        {
            animator.SetBool("IsWalk", false);
        }
    }
}
