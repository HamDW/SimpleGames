using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameScene : MonoBehaviour
{
    public TestGameUI m_GameUI;
    public TestHudUI m_HudUI;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        m_GameUI.Initialize();
        m_HudUI.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
