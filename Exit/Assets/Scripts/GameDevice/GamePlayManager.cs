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
    private TalkTextUI talkText;
    private PauseUI pauseScript;
    private GameObject pauseUIObj;
    

    //ステージ関連
    [SerializeField]
    private List<string> stagePrefabNameList = new List<string>();

    [SerializeField]
    private List<Vector3> stagePrefabPosList = new List<Vector3>();

    //[SerializeField,Header("中身確認用")]
    private GameObject[] stageObjctArray;

    private string stageFolderName;
    
    private Dictionary<string, bool> currentStageFlags = new Dictionary<string, bool>();  //フラグを管理するためのDictionary

    private GameObject currentStageObj;

    private Stage currentStageScript;

    private int stageValue;  //ステージ数


    //player関連
    private GameObject player;
    private PlayerController pc;

    [SerializeField,Header("一番最初のスタート位置")]
    private Vector3 playerStartPos = Vector3.zero;
    
    private Vector3 playerSpawnPos ;

    private float playerSpawnRote;

    [SerializeField,Header("ゲーム開始時のメッセージ")]
    private string[] startMessage = new string[0];

    private bool isStartMessage; //スタートメッセージが読まれたらtrue


    private bool isOption = false;  //オプション中ならtrue

    private int stageNum = 0;  //ステージ番号

    private GameState state = 0;

    private bool isStageClearFlag = false;  //ステージがクリアできる状態になったらtrue
    public int StageNum { get => stageNum; set => stageNum = value; }
    public Dictionary<string, bool> CurrentStageFlags { get => currentStageFlags; set => currentStageFlags = value; }
    public bool IsOption { get => isOption; set => isOption = value; }
    public GameState State { get => state; set => state = value; }
    public bool IsStageClearFlag { get => isStageClearFlag; set => isStageClearFlag = value; }
    //public GameObject Player { get => player; }
    public TalkTextUI TalkText { get => talkText; set => talkText = value; }
    public PlayerController PC { get => pc; set => pc = value; }
    public GameObject Player { get => player; set => player = value; }

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

        talkText=gamePlayUI.GetComponent<TalkTextUI>();
        pauseScript = gamePlayUI.GetComponent<PauseUI>();
        pauseUIObj = pauseScript.PauseUIObj;

        stageFolderName = "Prefabs/";

        stageObjctArray = new GameObject[stagePrefabNameList.Count];

        for (int i = 0; i < stagePrefabNameList.Count; i++)
        {
            var stage = (GameObject)Resources.Load(stageFolderName + stagePrefabNameList[i]);
            stageObjctArray[i] = Instantiate(stage, stagePrefabPosList[i], Quaternion.identity);
        }

        stageValue = stagePrefabNameList.Count;

        playerSpawnPos = Vector3.zero;
        playerSpawnRote = 0;

        PlayerCreate();
        
        StageInitialize();
        //PlayerInitialize();

        playerSpawnPos = currentStageScript.PlayerSpawnPos;
        player.transform.position = playerSpawnPos;

        //1番最初の位置のみここで指定
        player.transform.position = playerStartPos;

        state = GameState.Talk;
        isStartMessage = false;
        
    }



    public void StageInitialize()
    {
        //ステージオブジェクトの初期化
        var stagePrefab = (GameObject)Resources.Load(stageFolderName + stagePrefabNameList[stageNum]);
        //if (currentStageObj != null)
        //{
        //    Destroy(currentStageObj);
        //}
        //currentStageObj = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
        //currentStageScript = currentStageObj.GetComponent<Stage>();

        if (stageObjctArray[StageNum] != null)
        {
            Destroy(stageObjctArray[stageNum]);
        }
        stageObjctArray[StageNum] = Instantiate(stagePrefab, stagePrefabPosList[StageNum], Quaternion.identity);
        currentStageScript = stageObjctArray[StageNum].GetComponent<Stage>();

        //フラグ管理Dictionaryの初期化
        CurrentStageFlags = new Dictionary<string, bool>();

        foreach (var flagName in currentStageScript.StageFlagsList)
        {
            CurrentStageFlags.Add(flagName, false);
        }

        //アイテムリストの初期化
        pc.ItemList.Clear();
        
        for (int i = 0; i < currentStageScript.InitItemList.Count; i++)
        {
            var itemObj = currentStageScript.InitItemList[i];
            itemObj.GetComponent<ItemObj>().Initialize();
            pc.ItemList.Add(itemObj);
        }
        gamePlayUI.GetComponent<InventotyUI>().ItemList = pc.ItemList;

        state = GameState.Play;

        IsStageClearFlag = false;

        
        
        //player.transform.rotation = Quaternion.identity;
    }

    public void PlayerCreate()
    {
        if (player != null)
        {
            return;
        }

        var playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
        player = Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
        pc = player.GetComponent<PlayerController>();
    }

    public void PlayerRespawn()
    {
        playerSpawnPos = currentStageScript.PlayerSpawnPos;

        playerSpawnRote = currentStageScript.PlayerSpawnRotate;

        player.transform.position = playerSpawnPos;

        //player.transform.localEulerAngles = new Vector3(0.0f, playerSpawnRote, 0.0f);

        pc.Initialize();

        pc.MainCamera.GetComponent<CameraController>().RotateInitialize(playerSpawnRote);
        
    }

    void Update()
    {
        if (!isStartMessage)
        {
            talkText.IsTalk = true;
            talkText.MainMessages = startMessage;
            isStartMessage = true;
        }

        if (State == GameState.Play)
        {
            pauseScript.PauseStart();

            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }

        else if (State == GameState.Pause)
        {
            Pause();
        }

        else if (State == GameState.GameOver)
        {
            Cursor.visible = true;
        }

        else if (State == GameState.StageClear)
        {
            StageClear();
        }
        
    }

    public void FlagOn(string flagName)
    {
        foreach (var name in currentStageScript.StageFlagsList)
        {
            if (name == flagName)
            {
                CurrentStageFlags[name] = true;
            }
        }
    }


    //IEnumerator LoadNextStage()
    //{
    //    yield return SceneManager.LoadSceneAsync(stageNames[StageNum]);
    //}

    void NextStage()
    {

        StageNum++;
        //StartCoroutine(LoadNextStage());

        if (StageNum > stageValue - 1)
        {
            GameEnd();
            return;
        }

        StageInitialize();
        State = GameState.Play;

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
            gamePlayUI.GetComponent<PauseUI>().PauseEnd();
        }
    }

    void GameOver()
    {

    }

    

    public void GoalCheck()
    {
        //クリアフラグがtrueになったら
        if (IsStageClearFlag)
        {
            state = GameState.StageClear;
            gamePlayUI.GetComponent<TalkTextUI>().TextClose();
        }
    }

    void StageClear()
    {
        NextStage();
    }

    public void GameEnd()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        SceneManager.LoadScene("Title");
    }

    public void GameEndInstant()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}
