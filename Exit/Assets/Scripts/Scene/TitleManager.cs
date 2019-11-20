using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //「▷」←のUI画像(今はチェック画像)
    [SerializeField]
    public GameObject selectUI;
    RectTransform selectUIRect;
    

    public GameObject[] selectButtons = new GameObject[0];  //選択ボタンを入れる配列

    Vector3[] selectButtonsRectPositions;  //選択ボタンの位置配列

    int selectButtonsValue;  //選択ボタンの数

    int currentSelectButtonNum;  //現在の選択ボタンの番号

    private float beforeTrigger;


    private bool isGameStart;  //ゲームが始まるならtrue
    private bool isTutorial;  //チュートリアルならtrue
    private bool isGameEnd;  //ゲームをやめるならtrue


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        //選択ボタン関連の初期化
        selectUIRect = selectUI.GetComponent<RectTransform>();

        selectButtonsValue = selectButtons.Length;

        selectButtonsRectPositions = new Vector3[selectButtonsValue];

        for (int i = 0; i < selectButtonsValue; i++)
        {
            selectButtonsRectPositions[i] = selectButtons[i].GetComponent<RectTransform>().position;
        }

        currentSelectButtonNum = 0;

        beforeTrigger = 0;

        isGameStart = false;
        isTutorial = false;
        isGameEnd = false;
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
            selectUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Cursor.visible = false;
            selectUI.SetActive(true);
        }


        ////上キーとWを押したときにポジションを変えようとした跡地
        //if (!Cursor.visible)
        //{
        //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        SelectUI.transform.Translate(0,-1,0);
        //    }
        //}

        #endregion


        ChangeSelectUIPos();
        SelectNextScene();

        if (isGameStart)
        {
            GameStart();
        }

        if (isTutorial)
        {

        }

        if (isGameEnd)
        {
            GameEnd();
        }

        
    }

    void ChangeSelectUIPos()
    {
        float currentTrigger = Input.GetAxis("L_Stick_Verti");

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || (beforeTrigger == 0 && currentTrigger < 0))
        {
            if (currentSelectButtonNum == selectButtonsValue - 1)
            {
                currentSelectButtonNum = 0;
                beforeTrigger = currentTrigger;
                return;
            }

            currentSelectButtonNum++;
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || (beforeTrigger == 0 && currentTrigger > 0))
        {
            if (currentSelectButtonNum == 0)
            {
                currentSelectButtonNum = selectButtonsValue - 1;
                beforeTrigger = currentTrigger;
                return;
            }

            currentSelectButtonNum--;
        }
        
        beforeTrigger = currentTrigger;

        selectUIRect.position = selectButtonsRectPositions[currentSelectButtonNum] - new Vector3(30, 0, 0);
    }

    void SelectNextScene()
    {
        if (!isTutorial &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            if (currentSelectButtonNum == 0)
            {
                isGameStart = true;
            }

            else if (currentSelectButtonNum == 1)
            {
                //isTutorial = true;
            }

            else
            {
                isGameEnd = true;
            }
        }
    }

    public void GameStart()
    {
        //ステージ１にシーン切り替え
        SceneManager.LoadScene("Stage1");
        //SceneManager.LoadScene("TestNari");
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
