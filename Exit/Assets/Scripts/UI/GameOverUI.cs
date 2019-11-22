using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameoverObj = null;

    void Start()
    {
        
    }
    

    void Update()
    {
        var gameState = GamePlayManager.instance.State;

        if (gameState == GamePlayManager.GameState.GameOver)
        {
            if (!gameoverObj.activeSelf)
            {
                gameoverObj.SetActive(true);
            }
        }

        else
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
        
    }

    public void ReturnToTitle()
    {
        GamePlayManager.instance.GameEnd();
    }
}
