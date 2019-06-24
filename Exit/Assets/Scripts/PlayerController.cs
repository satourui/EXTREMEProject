using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float speed = 3.0f;
    private float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMove();
    }

    void CalculateMove()
    {
        float horizontallnput = Input.GetAxis("Horizontal");
        float verticallnput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontallnput, 0, verticallnput);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        controller.Move(velocity * Time.deltaTime);
    }
}
