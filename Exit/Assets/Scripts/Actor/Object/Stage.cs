using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
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
