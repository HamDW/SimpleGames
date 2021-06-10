using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGun : MonoBehaviour
{
    [SerializeField] GameObject m_PrefabBullet = null;      // 총알 프리팹
    [SerializeField] Transform m_BulletParent = null;       // 총알들 부모 노드
    public Transform m_BulletStartPos = null;               // 총알 발사 시작점
    [SerializeField] private Animator m_gunAnimator = null;
    
    private bool m_IsCanFire = false;                       // 사격 가능한가?
    private AudioSource m_Audio = null;


    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_IsCanFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsCanFire)
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


    public void CreateBullet(Vector3 vTargetPos)
    {
        GameObject go = Instantiate(m_PrefabBullet, m_BulletParent);
        go.transform.localScale = m_PrefabBullet.transform.localScale;
        go.transform.position = m_BulletStartPos.position;

        Bullet kBullet = go.GetComponent<Bullet>();
        kBullet.Initialize(vTargetPos, 1.0f);
    }

    public bool RayCastTest()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            CreateBullet(hit.point);

            m_Audio.Play();

            //Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;
            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 3.0f);
            return true;
        }
        return false;
    }

    public bool RayCastTest2()
    {
        int layer1 = LayerMask.NameToLayer("Background");
        int layer2 = LayerMask.NameToLayer("Target");
        int layerMask = (1 << layer1) | (1 << layer2);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            CreateBullet(hit.point);

            m_Audio.PlayOneShot(m_Audio.clip);

            Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            Vector3 vDir = hit.point - m_BulletStartPos.position;
            Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 1.0f);

            return true;
        }
        return false;
    }


 

    //public Vector3 ScreenToWorldPos()
    //{
    //    Vector3 vMouse = Input.mousePosition;
    //    vMouse.z = -Camera.main.transform.position.z;
    //    return Camera.main.ScreenToWorldPoint(vMouse);
    //}

}
