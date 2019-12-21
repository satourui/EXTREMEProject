using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 違うシーンへ移行する
    /// </summary>
    void NextScene()
    {
        GamePlayManager.instance.GameEndInstant();
        SceneManager.LoadScene("Ending");
        
    }
}
