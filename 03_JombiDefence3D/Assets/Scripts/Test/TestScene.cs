using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    public TestGameUI m_GameUI;
    public TestHudUI m_HudUI;
    [SerializeField] bool m_isFPSMode = false;

    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();

    public FPSCamera m_kFpsCamera = null;


    private void Awake()
    {
        //GameMgr.Inst.Initialize();
        //GameMgr.Inst.gameScene = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_BattleFSM.Initialize(Call_ReadyEnter, Call_WaveEnter, Call_GameEnter, Call_ResultEnter);

        if (m_kFpsCamera != null)
            m_kFpsCamera.m_IsFPS = GameMgr.Inst.IsFPS;

        m_BattleFSM.SetReadyState();
    }


    void Call_ReadyEnter()
    {
        //m_HudUI.StartReadyCount();
        m_BattleFSM.SetGameState();
    }

    void Call_WaveEnter() { }

    void Call_GameEnter()
    {
        GameStart();
    }

    void Call_ResultEnter()
    {
        GameMgr.Inst.IsFPS = false;
        m_kFpsCamera.m_IsFPS = false;

        //m_GameUI.SetGameResultEnter();
        //m_HudUI.SetGameResultEnter();
    }

    public void GameStart()
    {
        GameMgr.Inst.IsFPS = m_isFPSMode;
        m_kFpsCamera.m_IsFPS = m_isFPSMode;

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
        //m_HudUI.OpenMenuDlg();
        //m_BattleFSM.SetNoneState();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BattleFSM != null)
        {
            m_BattleFSM.OnUpdate();

            if (m_BattleFSM.IsGameState())
            {
                GameMgr.Inst.OnUpdate(Time.deltaTime);
            }
        }
    }
}
