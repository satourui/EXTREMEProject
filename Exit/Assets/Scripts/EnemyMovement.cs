using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField,Header("速さ")]
    public float speed = 1;
    [SerializeField, Header("Playerのオブジェ")]
    public Transform target;
    [SerializeField, Header("ついていく？")]
    public bool isActive = false;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            agent.SetDestination(target.position * speed);
        }
    }
}
