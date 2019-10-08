using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    float count;　//秒数カウント用
    public bool isClickflag; //クリックした？

    [SerializeField] GameObject TitleUI;  //TitlePanel
    [SerializeField] GameObject TutorialUI; //TutorialPanel
    [SerializeField] GameObject BlackUI;  //暗転用Panel 

    // Start is called before the first frame update
    void Start()
    {
        isClickflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //クリックされたらカウント開始
        if (isClickflag)
        {
            count = count + 0.1f;
        }
        if(count > 3.0f)
        {
            //一定時間たったら暗転パネルを非表示にする
            BlackUI.SetActive(false);
            isClickflag = false;
            count = 0.0f;
        }
    }

    public void OnRetry()
    {
        //ステージ１にシーン切り替え
        SceneManager.LoadScene("Stage1");
    }

    public void Tutorial()
    {
        //パネルUIの切り替え
        isClickflag = true;
        TitleUI.SetActive(false);
        TutorialUI.SetActive(true);
        BlackUI.SetActive(true);
    }

    public void Return()
    {
        //チュートリアルからタイトルに戻るための処理
        isClickflag = true;
        TitleUI.SetActive(true);
        TutorialUI.SetActive(false);
        BlackUI.SetActive(true);
    }
}
