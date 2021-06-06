using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public  float m_fMaxTime = 30.0f;       // ���� ����ð�
    private float m_fCurTime = 0;

    public void Initialize()
    {
        m_fCurTime = 0;
    }

    public float durationTime    {
        get { return m_fCurTime;  }
    }
    // ���� �ð�
    public float GetRemainTime()
    {
        return m_fMaxTime - m_fCurTime;
    }



    public void OnUpdate(float fElasedTime)
    {
        m_fCurTime += fElasedTime;    
    }
}
