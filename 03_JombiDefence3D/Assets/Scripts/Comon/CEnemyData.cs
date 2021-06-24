using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = int.MaxValue)]

public class CEnemyData : ScriptableObject
{
    [Serializable]
    public class SDataInfo
    {
        public string m_Name;
        public int m_Type = 0;
        public int m_Hp = 100;
        public float m_Speed = 1;
    }

    //private static CSaveData _inst = null;

    //public static CSaveData Inst {
    //    get {
    //        if (_inst == null)
    //            _inst = CreateInstance<CSaveData>();

    //        return _inst;
    //    }
    //}

    public List<SDataInfo> m_listData = null;


    public SDataInfo GetData( int idx )
    {
        if( idx >= 0 && idx < m_listData.Count)
            return m_listData[idx];

        return null;
    }


}
