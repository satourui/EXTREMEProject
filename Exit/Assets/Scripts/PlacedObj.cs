using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObj : MonoBehaviour
{
    [SerializeField]
    private string[] messages;
    [SerializeField]
    private string selectMessage;


    public string[] Messages { get => messages; set => messages = value; }
    public string SelectMessage { get => selectMessage; set => selectMessage = value; }

    void Start()
    {

    }

    
    void Update()
    {

    }
    
}
