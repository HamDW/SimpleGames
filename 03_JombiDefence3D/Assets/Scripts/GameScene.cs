using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameUI m_GameUI;
    public HudUI m_HudUI;
    [SerializeField] bool m_isFPSMode = false;
    
    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();


    private void Awake()
    {
        GameMgr.Inst.Initialize();
        GameMgr.Inst.gameScene = this;

        GameMgr.Inst.m_GameInfo.IsFPS = m_isFPSMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_BattleFSM.Initialize(Call_ReadyEnter, Call_WaveEnter, Call_GameEnter, Call_ResultEnter);
    }


    void Call_ReadyEnter()
    {
        m_HudUI.StartReadyCount();
    }

    void Call_WaveEnter()  {  }

    void Call_GameEnter()
    {
        GameStart();
    }

    void Call_ResultEnter()
    {
        m_GameUI.SetGameResultEnter();
        m_HudUI.SetGameResultEnter();
    }

    public void GameStart()
    {
        GameMgr.Inst.InitGameStart();
        m_GameUI.Initialize();
        m_HudUI.Initialize();
    }

    public void ResetGame()
    {
        m_BattleFSM.SetReadyState();
    }

    public void ExitGame()
    {
        m_HudUI.OpenMenuDlg();
        m_BattleFSM.SetNoneState();
    }

    // Update is called once per frame
    void Update()
    {
        if( m_BattleFSM != null)
        {
            m_BattleFSM.OnUpdate();

            if( m_BattleFSM.IsGameState() )
            {
                GameMgr.Inst.OnUpdate(Time.deltaTime);
            }
        }
    }
}
