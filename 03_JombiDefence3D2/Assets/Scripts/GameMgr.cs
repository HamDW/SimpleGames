using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr 
{
    private static GameMgr _inst = null;
    public  static GameMgr Inst
    {
        get {
            if (_inst == null)
                _inst = new GameMgr();

            return _inst;
        }
    }

    public bool IsInstalled { get; set; }
    public GameScene gameScene { get; set; }
    public GameInfo m_GameInfo = new GameInfo();

    public bool IsFPS
    {
        get { return m_GameInfo.m_IsFPS;  }
        set { m_GameInfo.m_IsFPS = value; }
    }

    public void Initialize()
    {
        IsInstalled = true;
    }

    public void InitGameStart()
    {
        m_GameInfo.Initialize();
    }

 


    public void OnUpdate(float fElasedTime)
    {
        m_GameInfo.OnUpdate(fElasedTime);        

        if( m_GameInfo.GetRemainTime() <= 0 )
        {
            gameScene.m_BattleFSM.SetResultState();
        }
    }


    public void AddScore( int nScore)
    {
        m_GameInfo.AddScore(nScore);
    }

    public int GetScore()
    {
        return m_GameInfo.m_nScore;
    }

}
