using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiRobot : MonoBehaviour
{
    private Animator m_Animator = null;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Animator.SetFloat("speed", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Animator.SetFloat("speed", 0.01f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Animator.SetTrigger("attack");
            
        }

    }
}
