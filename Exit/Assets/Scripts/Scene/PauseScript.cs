using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //ポーズした時に表示するUIのプレハブを入れてください
    private GameObject pauseUIPrefab;
    [SerializeField]
    //オプション画面UIのプレハブを入れてください
    private GameObject optionUIPrefab;

    //プレイヤーコントローラのNum変数用
    private PlayerController pc;
    //ポーズ表示非表示フラグ
    private bool Changeflag;

    // Start is called before the first frame update
    void Start()
    {
        Changeflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (Changeflag)
            {
                pauseUIPrefab.SetActive(false);
                optionUIPrefab.SetActive(false);
                Changeflag = false;
            }
            else if (!Changeflag)
            {
                pauseUIPrefab.SetActive(true);
                Changeflag = true;
            }
        }

    }

    public void Return()
    {
        Changeflag = false;
        pauseUIPrefab.SetActive(false);
        optionUIPrefab.SetActive(false);
    }

    /// <summary>
    /// ポーズ画面からオプション画面に切り替える処理群
    /// </summary>
    public void OptionChange()
    {
        pauseUIPrefab.SetActive(false);
        optionUIPrefab.SetActive(true);
    }

    /// <summary>
    /// オプション画面からポーズ画面に切り替える処理群
    /// </summary>
    public void PauseChange()
    {
        pauseUIPrefab.SetActive(true);
        optionUIPrefab.SetActive(false);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE 
        UnityEngine.Application.Quit();   
#endif  
    }
}
