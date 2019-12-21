using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
    private EnemyMovement enemyMovement;

    [SerializeField]
    public LayerMask obstacleLayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerStay(Collider col)
    {

        //if(col.tag == "Player")
        //{
        //    Debug.DrawLine(transform.position +
        //        Vector3.up, col.transform.position + Vector3.up,
        //        Color.blue);
        //    //enemyMovement.isActive = true;

        //    if (!Physics.Linecast(transform.position + 
        //        Vector3.up, col.transform.position +
        //        Vector3.up, obstacleLayer))
        //    {
        //        enemyMovement.isActive = true;//ついてくるようにする
        //    }
        //}
    }

    private void OnTriggerExit(Collider col)
    {
        //if(col.tag == "Player")
        //{
        //    enemyMovement.isActive = false;//ついてこないようにする
        //}
    }
}
