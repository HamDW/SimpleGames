using UnityEngine;

using System.Collections;
using System.Collections.Generic;

// new를 사용하는 오브젝트 와 class만 사용할수 있는 탬플릿 

public class Singleton<T> where T : class, new() 
{
    static T instance = null;
    static readonly object objLock = new object();

    public static T Inst
    {
        get {
            if (instance == null) {
                lock (objLock){
                    instance = new T();
                }
            }
            return instance;
        }
    }
    
}

// MonoBehaviour를 상속받은 클래스만 사용할수 있는 탬플릿 
public class Singleton2<T>: MonoBehaviour where T : MonoBehaviour
{
    static T instance = null;

    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = "== Singleton class";

                DontDestroyOnLoad(go);
                instance = go.AddComponent(typeof(T)) as T;
            }
            return instance;
        }
    }
  
}
