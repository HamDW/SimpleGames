using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public const int DATTACK_VAULE = 100;    // �Ѿ� ���ݷ�
                                             // 
    public  float m_fMaxTime = 180.0f;       // ���� ��ü �ð�
    private float m_fCurTime = 0;           // ���� ��� �ð�
    public int m_nScore = 0;                // ����


    public void Initialize()
    {
        m_fCurTime = 0;
        m_nScore = 0;
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

    public void AddScore( int nScore)
    {
        m_nScore += nScore;
    }


}
