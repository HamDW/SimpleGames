using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget2 : MonoBehaviour
{
    [SerializeField] GameObject m_Body = null;
    public float m_Speed = 1.0f;
    public FXParticle m_Explosion = null;                    // ���� ����Ʈ
    public TestHudUI m_HudUI;


    [HideInInspector] public bool m_IsDead = false;           // �浹 2�� �Ǵ°� ����
    [HideInInspector] public bool m_bMove = false;            // �̵� ����  

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 10.0f);
    }

    public void Initialize(bool bMove)
    {
        m_bMove = bMove;

    }


    // Update is called once per frame
    void Update()
    {
        if (m_bMove)
            transform.Translate(Vector3.left * m_Speed * Time.deltaTime * 10);
    }

    public void CheckHit(Vector3 vHitWorldPos)
    {
        Debug.Log("Hit");
        BoxCollider kCollider = GetComponent<BoxCollider>();
        Vector3 vPos = transform.InverseTransformPoint(vHitWorldPos);  // Ÿ�� ���� ���� ��ǥ�� ��ȯ

        Vector3 vSize = kCollider.size;
        if (vPos.x > -vSize.x * 0.25f && vPos.x < vSize.x * 0.25f
            && vPos.y > -vSize.y * 0.25f && vPos.y < vSize.y * 0.25f)
        {
            Debug.Log("100 ��");
            m_HudUI.PrintScore2(100);
        }
        else
        {
            Debug.Log("50 ��");
            m_HudUI.PrintScore2(50);
        }
    }
}
