using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI = null;

    public GameObject PauseUI { get => pauseUI; set => pauseUI = value; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseStart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GamePlayManager.instance.State = GamePlayManager.GameState.Pause;
            Cursor.visible = true;
        }
    }

    public void PauseEnd()
    {
        pauseUI.SetActive(false);
        Cursor.visible = false;
        GamePlayManager.instance.State = GamePlayManager.GameState.Play;
    }

    /// <summary>
    /// ポーズ画面からオプション画面に切り替える処理群
    /// </summary>
    public void ChangeOption()
    {

    }

    /// <summary>
    /// オプション画面からポーズ画面に切り替える処理群
    /// </summary>
    public void ChangePause()
    {

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
