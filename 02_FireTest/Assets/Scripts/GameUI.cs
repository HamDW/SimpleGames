using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public CGun m_Gun = null;
    public GameObject m_prefabTarget;
    public Transform m_TargetParent;

    public float m_CreateDelay = 1.0f;
    
    [Header("타겟 생성 범위")]
    public float m_Left = -3.0f;       // 왼쪽
    public float m_Right = 3.0f;       // 오른쪽
    public float m_Bottom = 12.0f;       // 하단
    public float m_Up = 12.0f;          // 상단
    public float m_Depth = 20.0f;       // 깊이( z값 )



    [HideInInspector] public bool m_bCreate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        m_Gun.Initialize();

        // 타겟 생성 시작
        m_bCreate = true;
        StartCoroutine(EnumFunc_TargetCreate());
    }


    IEnumerator EnumFunc_TargetCreate()
    {
        while(m_bCreate)
        {
            CreateTarget();
            
            yield return new WaitForSeconds(m_CreateDelay); //0.3f
        }
    }


    public void CreateTarget()
    {
        GameObject go = Instantiate(m_prefabTarget, m_TargetParent);
        go.transform.position = RandomPos();
        go.transform.localScale = m_prefabTarget.transform.localScale;

        CTarget kTarget = go.GetComponent<CTarget>();
        kTarget.Initialize();
    }

    private Vector3 RandomPos()
    {
        int x = Random.Range((int)m_Left, (int)m_Right);
        int y = Random.Range((int)m_Bottom, (int)m_Up);
        int z = Random.Range((int)m_Depth - 10, (int)m_Depth + 8);

        return new Vector3(x, y, z);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameMgr.Inst.gameScene.m_BattleFSM.IsGameState())
            return;

        if( Input.GetMouseButtonDown(0))
        {
            m_Gun.Fire();
        }
    }

  
    public void SetGameResultEnter()
    {
        m_bCreate = false;
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
