using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public const float DFIRE_DELAY_MAX = 0.4f;

    //public CGun m_Gun = null;
    public Player m_Player = null;
    public Transform [] m_SpwanList = null;     // 적 생성 지점 리스트

    public GameObject m_prefabEnemy = null;
    public Transform m_EnemyParent = null;

    public CEnemyData m_ZombiData = null;

    //[SerializeField] SpriteRenderer m_sprMouseCursor = null;

    public float m_CreateDelay = 2.0f;
    [HideInInspector] public bool m_bMoveStart = false;

    float m_fFireDelay = 0;
    bool m_bFire = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ZombiData Count = " + m_ZombiData.m_listData.Count);
    }

    public void Initialize()
    {
        Cursor.visible = false;
        if (GameMgr.Inst.IsFPS){
            Cursor.lockState = CursorLockMode.Locked;
        }

        m_Player.Initialize();

        // 타겟 생성 시작
        m_bMoveStart = true;
        StartCoroutine(EnumFunc_EnemyCreate());
    }


    IEnumerator EnumFunc_EnemyCreate()
    {
        while( m_bMoveStart )
        {
            CreateEnemy();
            
            yield return new WaitForSeconds(m_CreateDelay); //0.3f
        }
    }


    private CEnemyData.SDataInfo MakeRandomZombiData()
    {
        int nRes = Random.Range(0, 3);

        return m_ZombiData.GetData(nRes);
    }

    private Vector3 MakeRandomPosition()
    {
        int nRes = Random.Range(0, m_SpwanList.Length);
        return m_SpwanList[nRes].position;
    }


    public void CreateEnemy()
    {
        GameObject go = Instantiate(m_prefabEnemy, m_EnemyParent);
        go.transform.position = MakeRandomPosition();
        go.transform.localScale = m_prefabEnemy.transform.localScale;

        CEnemyData.SDataInfo kInfo = MakeRandomZombiData();

        Enemy kTarget = go.GetComponent<Enemy>();
        kTarget.Initialize(kInfo, m_Player.transform);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameMgr.Inst.gameScene.m_BattleFSM.IsGameState())
            return;

        //Update_Fire();
    }

    //private void Update_Fire()
    //{
    //    m_fFireDelay += Time.deltaTime;
    //    if (m_fFireDelay >= DFIRE_DELAY_MAX)
    //    {
    //        m_bFire = true;
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (m_bFire)
    //        {
    //            // 0.3초 간격으로 사격을 하자
    //            m_Player.Fire();
    //            m_bFire = false;
    //            m_fFireDelay = 0;
    //        }
    //    }
    //}


    public void SetGameResultEnter()
    {
        m_bMoveStart = false;
        DestroyAllTarget();
        m_Player.SetIsCanFire(false);


        FPSCamera.UnlockedMouse();

        GameMgr.Inst.IsFPS = false;
    }


    public void DestroyAllTarget()
    {
        Enemy[] listEnemy = m_EnemyParent.GetComponentsInChildren<Enemy>();
        for( int i = 0; i < listEnemy.Length; i++)
        {
            if (listEnemy[i].gameObject != null)
                Destroy(listEnemy[i].gameObject, 0.1f);
        }
    }

}
