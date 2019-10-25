using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;  //カメラのターゲット

    [Header("playerとカメラの位置の差分")]
    public Vector3 offset = Vector3.zero;  //カメラとターゲットの差分

    public float sensitivity = 0;  //カメラ感度

    public float JHori = 0;     //パッドの速さ調整用
    public float JVerti = 0;

    //float angle;

    Vector3 roteuler;  //オイラー角

    public float minRotateX = 0;
    public float maxRotateX = 0;

    //private string[] controllerName;

    //[SerializeField]
    //public GameObject gameManager;
    //[SerializeField]
    //public PauseScript pauseScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.transform.position + offset;
        //angle = transform.localEulerAngles.x;
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);  //親オブジェクトのオイラー角取得

        //pauseScript = gameManager.GetComponent<PauseScript>();
    }
    

    void Update()
    {
        //controllerName = Input.GetJoystickNames();
        //if (target.GetComponent<PlayerController>().State == PlayerController.PlayerState.Normal)
        //if (pauseScript.GetPlayerflag())

        if(GamePlayManager.instance.State==GamePlayManager.GameState.Play)
        {
            CameraMouseRotation();
        }

        transform.position = player.transform.position + offset;
    }

    private void CameraMouseRotation()
    {

        //Unityを起動してから1度もパッドを接続しないで実行すると
        //配列の大きさが0になりエラーになるのでとりあえずreturnで返す
        //if (controllerName.Length == 0)
        //    return;

        //マウス処理
        float mouse_RotateX = Input.GetAxis("Mouse X");
        float mouse_RotateY = Input.GetAxis("Mouse Y");
        //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
        //transform.RotateAround(transform.position, transform.right, angle.y);
        //transform.RotateAround(transform.position, Vector3.up, angle.x);
        roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouse_RotateY, minRotateX, maxRotateX), roteuler.y + mouse_RotateX, 0f);  //Mathf.Clampで角度制限
        transform.localEulerAngles = roteuler;



        //パッド処理
        float pad_RotateX = Input.GetAxis("R_Stick_Hori");
        float pad_RotateY = Input.GetAxis("R_Stick_Verti");
        //速度変更の仕方が不明なので仮仕様
        pad_RotateX = pad_RotateX * JHori;
        pad_RotateY = pad_RotateY * JVerti;
        //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
        //transform.RotateAround(transform.position, transform.right, angle.y);
        //transform.RotateAround(transform.position, Vector3.up, angle.x);
        roteuler = new Vector3(Mathf.Clamp(roteuler.x - pad_RotateY, minRotateX, maxRotateX), roteuler.y + pad_RotateX, 0f);  //Mathf.Clampで角度制限
        transform.localEulerAngles = roteuler;
    }
}
