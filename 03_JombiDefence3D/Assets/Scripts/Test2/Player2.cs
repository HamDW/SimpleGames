using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1��Ī �������� ĳ������ �¿� ȸ�� �� ���� ȸ��
//  - �¿�ȸ�� : Player ȸ�� ( ĳ���Ͱ� ����ȸ���ϸ� ��,�ڷ� ������ �̻��ϰ� ���̱� ������ �¿츸 ȸ���Ѵ�.)
//  - ����ȸ�� : Camera ȸ��

public class Player2 : MonoBehaviour
{
    public float DFIRE_DELAY_MAX = 0.3f;
    public TestGun2 m_Gun = null;
    private PlayerMove2 m_Move = null;

    private float m_fFireTime = 0;          // �߻�ð��� ����ϱ� ���� ����  
    private bool m_bCanFire = false;        // �߻� ���� ����

    void Start()
    {
        m_Move = GetComponent<PlayerMove2>();
        Initialize();
    }

    void Update()
    {
        Update_Fire();
    }


    public void Initialize()
    {
        m_Move.Initialize();      
    }



    public void Fire()
    {
        m_Gun.Fire();
        m_Move.m_Animator.SetTrigger("Attack");
    }


    public void SetIsCanFire(bool bState)
    {
        m_Gun.SetIsCanFire(bState);
    }

    private void Update_Fire()
    {
        m_fFireTime += Time.deltaTime;
        if (m_fFireTime >= DFIRE_DELAY_MAX)
        {
            m_bCanFire = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_bCanFire)
            {
                // 0.3�� �������� ����� ����
                this.Fire();
                m_bCanFire = false;
                m_fFireTime = 0;
            }
        }
    }

}
