
using UnityEngine;
using System.Collections;
public abstract class DDOLSingleton<T>:MonoBehaviour where T:DDOLSingleton<T>
{
    protected static T m_Instance = null;

    public static T Instance
    {
        get
        {
            if (null == m_Instance)
            {
                GameObject go = GameObject.Find("DDOLGameObject");
                if (null == go)
                {
                    go = new GameObject("DDOLGameObject");
                    DontDestroyOnLoad(go);
                }

                m_Instance = go.AddComponent<T>();
            }

            return m_Instance;
        }
    }

    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}


