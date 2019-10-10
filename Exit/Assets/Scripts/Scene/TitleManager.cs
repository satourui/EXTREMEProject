using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    //「▷」←のUI画像(今はチェック画像)
    [SerializeField]
    public GameObject SelectUI;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {
        #region カーソルオンオフに関する処理(死ぬほどコードが汚く見辛いので訂正できるならよろしくお願いします)
        
        //マウスカーソルのオン キーボードを押したときにオフにする
        if (Input.GetAxis("Mouse X") > 0.1f || Input.GetAxis("Mouse X") < -0.1f
            || Input.GetAxis("Mouse Y") > 0.1f || Input.GetAxis("Mouse Y") < -0.1f)
        {
            Cursor.visible = true;
            SelectUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            Cursor.visible = false;
            SelectUI.SetActive(true);
        }


        //上キーとWを押したときにポジションを変えようとした跡地
        if (!Cursor.visible)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectUI.transform.Translate(0,-1,0);
            }
        }
        
        #endregion
    }
}
