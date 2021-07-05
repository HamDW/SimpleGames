using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestGun : MonoBehaviour
{
    [SerializeField] GameObject m_PrefabBullet = null;      // 총알 프리팹
    [SerializeField] Transform m_BulletParent = null;       // 총알들 부모 노드
    public Transform m_BulletStartPos = null;               // 총알 발사 시작점
    [SerializeField] private Animator m_gunAnimator = null;
    [SerializeField] Transform m_EffectParent = null;

    public bool m_IsCanFire = false;                       // 사격 가능한가?
    private AudioSource m_Audio = null;

    private void Awake()
    {
        m_IsCanFire = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsCanFire)
            RotationGun();
    }

    public void Initialize()
    {
        m_IsCanFire = true;
    }

    public void SetIsCanFire(bool bState)
    {
        m_IsCanFire = bState;
    }

    public void RotationGun()
    {
        Vector3 vMouse = Input.mousePosition;
        vMouse.z = -Camera.main.transform.position.z;
        Vector3 vWorld = Camera.main.ScreenToWorldPoint(vMouse);

        transform.LookAt(vWorld);
    }

    public bool Fire()
    {
        m_gunAnimator.SetTrigger("Fire");
        return RayCastTest();
    }

    public bool RayCastTest()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            CreateDamageEffect(hit.point);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                TestEnemy kEnemy = hit.collider.gameObject.GetComponent<TestEnemy>();
                kEnemy.AddDamage(GameInfo.DATTACK_VAULE);
            }

            m_Audio.Play();

            //Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;

            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 3.0f);
            return true;
        }
        return false;
    }




    public void CreateDamageEffect(Vector3 vPos)
    {
        GameObject goPrefab = Resources.Load<GameObject>("Prefabs/FX/FxDamage");

        GameObject go = Instantiate(goPrefab, m_EffectParent);
        vPos.z -= 0.3f;        // 약간 앞쪽에 출력한다.
        go.transform.position = vPos;
        go.transform.localScale = goPrefab.transform.localScale;

    }

}
