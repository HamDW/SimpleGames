using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform m_Target = null;
    private Animator m_Animator = null;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Initialize(Transform target)
    {
        m_Target = target;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
