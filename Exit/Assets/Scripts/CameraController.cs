﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  //カメラのターゲット

    private Vector3 offset;  //カメラとターゲットの差分

    public float sensitivity = 0;  //カメラ感度

    public float JHori = 0;     //パッドの速さ調整用
    public float JVerti = 0;

    //float angle;

    Vector3 roteuler;  //オイラー角

    public float minRotateX = 0;
    public float maxRotateX = 0;

    private string[] contllorerName;

    void Start()
    {
        offset = transform.position - target.transform.position;

        //angle = transform.localEulerAngles.x;
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);  //親オブジェクトのオイラー角取得
    }

    // Update is called once per frame
    void Update()
    {
        contllorerName = Input.GetJoystickNames();
        CameraMouseRotation();

        transform.position = target.transform.position + offset;
    }

    private void CameraMouseRotation()
    {
        if (contllorerName[0] == "")
        {
            float rotateX = Input.GetAxis("Mouse X");
            float rotateY = Input.GetAxis("Mouse Y");
            //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
            //transform.RotateAround(transform.position, transform.right, angle.y);
            //transform.RotateAround(transform.position, Vector3.up, angle.x);

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - rotateY, minRotateX, maxRotateX), roteuler.y + rotateX, 0f);  //Mathf.Clampで角度制限
            transform.localEulerAngles = roteuler;
        }
        else
        {
            float rotateX = Input.GetAxis("R_Stick_Hori");
            float rotateY = Input.GetAxis("R_Stick_Verti");

            //速度変更の仕方が不明なので仮仕様
            rotateX = rotateX * JHori;
            rotateY = rotateY * JVerti;
            //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
            //transform.RotateAround(transform.position, transform.right, angle.y);
            //transform.RotateAround(transform.position, Vector3.up, angle.x);

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - rotateY, minRotateX, maxRotateX), roteuler.y + rotateX, 0f);  //Mathf.Clampで角度制限
            transform.localEulerAngles = roteuler;
        }


    }
}
