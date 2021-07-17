#define DUSE_UPGRADE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CTarget : MonoBehaviour
{
    [SerializeField] GameObject m_Body = null;
    public FXParticle m_Explosion = null;                    // 폭파 이펙트

    [HideInInspector] public bool m_IsDead = false;           // 충돌 2번 되는것 방지
    [HideInInspector] public bool m_bMove = false;            // 이동 시작  

    public float m_Speed = 1.0f;
    Vector3 m_vDir = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    public void Initialize(bool bRandomMove)
    {
        m_bMove = true;
        m_vDir = Vector3.left;

#if DUSE_UPGRADE
        Init2(bRandomMove);
#endif 

    }

     

    // Update is called once per frame
    void Update()
    {
        if(m_bMove )
            transform.Translate(m_vDir * m_Speed * Time.deltaTime * 10);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (!m_IsDead)
            {
                m_Explosion.Play();
                GameMgr.Inst.AddScore(1);
                GameMgr.Inst.gameScene.m_HudUI.PrintScore();

                m_IsDead = true;
                m_Body.SetActive(false);
                
                Destroy(collision.gameObject, 0.01f);
                Destroy(gameObject, 0.4f);                  // 폭파이펙트 처리를 위해 조금 늦게 파괴함
            }
        }
    }



#if DUSE_UPGRADE

    public bool m_IsRandomMove = false;

    public void SetIsRandomMove( bool bRandom )
    {
        m_IsRandomMove = bRandom;
    }

    public void Init2(bool bRandomMove)
    {
        m_IsRandomMove = bRandomMove;

        if ( m_IsRandomMove )
        {
            MakeRandomDir();
        }
        else
        {
            m_vDir = Vector3.left;
        }
        //Invoke("HideTarget", 10.0f);
    }

    void MakeRandomDir()
    {
        int nValue = Random.Range(0, 5);
        switch (nValue)
        {
            case 0:
                m_vDir = Vector3.left;
                break;
            case 1:
                m_vDir = Vector3.right;
                break;
            case 2:
                m_vDir = Vector3.down;
                break;
            case 3:
                m_vDir = new Vector3(1, -1, 0);
                m_vDir.Normalize();
                break;
            case 4:
                m_vDir = new Vector3(-1, -1, 0);
                m_vDir.Normalize();
                break;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    void HideTarget()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

#endif



}
