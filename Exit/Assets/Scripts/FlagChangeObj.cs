using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeObj : MonoBehaviour
{
    [SerializeField]
    private string flagName;

    StageFlagManager flagManager;

    void Start()
    {
        flagManager = GameObject.FindGameObjectWithTag("FlagManager").GetComponent<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlagOn()
    {
        flagManager.FlagOn(flagName);
    }
}
