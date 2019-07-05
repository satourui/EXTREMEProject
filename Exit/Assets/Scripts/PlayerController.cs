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


        //Unityを起動してから1度もパッドを接続しないで実行すると
        //配列の大きさが0になりエラーになるのでとりあえずreturnで返す
        if (controllerNames.Length == 0)
            return;



        //接続されていなかったらキーボードの入力を参考にする 　先頭の文字が空白なら～
        //キーボード操作
        if (controllerNames[0] == "")
        {
            //一定間隔で移動
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            PlayerMove(x, z);
            PlayerRotate();


            if (Input.GetKeyDown(KeyCode.Z))
            {
                FlashLightSwicthing();
            }
        }

        //パッド操作
        else　　//接続されていたらパッド操作に切り替える
        {
            //一定間隔で移動
            float x = Input.GetAxisRaw("L_Stick_Hori");
            float z = Input.GetAxisRaw("L_Stick_Verti");
            PlayerMove(x, z);
            PlayerRotate();

            //Xボタン
            //if (Input.GetKeyDown(KeyCode.JoystickButton2))
            //{
                FlashLightSwicthing();
            //}

            //float switchingNum = Input.GetAxisRaw("LightSwitch");
            //if (switchingNum > 0)

        }

        
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
        GameObject flashLight = GameObject.Find("FlashLight");

        if (flashLight == null)
            return;

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
            flashLight.GetComponent<FlashLightController>().LightSwitching();


        float switchingNum = Input.GetAxisRaw("LightSwitch");
        if (switchingNum > 0)
            flashLight.GetComponent<FlashLightController>().SwitchOn();

        else if(switchingNum<0)
            flashLight.GetComponent<FlashLightController>().SwitchOff();
    }
}
