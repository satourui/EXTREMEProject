using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance = null;

    public enum GameState
    {
        Play,
        Talk,
        Pause,
        GameOver,
        StageClear,
    }


    //UI関連
    public GameObject gamePlayUI = null;
    private PauseScript pauseScript;
    private GameObject pauseUIObj;

    //ステージ関連
    //Inspectorに複数データを表示するためのクラス
    [System.SerializableAttribute]
    public class StageFlagList
    {
        public List<string> flags = new List<string>();

        public StageFlagList(List<string> list)
        {
            flags = list;
        }
    }

    [SerializeField, Header("ステージ名リスト")]
    private List<string> stageNames = new List<string>();

    [SerializeField, Header("全ステージのフラグリスト")]
    private List<StageFlagList> stageFlagList = new List<StageFlagList>();  //ゲーム全体のステージフラグ管理リスト
    

    private Dictionary<string, bool> currentStageFlags = new Dictionary<string, bool>();  //フラグを管理するためのDictionary

    //player関連
    private GameObject player;
    PlayerController pc;

    [SerializeField, Header("各ステージのplayerの初期位置")]
    private List<Vector3> playerInitPosList = new List<Vector3>();

    private Vector3 playerSpawnPos = Vector3.zero;
    

    private bool isOption = false;  //オプション中ならtrue

    private int stageNum = 0;  //ステージ番号

    private GameState state = 0;

    private bool isStageClearFlag = false;  //ステージがクリアできる状態になったらtrue
    public int StageNum { get => stageNum; set => stageNum = value; }
    public Dictionary<string, bool> CurrentStageFlags { get => currentStageFlags; set => currentStageFlags = value; }
    public bool IsOption { get => isOption; set => isOption = value; }
    public GameState State { get => state; set => state = value; }
    public bool IsStageClearFlag { get => isStageClearFlag; set => isStageClearFlag = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);  //シーンを切り替えても消えない
        }
        else
        {
            Destroy(this.gameObject);
        }

        //マウスカーソルの削除
        Cursor.visible = false;

        pauseScript = gamePlayUI.GetComponent<PauseScript>();
        pauseUIObj = pauseScript.PauseUI;

        //RenderSettings.


        StageInitialize();

    }



    void StageInitialize()
    {
        //フラグ管理Dictionaryの初期化
        CurrentStageFlags = new Dictionary<string, bool>();

        foreach (var flagName in stageFlagList[StageNum].flags)
        {
            CurrentStageFlags.Add(flagName, false);
        }

        IsStageClearFlag = false;

        //IsPlayerDead = false;

        //IsPause = false;

        var playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
        player = Instantiate(playerPrefab, playerInitPosList[stageNum], Quaternion.identity);
        pc = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        

        if (State == GameState.Play)
        {
            pauseScript.PauseStart();
        }

        else if (State == GameState.Pause)
        {
            Pause();
        }

        else if (State == GameState.GameOver)
        {

        }

        else if (State == GameState.StageClear)
        {

        }
        
    }

    public void FlagOn(string flagName)
    {
        foreach (var name in stageFlagList[StageNum].flags)
        {
            if (name == flagName)
            {
                CurrentStageFlags[name] = true;
            }
        }
    }


    IEnumerator LoadNextStage()
    {
        yield return SceneManager.LoadSceneAsync(stageNames[StageNum]);
    }

    void NextStage()
    {

        StageNum++;
        StartCoroutine(LoadNextStage());
        StageInitialize();

    }

    void Pause()
    {
        if (!pauseUIObj.activeSelf)
        {
            pauseUIObj.SetActive(true);
            return;
        }



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gamePlayUI.GetComponent<PauseScript>().PauseEnd();
        }
    }

    void GameOver()
    {

    }

    void PlayerRespawn()
    {

    }

    public void GoalCheck()
    {
        //クリアフラグがtrueになったら
        if (IsStageClearFlag)
        {
            state = GameState.StageClear;
            gamePlayUI.GetComponent<TalkText>().TextClose();
            Debug.Log("クリア");
        }
    }
}
