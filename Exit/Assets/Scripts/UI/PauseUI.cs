using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUIObj = null;

    private PlayerController playerController;
    private CameraController cameraController;
    private float sensitivity;

    public GameObject PauseUIObj { get => pauseUIObj; set => pauseUIObj = value; }


    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 30;
        playerController = GamePlayManager.instance.Player.GetComponent<PlayerController>();
        //cameraController = playerController.MainCamera.gameObject.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraController == null)
        {
            cameraController = playerController.MainCamera.gameObject.GetComponent<CameraController>();
        }

        //cameraController.Sensitivity = sensitivity;
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
        pauseUIObj.SetActive(false);
        Cursor.visible = false;
        GamePlayManager.instance.State = GamePlayManager.GameState.Play;
    }

    /// <summary>
    /// ポーズ画面からオプション画面に切り替える処理群
    /// </summary>
    public void ChangeOption()
    {


        cameraController.Sensitivity = sensitivity;
    }
    /// <summary>
    /// 渡された数値をある範囲から別の範囲に変換
    /// </summary>
    /// <param name="value">変換する入力値</param>
    /// <param name="start1">現在の範囲の下限</param>
    /// <param name="stop1">現在の範囲の上限</param>
    /// <param name="start2">変換する範囲の下限</param>
    /// <param name="stop2">変換する範囲の上限</param>
    /// <returns>変換後の値</returns>
    float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
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
