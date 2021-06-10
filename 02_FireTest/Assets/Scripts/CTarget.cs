using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTarget : MonoBehaviour
{
    [SerializeField] GameObject m_Body = null;
    public FXParticle m_Explsion = null;

    [HideInInspector] public bool m_IsDead = false;           // 충돌 2번 되는것 방지
    
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
        Invoke("HideTarget", 3.0f);
    }

    void HideTarget()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.left * 0.0001f);
        transform.Translate(Vector3.right * 0.0001f);
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
