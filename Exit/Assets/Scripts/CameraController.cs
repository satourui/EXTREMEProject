using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  //カメラのターゲット

    private Vector3 offset;  //カメラとターゲットの差分

    public float sensitivity = 0;  //カメラ感度


    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMouseRotation();

        transform.position = target.transform.position + offset;
    }

    private void CameraMouseRotation()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * sensitivity, -Input.GetAxis("Mouse Y") * sensitivity, 0);
        transform.RotateAround(transform.position, Vector3.up, angle.x);
        transform.RotateAround(transform.position, transform.right, angle.y);
    }
}
