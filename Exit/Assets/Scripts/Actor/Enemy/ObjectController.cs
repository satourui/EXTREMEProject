using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ObjectController : MonoBehaviour
{
    [SerializeField,Header("Target")]
    public Transform[] targets = null;
    [SerializeField,Header("判定範囲")]
    public float destinationThreshold = 0.0f;

    private NavMeshAgent navAgent = null;//Navi

    private int targetIndex = 0;//targetのIndex

    private EnemyMovement enemyMovement;//playerが死んだら用

    //private Vector3 CurretTargetPosition
    //{
    //    get
    //    {
    //        if (targets == null || targets.Length <= targetIndex)
    //        {
    //            return Vector3.zero;
    //        }

    //        return targets[targetIndex].position;
    //    }
    //}

    private void Start()
    {
        //navAgent = GetComponent<NavMeshAgent>();
        //navAgent.destination = CurretTargetPosition;

        //enemyMovement = GetComponent<EnemyMovement>();//playerが死んだら用
    }

    private void Update()
    {
        ////↓敵の動きを止めたいとき
        //if (enemyMovement.state == EnemyMovement.EnemyState.Idle)
        //{
        //    return;
        //}

        //if (navAgent.remainingDistance <= destinationThreshold)
        //{
        //    targetIndex = (targetIndex + 1) % targets.Length;

        //    navAgent.destination = CurretTargetPosition;
        //}
    }

} // class ObjectController
