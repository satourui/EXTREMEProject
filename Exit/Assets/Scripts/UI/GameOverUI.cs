using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject gameoverObj = null;

    void Start()
    {

    }


    void Update()
    {
        var gameState = GamePlayManager.instance.State;

        //if (gameState == GamePlayManager.GameState.GameOver)
        //{
        //    if (!gameoverObj.activeSelf)
        //    {
        //        gameoverObj.SetActive(true);
        //    }
        //}

        //else
        //{
        //    if (gameoverObj.activeSelf)
        //    {
        //        gameoverObj.SetActive(false);
        //    }
        //}

        if (gameState != GamePlayManager.GameState.GameOver)
        {
            if (gameoverObj.activeSelf)
            {
                gameoverObj.SetActive(false);
            }
        }

    }

    public void RetryGame()
    {
        //GamePlayManager.instance.PlayerCreate();
        GamePlayManager.instance.PlayerRespawn();
        GamePlayManager.instance.StageInitialize();
        GamePlayManager.instance.PC.MainCamera.GetComponent<CameraController>().RoteCount = 0;

    }

    public void ReturnToTitle()
    {
        GamePlayManager.instance.GameEnd();
    }


    public void GameOver()
    {
        var gameState = GamePlayManager.instance.State;

        if (gameState == GamePlayManager.GameState.GameOver)
        {
            if (!gameoverObj.activeSelf)
            {
                gameoverObj.SetActive(true);
            }
        }
    }
}
