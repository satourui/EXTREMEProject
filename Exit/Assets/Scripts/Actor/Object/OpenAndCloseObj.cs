using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseObj : MonoBehaviour
{
    Animator animator;
    public string parameterName;
    private bool isOpen;  //開いている状態ならtrue

    public string nextSelectMessage = "";  //選択後の選択文(例:閉める)

    public bool IsOpen { get => isOpen; set => isOpen = value; }

    void Start()
    {
        IsOpen = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoopAnimation()
    {
        if (!IsOpen)
        {
            animator.SetBool(parameterName, true);
            IsOpen = true;
        }

        else
        {
            animator.SetBool(parameterName, false);
            IsOpen = false;
        }

    }

    public void ChangeSelectMessage()
    {
        var po = GetComponent<PlacedObj>();

        string temporaryMessage = po.SelectMessage;
        po.SelectMessage = nextSelectMessage;
        nextSelectMessage = temporaryMessage;
    }
}
