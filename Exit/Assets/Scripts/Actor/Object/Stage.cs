using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //[SerializeField, Header("このステージの敵")]
    //private GameObject enemy = null;

    //[SerializeField, Header("敵の順路配列")]
    //private EnemyRoutes[] enemyRoutes = new EnemyRoutes[0];

    //Inspectorに複数データを表示するためのクラス
    //[System.SerializableAttribute]
    //public class EnemyRoutes
    //{
    //    public GameObject[] routes = new GameObject[0];

    //    public EnemyRoutes(GameObject[] route)
    //    {
    //        routes = route;
    //    }
    //}

    [SerializeField]
    private List<string> stageFlagsList = new List<string>();

    [SerializeField]
    private List<GameObject> initItemList = new List<GameObject>();

    [SerializeField]
    private Vector3 playerSpawnPos = Vector3.zero;

    [SerializeField]
    private float playerSpawnRotate = 0;

    public List<string> StageFlagsList { get => stageFlagsList; }
    public List<GameObject> InitItemList { get => initItemList; set => initItemList = value; }
    public Vector3 PlayerSpawnPos { get => playerSpawnPos; set => playerSpawnPos = value; }
    public float PlayerSpawnRotate { get => playerSpawnRotate; set => playerSpawnRotate = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
