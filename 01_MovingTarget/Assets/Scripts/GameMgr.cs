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


    public GameScene gameScene { get; set; }

    public GameInfo m_GameInfo = new GameInfo();


    public void Initialize()
    {
        m_GameInfo.Initialize();
    }


    public void OnUpdate(float fElasedTime)
    {
        m_GameInfo.OnUpdate(fElasedTime);        
    }
}
