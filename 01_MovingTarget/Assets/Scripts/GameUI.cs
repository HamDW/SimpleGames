#define DUSE_UPGRADE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public CGun m_Gun = null;
    public Transform m_trOffstPos = null;

    public GameObject m_prefabTarget;
    public Transform m_TargetParent;

    public float m_CreateDelay = 2.0f;
    [HideInInspector] public bool m_bMoveStart = false;


    float m_fFireDelay = 0;
    bool m_bFire = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        m_Gun.Initialize();

        // 타겟 생성 시작
        m_bMoveStart = true;
        StartCoroutine(EnumFunc_TargetCreate());
    }


    IEnumerator EnumFunc_TargetCreate()
    {
        while( m_bMoveStart )
        {
            //SCreateTarget();
            SelectCreateTarget();

            yield return new WaitForSeconds(m_CreateDelay); //0.3f
        }
    }


    public void CreateTarget()
    {
        GameObject go = Instantiate(m_prefabTarget, m_TargetParent);
        go.transform.position = m_trOffstPos.position;
        go.transform.localScale = m_prefabTarget.transform.localScale;

        CTarget kTarget = go.GetComponent<CTarget>();
        kTarget.Initialize(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameMgr.Inst.gameScene.m_BattleFSM.IsGameState())
            return;

        //Update_MouseCursor();

        m_fFireDelay += Time.deltaTime;

        if ( Input.GetMouseButtonDown(0))
        {
            if (m_fFireDelay > 0.2f)
            {
                m_bFire = true;
            }

            if (m_bFire)
            {
                m_Gun.Fire();

                m_fFireDelay = 0;
                m_bFire = false;
            }
        }
    }

  
    public void SetGameResultEnter()
    {
        m_bMoveStart = false;
        DestroyAllTarget();
        m_Gun.SetIsCanFire(false);
        m_Gun.ResetPosition();
    }


    public void DestroyAllTarget()
    {
        CTarget[] listTarget = m_TargetParent.GetComponentsInChildren<CTarget>();
        for( int i = 0; i < listTarget.Length; i++ )
        {
            if(listTarget[i].gameObject != null)
                Destroy(listTarget[i].gameObject, 0.1f);
        }
    }

#if DUSE_UPGRADE

    public enum ETargetType
    {
        eLine1 = 1,         // 한줄로 나오기
        eLineMulti,         // 여러줄로 나오기
        eRandomMove,        // 랜덤으로 방향 설정후 이동하기
    }

    public ETargetType m_TargetType = ETargetType.eLine1;       // 타겟 출현 방식

    [SerializeField] SpriteRenderer m_sprMouseCursor = null;

    [Header("타겟 생성 범위")]
    public float m_Left = -3.0f;       // 왼쪽
    public float m_Right = 3.0f;       // 오른쪽
    public float m_Bottom = 12.0f;       // 하단
    public float m_Up = 12.0f;          // 상단
    public float m_Depth = 20.0f;       // 깊이( z값 )

    public void CreateTarget2()
    {
        GameObject go = Instantiate(m_prefabTarget, m_TargetParent);
        go.transform.position = RandomPos();
        go.transform.localScale = m_prefabTarget.transform.localScale;

        CTarget kTarget = go.GetComponent<CTarget>();
        kTarget.Initialize(true);
    }

    private Vector3 RandomPos()
    {
        int x = Random.Range((int)m_Left, (int)m_Right);
        int y = Random.Range((int)m_Bottom, (int)m_Up);
        int z = Random.Range((int)m_Depth - 10, (int)m_Depth + 8);

        return new Vector3(x, y, z);
    }


    public void SelectCreateTarget()
    {
        switch( m_TargetType )
        {
            case ETargetType.eLine1:
                CreateTarget();
                break;
            case ETargetType.eLineMulti:
                break;
            case ETargetType.eRandomMove:
                CreateTarget2();
                break;
        }
    }

    void Update_MouseCursor()
    {
        Vector3 vPos = Input.mousePosition;
        Camera kCamera = Camera.main;
        vPos.z = -kCamera.transform.position.z;

        Vector3 vWorld = kCamera.ScreenToWorldPoint(vPos);
        m_sprMouseCursor.transform.position = vWorld;
    }

#endif
    //public void RayCastTestForMouseCursor()
    //{
    //    RaycastHit hit;
    //    Vector3 vPos = Input.mousePosition;
    //    //vPos.z = -Camera.main.transform.position.z;
    //    Ray ray = Camera.main.ScreenPointToRay(vPos);
    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        m_Gun.Fire();

    //        Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
    //        Vector3 vDir = hit.point - m_Gun.m_BulletStartPos.position;
    //        Debug.DrawRay(m_Gun.m_BulletStartPos.position, vDir, Color.red, 3.0f);
    //    }
    //}


}
