using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.0f;　　//速度
    private Rigidbody rb;         //Rigidbody
    Vector3 velocity = Vector3.zero;  //移動量

    public Transform mainCamera;   //メインカメラ
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラーが接続されているかどうか調べる
        var controllerNames = Input.GetJoystickNames();
        //接続されていなかったらキーボードの入力を参考にする 　先頭の文字が空白なら～
        if(controllerNames[0] == "")
        {
            //一定間隔で移動
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            PlayerMove(x, z);
            PlayerRotate();
        }
        else　　//接続されていたらパッド操作に切り替える
        {
            //一定間隔で移動
            float x = Input.GetAxisRaw("L_Stick_Hori");
            float z = Input.GetAxisRaw("L_Stick_Verti");
            PlayerMove(x, z);
            PlayerRotate();
        }

        FlashLightSwicthing();
    }

    void PlayerMove(float x, float z)
    {
        //XとZへの力がどちらも0でないとき
        if (x != 0 || z != 0)
        {
            //移動
            velocity.Set(x, 0, z);
            velocity = velocity.normalized * speed * Time.deltaTime;
            velocity = transform.rotation * velocity;
            rb.MovePosition(transform.position + velocity);
        }
    }

    
    void PlayerRotate()
    {
        transform.rotation = Quaternion.Euler(0.0f, mainCamera.transform.localEulerAngles.y, 0.0f);
    }

    void FlashLightSwicthing()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject flashLight = GameObject.Find("FlashLight");

            if (flashLight == null)
                return;

            flashLight.GetComponent<FlashLightController>().LightSwitching();
        }
    }
}
