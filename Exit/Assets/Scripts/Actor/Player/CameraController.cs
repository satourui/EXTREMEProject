using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject playerObj;
    private Transform playerTransform;  //カメラのターゲット

    [Header("playerとカメラの位置の差分")]
    public Vector3 offset = Vector3.zero;  //カメラとターゲットの差分

    [SerializeField]
    private float sensitivity = 0;  //カメラ感度

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

    //カン
    public float lerpSpeed = 0.1f;

    private Vector3 playerPos;
    private Vector3 pVec3;
    private PlayerController playerCont;

    [Header("揺れるスピード")]
    public float rubSpeed = 15f;

    [Header("揺れ幅")]
    public float rubHeight = 20f;

    public float Sensitivity { get => sensitivity; set => sensitivity = value; }

    void Start()
    {
        playerObj = GamePlayManager.instance.Player;
        playerTransform = playerObj.transform;
        transform.position = playerTransform.transform.position + offset;
        //angle = transform.localEulerAngles.x;
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);  //親オブジェクトのオイラー角取得

        //pauseScript = gameManager.GetComponent<PauseScript>();

        //かん
        playerPos = Vector3.zero;
        pVec3 = Vector3.zero;

        pVec3 = playerTransform.transform.position + offset;

        playerCont = playerObj.GetComponent<PlayerController>();
    }
    

    void Update()
    {
        //controllerName = Input.GetJoystickNames();
        //if (target.GetComponent<PlayerController>().State == PlayerController.PlayerState.Normal)
        //if (pauseScript.GetPlayerflag())

        if (GamePlayManager.instance.State == GamePlayManager.GameState.Play)
        {
            CameraMouseRotation();


            //かん↓  慣性かけてる
            pVec3 = playerTransform.transform.position + offset;

            //縦の動き                                                          ↓スピード↓縦幅(大きくすると縮まる)

            if (playerCont.isWalk)
            {
                playerPos = new Vector3(playerTransform.position.x, Mathf.Sin(Time.time * (1f * rubSpeed)) / (1f * rubHeight) + (playerTransform.position.y + offset.y),
                        playerTransform.position.z);
            }
            else if (!playerCont.isWalk)
            {
                playerPos = pVec3;
            }

            transform.position = playerPos;
            //かん↑
        }
    }

    private void CameraMouseRotation()
    {

        //Unityを起動してから1度もパッドを接続しないで実行すると
        //配列の大きさが0になりエラーになるのでとりあえずreturnで返す
        //if (controllerName.Length == 0)
        //    return;

        //マウス処理
        float mouse_RotateX = Input.GetAxis("Mouse X") * sensitivity / 10;
        float mouse_RotateY = Input.GetAxis("Mouse Y") * sensitivity / 10;
        //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
        //transform.RotateAround(transform.position, transform.right, angle.y);
        //transform.RotateAround(transform.position, Vector3.up, angle.x);
        roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouse_RotateY, minRotateX, maxRotateX), (roteuler.y + mouse_RotateX), 0f);  //Mathf.Clampで角度制限
        transform.localEulerAngles = roteuler;



        //パッド処理
        float pad_RotateX = Input.GetAxis("R_Stick_Hori")*sensitivity/100;
        float pad_RotateY = Input.GetAxis("R_Stick_Verti")*sensitivity/100;
        //速度変更の仕方が不明なので仮仕様
        pad_RotateX = pad_RotateX * JHori;
        pad_RotateY = pad_RotateY * JVerti;
        //Vector3 angle = new Vector3(rotateX * sensitivity, -rotateY * sensitivity, 0);
        //transform.RotateAround(transform.position, transform.right, angle.y);
        //transform.RotateAround(transform.position, Vector3.up, angle.x);
        roteuler = new Vector3(Mathf.Clamp(roteuler.x - pad_RotateY, minRotateX, maxRotateX), (roteuler.y + pad_RotateX), 0f);  //Mathf.Clampで角度制限
        transform.localEulerAngles = roteuler;
        
    }

    public void RotateInitialize(float angle)
    {
        //transform.localEulerAngles = Vector3.zero;
        //roteuler.y = angle;
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        //transform.localEulerAngles = new Vector3(0.0f, angle, 0.0f);
        //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
