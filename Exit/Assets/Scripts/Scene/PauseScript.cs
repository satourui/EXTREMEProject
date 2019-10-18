using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField,Header("ポーズ画面に表示するUIを入れてください")]
    private GameObject pauseUIPrefab;
    [SerializeField,Header("オプション画面UIを入れてください")]
    private GameObject optionUIPrefab;
    
    //ポーズ表示非表示フラグ
    private bool Changeflag;
    private bool playerAciveflag;

    // Start is called before the first frame update
    void Start()
    {
        Changeflag = false;
        playerAciveflag = true;
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
                playerAciveflag = true;
            }
            else if (!Changeflag)
            {
                pauseUIPrefab.SetActive(true);
                Changeflag = true;
                playerAciveflag = false;
            }
        }

    }

    public void Return()
    {
        Changeflag = false;
        playerAciveflag = true;
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

    public bool GetPlayerflag()
    {
        return playerAciveflag;
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
