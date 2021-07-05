using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameUI : MonoBehaviour
{
    public TestPlayer m_Player = null;
    public TestEnemy m_Enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        m_Player.Initialize();
        m_Enemy.Initialize();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
