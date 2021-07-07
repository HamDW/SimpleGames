using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public const int DATTACK_VAULE = 100;    // �Ѿ� ���ݷ�
    public const int DENEMY_ATTACK = 10;    // �� ���ݷ�
    public const int DMAX_HP = 200;         // player �ִ� HP
                                             // 
    public  float m_fMaxTime = 90.0f;       // ���� ��ü �ð�
    private float m_fCurTime = 0;           // ���� ��� �ð�
    public int m_nScore = 0;                // ����

    public bool m_IsFPS = false;

    public int m_HP = 0;                    // Player HP


    public void Initialize()
    {
        m_fCurTime = 0;
        m_nScore = 0;
        m_HP = DMAX_HP;
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
    public bool AddDamage( int nDamage)
    {
        m_HP -= nDamage;
        if (m_HP <= 0)
        {
            m_HP = 0;
            return false;
        }
        return true;
    }

    public int GetMaxHP()
    {
        return DMAX_HP;
    }
}