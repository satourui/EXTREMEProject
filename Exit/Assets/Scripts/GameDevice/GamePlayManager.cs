using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance = null;


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

    [SerializeField,Header("ステージ名リスト")]
    private List<string> stageNames = new List<string>();

    [SerializeField,Header("全ステージのフラグリスト")]
    private List<StageFlagList> stageFlagList = new List<StageFlagList>();  //ゲーム全体のステージフラグ管理リスト

    [SerializeField,Header("各ステージのクリア条件フラグ名")]
    private List<string> clearFlagList = new List<string>();  //各ステージでクリア条件になるフラグの名前

    public Dictionary<string, bool> currentStageFlags = new Dictionary<string, bool>();  //フラグを管理するためのDictionary

    //player関連
    private GameObject player;

    [SerializeField, Header("各ステージのplayerの初期位置")]
    private List<Vector3> playerInitPosList = new List<Vector3>();

    private Vector3 playerSpawnPos = Vector3.zero;


    private bool isStageClear = false;  //ステージをクリアしたら
    
    private bool isPlayerDead = false;  //playerが死んでいたらtrue
    
    private bool isPause = false;  //ポーズ中ならtrue

    private int stageNum = 0;  //ステージ番号

    public bool IsStageClear { get => isStageClear; set => isStageClear = value; }
    public bool IsPlayerDead { get => isPlayerDead; set => isPlayerDead = value; }
    public bool IsPause { get => isPause; set => isPause = value; }
    public int StageNum { get => stageNum; set => stageNum = value; }

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

        StageInitialize();

    }

    

    void StageInitialize()
    {
        //フラグ管理Dictionaryの初期化
        currentStageFlags = new Dictionary<string, bool>();

        foreach (var flagName in stageFlagList[StageNum].flags)
        {
            currentStageFlags.Add(flagName, false);
        }


        IsStageClear = false;

        IsPlayerDead = false;

        IsPause = false;

        var playerPrefab = (GameObject)Resources.Load("Prefabs/Player");
        player = Instantiate(playerPrefab, playerInitPosList[stageNum], Quaternion.identity);
    }

    void Update()
    {
        //プレイヤーが死んだら
        if (IsPlayerDead)
        {
            GameOver();
        }

        //ポーズ中なら
        if (IsPause)
        {
            Pause();
        }

        //クリアフラグがtrueになったら
        if (currentStageFlags[clearFlagList[stageNum]])
        {
            IsStageClear = true;
        }


        //クリアしたら
        if (IsStageClear)
        {
            NextStage();
        }
    }

    public void FlagOn(string flagName)
    {
        foreach (var name in stageFlagList[StageNum].flags)
        {
            if (name == flagName)
            {
                currentStageFlags[name] = true;
            }
        }
    }

    
    IEnumerator LoadNextStage()
    {
        yield return SceneManager.LoadSceneAsync(stageNames[StageNum]);
    }

    void NextStage()
    {
        if (IsStageClear)
        {
            StageNum++;
            StartCoroutine(LoadNextStage());
            StageInitialize();
        }
    }

    void Pause()
    {

    }

    void GameOver()
    {

    }

    void PlayerRespawn()
    {

    }
}
