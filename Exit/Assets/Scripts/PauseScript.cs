using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //ポーズした時に表示するUIのプレハブを入れてください
    private GameObject pauseUIPrefab;

    //ポーズUIのインスタンス
    private GameObject pauseUIInstance;
    //プレイヤーコントローラのNum変数用
    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            //インスタンス化されていないときは
            if(pauseUIInstance == null)
            {
                //ポーズプレハブをインスタンス化して表示
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            }
            else
            {
                //インスタンス化されているときに「1」を押したらポーズを破棄する
                Destroy(pauseUIInstance);
            }
        }
    }
}
