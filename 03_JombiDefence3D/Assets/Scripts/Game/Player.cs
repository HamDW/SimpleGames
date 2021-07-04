using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CGun m_Gun = null;


    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }
}
