using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] Image m_Bar = null;
    [SerializeField] Text m_txtHP = null;

    private bool m_bAction = false;
    
    
    void Start()
    {
    }

    // Start is called before the first frame update
    public void Initialize()
    {
        m_bAction = true;
        PrintHPBar();
    }

    public void SetIsActionBar( bool bAction ){
        m_bAction = bAction;
    }

    public void PrintHPBar()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        //ActorInfo kActor = kGameInfo.m_ActorInfo;
        int curHP = kGameInfo.m_HP;
        int maxHP = kGameInfo.GetMaxHP();

        m_Bar.fillAmount = (float)curHP / maxHP;
        m_txtHP.text = kGameInfo.m_HP.ToString();
    }


    void Update()
    {
        if( m_bAction )
            PrintHPBar();        
    }

}
