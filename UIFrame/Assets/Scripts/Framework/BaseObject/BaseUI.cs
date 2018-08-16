using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
	
	#region Cache gameObject & transform

	private GameObject m_cacheGameObject;
	/// <summary>
	/// Gets the cache game object
	/// </summary>
	public GameObject CacheGameObject
	{
		get
		{
			if (null == m_cacheGameObject)
				m_cacheGameObject = this.gameObject;
			return m_cacheGameObject;
		}
		
	}

	private Transform m_cacheTransform;
	/// <summary>
	/// get cache transform
	/// </summary>
	public Transform CacheTransform
	{
		get
		{
			if (null == m_cacheTransform)
			{
				m_cacheTransform = this.transform;
			}
			return m_cacheTransform;
		}
	}
	#endregion

	#region EnumObjectState & UIType

	protected EnumObjectState m_state = EnumObjectState.None;
	public event StateChangedEvent StateChanged;
	public EnumObjectState State
	{
		get { return this.m_state; }
		set
		{
			EnumObjectState oldState = m_state;
			this.m_state = value;
			if (null != StateChanged)
				StateChanged(this, this.m_state, oldState);
		}
	}
	
	public abstract EnumUIType GetUIType();
	

	#endregion
	

	// Use this for initialization
	void Start () {
		OnStart();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.m_state == EnumObjectState.Ready)
			OnUpdate(Time.deltaTime);
	}

	void Awake()
	{
		State = EnumObjectState.Initial;
		OnAwake();
	}

	public void Release()
	{
		this.State = EnumObjectState.Closing;
		Destroy(this.CacheGameObject);
		OnRelease();
	}

	

	private void OnDestroy()
	{
		State = EnumObjectState.None;
	}

	protected virtual void OnAwake()
	{
		State = EnumObjectState.Loading;
		this.OnPlayOpenUIAudio();
		
	}
	protected virtual void OnStart(){}
	protected virtual void OnUpdate(float deltaTime){}
	protected  virtual void OnRelease()
	{
		State = EnumObjectState.None;
		this.OnPlayCloseUIAudio();
	}
	/// <summary>
	/// 播放打开界面音乐
	/// </summary>
	protected virtual void OnPlayOpenUIAudio()
	{
		
	}
	/// <summary>
	/// 播放关闭界面音乐
	/// </summary>
	protected virtual void OnPlayCloseUIAudio()
	{
		
	}

	protected virtual void SetUI(params object[] uiParams)
	{
		State = EnumObjectState.Loading;
	}
	protected virtual void OnLoadData(){}

	public void SetUIWhenOpening(params object[] uiParams)
	{
		SetUI(uiParams);
		CoroutineController.Instance.StartCoroutine(LoadDataAsyn());
		
	}

	private IEnumerator LoadDataAsyn()
	{
		yield return new WaitForSeconds(0);
		if (State == EnumObjectState.Loading)
		{
			this.OnLoadData();
			this.State = EnumObjectState.Ready;
		}
	}

	public virtual void SetUIParam(params object[] yuParams)
	{
		
	}
	
	
}
