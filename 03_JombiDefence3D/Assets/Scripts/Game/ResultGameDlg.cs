using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultGameDlg : MonoBehaviour
{
    [SerializeField] Text m_txtScore = null;

    [SerializeField] Button m_btnReStart = null;
    [SerializeField] Button m_btnExit = null;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_btnReStart.onClick.AddListener(OnClicked_Restart);
        m_btnExit.onClick.AddListener(OnClicked_Exit);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);

        PrintScore();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }


    public void OnClicked_Restart()
    {
        GameScene kGameScene = GameMgr.Inst.gameScene;
        kGameScene.ResetGame();
        CloseUI();
    }

    public void OnClicked_Exit()
    {
        GameScene kGameScene = GameMgr.Inst.gameScene;
        kGameScene.ExitGame();
        CloseUI();
    }
    public void PrintScore()
    {
        if (m_txtScore != null)
        {
            int nScore = GameMgr.Inst.GetScore();
            m_txtScore.text = string.Format("Score  :  {0}", nScore);
        }
    }

}
