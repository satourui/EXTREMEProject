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

    private Vector3 CurretTargetPosition
    {
        get
        {
            if (targets == null || targets.Length <= targetIndex)
            {
                return Vector3.zero;
            }

            return targets[targetIndex].position;
        }
    }

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = CurretTargetPosition;
    }

    private void Update()
    {
        if (navAgent.remainingDistance <= destinationThreshold)
        {
            targetIndex = (targetIndex + 1) % targets.Length;

            navAgent.destination = CurretTargetPosition;
        }
    }

} // class ObjectController
