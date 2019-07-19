using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemDropObj : MonoBehaviour
{
    [SerializeField]
    private string dropItemName;

    StageFlagManager flagManager;

    void Start()
    {
        flagManager = GameObject.FindGameObjectWithTag("FlagManager").GetComponent<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemGet()
    {
        
    }
}
