
using UnityEngine;
public abstract class DDOLSingleton<T>:MonoBehaviour where T:DDOLSingleton<T>
{
    protected static T m_Instance;

    public static T Instance
    {
        get
        {
            if (null == Instance)
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
