using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTarget : MonoBehaviour
{
    [SerializeField] GameObject m_Body = null;
    public FXParticle m_Explsion = null;

    [HideInInspector] public bool m_IsDead = false;           // 충돌 2번 되는것 방지

    Vector3 m_vDir = Vector3.zero;
    float m_Speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

  
    public void Initialize()
    {
        Show();
        MakeRandomDir();

        Invoke("HideTarget", 10.0f);
    }

    void MakeRandomDir()
    {
        int nValue = Random.Range(0, 5);
        switch(nValue)
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
                m_vDir = new Vector3(1,-1, 0);
                m_vDir.Normalize();
                break;
            case 4:
                m_vDir = new Vector3(-1, -1, 0);
                m_vDir.Normalize();
                break;
        }
    }

    void HideTarget()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
    private void Update()
    {
        transform.Translate(m_vDir * m_Speed * Time.deltaTime * 10);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //if (!m_IsDead)
            {
                m_Explsion.Play();
                GameMgr.Inst.AddScore(1);
                GameMgr.Inst.gameScene.m_HudUI.PrintScore();

                m_IsDead = true;
                m_Body.SetActive(false);
                Destroy(collision.gameObject, 0.001f);
                
                
                Destroy(gameObject, 0.5f);
            }
        }
    }




}
