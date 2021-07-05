using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestPlayer : MonoBehaviour
{
    public const float DFIRE_DELAY_MAX = 0.3f;

    public TestGun m_Gun = null;
    float m_fFireDelay = 0;
    bool m_bFire = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_Gun.Initialize();
    }

    public void Fire()
    {
        m_Gun.Fire();
    }


    public void SetIsCanFire(bool bState)
    {
        m_Gun.SetIsCanFire(bState);
    }

    // Update is called once per frame
    void Update()
    {
        Update_Fire();
    }


    private void Update_Fire()
    {
        m_fFireDelay += Time.deltaTime;
        if (m_fFireDelay >= DFIRE_DELAY_MAX)
        {
            m_bFire = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_bFire)
            {
                // 0.3초 간격으로 사격을 하자
                this.Fire();
                m_bFire = false;
                m_fFireDelay = 0;
            }
        }
    }

}


