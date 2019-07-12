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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        flashLight = GameObject.Find("FlashLight").GetComponent<FlashLightController>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = (transform.position - target.position).magnitude;

        isActive = flashLight.LightOnFlag;
        if (isActive && targetDistance < chaceDistance)
        {
            agent.SetDestination(target.position * speed);
        }
    }
}
