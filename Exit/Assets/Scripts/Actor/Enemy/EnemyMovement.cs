using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField,Header("速さ")]
    public float speed = 1.0f;
    [SerializeField, Header("Playerのオブジェ")]
    public Transform target;
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

    public enum EnemyState
    {
        Idle,Walk
    }
    public EnemyState state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        flashLight = GameObject.Find("FlashLight").GetComponent<FlashLightController>();

        animator = GetComponent<Animator>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();//Playerのスクリプト

        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isDead)
        {
            isPlayerDead = true;//enemyのスクリプト用
            return;//playerが死んだら何もしない
        }

        targetDistance = (transform.position - target.position).magnitude;

        isActive = flashLight.LightOnFlag;
        
        if (isActive && targetDistance < chaceDistance)
        {
            agent.SetDestination(target.position);
        }

        if(state == EnemyState.Walk)
        {
            animator.SetBool("IsWalk", true);
        }
    }
}
