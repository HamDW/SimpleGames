using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameUI m_GameUI;
    public HudUI m_HudUI;
    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();

    private void Awake()
    {
        GameMgr.Inst.Initialize();
        GameMgr.Inst.gameScene = this;   
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
        m_GameUI.Initialize();
        m_HudUI.Initialize();
        Cursor.visible = false;
    }

    void Call_ResultEnter()
    {
        Cursor.visible = true;
        m_GameUI.SetGameReuslt();
    }

    public void ResetGame()
    {
        m_GameUI.Initialize();
        m_HudUI.Initialize();
    }

    public void ExitGame()
    {

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
