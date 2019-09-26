using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChangeObj : MonoBehaviour
{
    [SerializeField,Header("リンクするフラグの名前")]
    private string flagName;

    [SerializeField, Header("フラグを変更してお役御免ならtrue")]
    private bool isDead;

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
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
