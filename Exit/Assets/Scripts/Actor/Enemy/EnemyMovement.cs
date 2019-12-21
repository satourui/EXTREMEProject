using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    #region カンくんのコード
    //[SerializeField,Header("速さ")]
    //public float speed = 1.0f;

    //private Transform target;

    //[SerializeField, Header("ついていく？")]
    //public bool isActive = false;

    //private NavMeshAgent agent;

    //FlashLightController flashLight;

    //public float chaceDistance = 0;  //追いかけてくる直線距離
    //float targetDistance;  //ターゲットとの直線距離

    ////2019/10/10追加
    //private Animator animator;

    ////2019/10/11
    //private PlayerController playerController;//playerの操作
    //public bool isPlayerDead;

    ////2019/10/15
    //private Vector3 currentTargetPos;

    ////2019/10/18
    //[SerializeField, Header("見失ったらとどまる時間")]
    //public float idleTime = 5f;

    // private float currrentTime;

    //private GameObject player;

    ////2019/11/25
    //private bool isPlayerFound;


    //public Transform gameOverInitPos;

    //bool isGameoverFlag;
    #endregion

    private GameObject target;

    NavMeshAgent agent;

    [SerializeField]
    private GameObject[] routeTargetArray = new GameObject[0];  //巡回ルート配列

    private int routeTargetNum;  //現在の巡回ターゲット番号

    [SerializeField]
    private float chaseDistance = 0;  //playerとの直線距離がこれより短かったら追いかける

    private float targetDistance;  //playerとの直線距離

    private bool isChase;

    private bool isLightOn;  //ライトが点いていたらtrue
    private FlashLightController flashLight;

    private Animator animator;

    //public enum EnemyState
    //{
    //    Idle,Walk,Found
    //}
    //public EnemyState state;

    // Start is called before the first frame update
    void Start()
    {
        #region カンくんのコード
        //player = GameObject.FindGameObjectWithTag("Player");//player関連
        //player = GamePlayManager.instance.Player;
        //playerController = GamePlayManager.instance.PC;//player関連
        //target = player.transform;//player関連

        //agent = GetComponent<NavMeshAgent>();
        //flashLight = GameObject.Find("FlashLight").GetComponent<FlashLightController>();

        //animator = GetComponent<Animator>();

        //isPlayerDead = false;

        //currentTargetPos = Vector3.zero;

        //isPlayerFound = false;

        //isGameoverFlag = false;

        //isChaseFlag = false;
        #endregion

        target = GamePlayManager.instance.Player;
        agent = GetComponent<NavMeshAgent>();
        routeTargetNum = 0;
        targetDistance = 0;
        isChase = false;
        isLightOn = false;
        flashLight = GamePlayManager.instance.PC.GetComponentInChildren<FlashLightController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region カンくんのコード
        //var gameState = GamePlayManager.instance.State;
        //if (isGameoverFlag)
        //{
        //    if(gameState == GamePlayManager.GameState.Play)
        //    {
        //        transform.position = gameOverInitPos.position;
        //        transform.rotation = gameOverInitPos.rotation;
        //        agent.ResetPath();
        //        state = EnemyState.Walk;
        //        isGameoverFlag = false;
        //    }
        //}

        //if (playerController.isDead)
        //{
        //    isPlayerDead = true;//enemyのスクリプト用
        //    return;//playerが死んだら何もしない
        //}

        //targetDistance = (transform.position - currentTargetPos).magnitude;

        //if (targetDistance < 0.7f)//playerとenemyが近づいたら  ↓から
        //{
        //    state = EnemyState.Idle;
        //}

        //if(state == EnemyState.Idle)//EnemyState.Idleの間動きを止めてアニメーション
        //{
        //    currrentTime += Time.deltaTime;
        //    agent.ResetPath();
        //    if (currrentTime >= idleTime)//EnemyState.Idleの間動きを止めてアニメーション
        //    {
        //        state = EnemyState.Walk;
        //        currrentTime = 0;
        //        currentTargetPos = target.transform.position;//ここがないと敵が変な動きになる
        //    }//        
        //}

        //isActive = flashLight.LightOnFlag;
        //if (isActive)
        //{
        //    currentTargetPos = target.transform.position;
        //}

        //targetDistance = (transform.position - currentTargetPos).magnitude;

        //if (isActive && targetDistance < chaceDistance)
        //{
        //    agent.SetDestination(currentTargetPos);
        //    state = EnemyState.Found;
        //}



        //StateAnimation();

        //if (gameState == GamePlayManager.GameState.GameOver)
        //{
        //    //transform.position = gameOverInitPos.position;
        //    //transform.rotation = gameOverInitPos.rotation;
        //    //agent.ResetPath();
        //    //state = EnemyState.Walk;

        //    isGameoverFlag = true;
        //}
        #endregion

        Chase();
        
    }

    private void StateAnimation()
    {
        #region カンくんのコード
        //if (state == EnemyState.Walk)
        //{
        //    animator.SetBool("IsWalk", true);
        //    animator.SetBool("IsFind", false);
        //}
        //else if (state == EnemyState.Idle)
        //{
        //    animator.SetBool("IsWalk", false);
        //    animator.SetBool("IsFind", false);
        //}
        //else if(state == EnemyState.Found)
        //{
        //    animator.SetBool("IsFind", true);
        //}
        #endregion

    }

    private void Chase()
    {
        //敵とplayerの直線距離計算
        targetDistance = (target.transform.position - transform.position).magnitude;

        isLightOn = flashLight.LightOnFlag;

        //playerとの距離が設定したチェイス距離より短いかつライトが点いていたら
        if (targetDistance < chaseDistance && isLightOn)
        {
            isChase = true;
        }
        

        //チェイス中なら
        if (isChase)
        {
            animator.SetBool("isFound", true);

            //エージェントの目的地をplayerのポジションに
            agent.destination = target.transform.position;

            //playerとの距離がチェイス距離より遠くなったら(見失ったら)
            if (targetDistance > chaseDistance || !isLightOn)
            {
                isChase = false;
                animator.SetBool("isFound", false);

                float nextRouteDistance = 0;  //次のルートまでの距離
                int currentRouteIndex = 0;    //現在、何個目のルートか
                int nextRouteIndex = 0;       //次のルートになる番号


                //一番近いルートを探す
                foreach (var routeTarget in routeTargetArray)
                {
                    float currentRouteDistance = (routeTarget.transform.position - transform.position).magnitude;

                    if (nextRouteDistance == 0)
                    {
                        nextRouteDistance = currentRouteDistance;
                        nextRouteIndex = currentRouteIndex;
                    }

                    else
                    {
                        if (currentRouteDistance < nextRouteDistance)
                        {
                            nextRouteDistance = currentRouteDistance;
                            nextRouteIndex = currentRouteIndex;
                        }
                    }
                    currentRouteIndex++;
                }

                routeTargetNum = nextRouteIndex;
            }
        }

        else
        {
            agent.destination = routeTargetArray[routeTargetNum].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;

        if (obj.tag == "EnemyRoute")
        {
            if (routeTargetNum == routeTargetArray.Length - 1)
            {
                routeTargetNum = 0;
                return;
            }

            routeTargetNum++;
        }
    }
}
