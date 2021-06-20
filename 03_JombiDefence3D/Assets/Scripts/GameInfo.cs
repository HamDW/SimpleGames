using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public const int DATTACK_VAULE = 100;    // 총알 공격력
                                             // 
    public  float m_fMaxTime = 180.0f;       // 게임 전체 시간
    private float m_fCurTime = 0;           // 게임 경과 시간
    public int m_nScore = 0;                // 점수


    public void Initialize()
    {
        m_fCurTime = 0;
        m_nScore = 0;
    }

    public float durationTime    {
        get { return m_fCurTime;  }
    }
    // 남은 시간
    public float GetRemainTime()
    {
        return m_fMaxTime - m_fCurTime;
    }

    public void OnUpdate(float fElasedTime)
    {
        m_fCurTime += fElasedTime;    
    }

    public void AddScore( int nScore)
    {
        m_nScore += nScore;
    }


}
