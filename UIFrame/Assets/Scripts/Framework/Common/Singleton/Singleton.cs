using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T :class,new() {

	protected static T m_Instance = null;

	public static T Instance
	{
		get
		{
			if (null == m_Instance)
			{
				m_Instance = new T();
			}

			return m_Instance;
		}
	}

	protected Singleton()
	{
		if (null != m_Instance)
		{
			throw new SingletonException("This"+typeof(T).ToString()+"Singleton is null");
		}
		Init();
	}

	public virtual void Init()
	{
		Debug.Log("Singleton : Singleton<T> Init ");
	}
}
