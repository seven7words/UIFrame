using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 界面信息
/// </summary>
public class UIInfoData
{
	public EnumUIType UIType
	{
		get;
		private set;
	}
	public string Path { get; private set; }
	public object[] UIparams { get; private set; }

	public Type ScriptType { get; private set; }
	public UIInfoData(EnumUIType uiType,string path,params object[] uiparams) 
	{
		UIType = uiType;
		Path = path;
		UIparams = uiparams;
		ScriptType = UIPathDefines.GetUIScriptByType(uiType);
	}
}

public class UIManager : Singleton<UIManager>
{

	/// <summary>
	/// 打开UI的字典
	/// </summary>
	private Dictionary<EnumUIType,GameObject> dicOpenUIs = new Dictionary<EnumUIType, GameObject>();

	private Stack<UIInfoData> stackOpenUIs = new Stack<UIInfoData>();
	
	public override void Init()
	{
		base.Init();
		
	}

	public T GetUI<T>(EnumUIType uiType) where T:BaseUI
	{
		GameObject _retObj = GetUIObject(uiType);
		if (_retObj != null)
		{
			return _retObj.GetComponent<T>();
		}

		return null;
	}

	public GameObject GetUIObject(EnumUIType _uiType)
	{
		GameObject _retObj = null;
		if (!dicOpenUIs.TryGetValue(_uiType, out _retObj))
		{
			throw new Exception("dicOpenUIs TryGetValue Failure ! _uiType"+_uiType.ToString());
			
		}

		return _retObj;
	}

	public void PreloadUI(EnumUIType uiType)
	{
		string path = UIPathDefines.GetPrefabPathByType(uiType);
		Resources.Load(path);
//		ResourcesManager.Instance.ResourcesLoad(path);
	}

	public void PreloadUI(EnumUIType[] uiTypes)
	{
		for (int i = 0; i < uiTypes.Length; i++)
		{
			PreloadUI(uiTypes[i]);
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="isCloseOther"></param>
	/// <param name="uiTypes"></param>
	/// <param name="uiParams"></param>
	public void OpenUI(bool isCloseOther,EnumUIType[] uiTypes, params object[] uiParams)
	{
		//Close Other UI
		if (isCloseOther)
		{
			CloseUIAll();
		}
		//push uiTypes in stack
		for (int i = 0; i < uiTypes.Length; i++)
		{
			EnumUIType uiType = uiTypes[i];
			if (!dicOpenUIs.ContainsKey(uiType))
			{
				string path = UIPathDefines.GetPrefabPathByType(uiType);
				stackOpenUIs.Push(new UIInfoData(uiType,path,uiParams));
			}
			
			
		}
		//Open UI 协程加载UI
		if (stackOpenUIs.Count > 0)
		{
			CoroutineController.Instance.StartCoroutine(AsyncLoadData());
		}
	}

	private IEnumerator<int> AsyncLoadData()
	{
		UIInfoData uiInfoData = null;
		UnityEngine.Object prefab = null;
		GameObject uiObj = null;
		if (stackOpenUIs != null && stackOpenUIs.Count > 0)
		{
			do
			{
				uiInfoData = stackOpenUIs.Pop();
				prefab = Resources.Load(uiInfoData.Path);
				if (null != prefab)
				{
					uiObj = MonoBehaviour.Instantiate(prefab) as GameObject;
					BaseUI baseUI = uiObj.GetComponent<BaseUI>();
					if (null != baseUI)
					{
						baseUI.SetUIWhenOpening();
						
					}
					else
					{
						baseUI = uiObj.AddComponent(uiInfoData.ScriptType) as BaseUI;
						
					}
					dicOpenUIs.Add(uiInfoData.UIType,uiObj);
				}
			} while (stackOpenUIs.Count>0);
		}
		yield return 0;
		
	}
	public void CloseUI(EnumUIType uiType)
	{
		GameObject uiObj = GetUIObject(uiType);
		if (uiObj == null)
		{
			dicOpenUIs.Remove(uiType);
		}
		else
		{
			BaseUI baseUI = uiObj.GetComponent<BaseUI>();
			if (null == baseUI)
			{
				GameObject.Destroy(uiObj);
				dicOpenUIs.Remove(uiType);
			}
			else
			{
				baseUI.StateChanged += CloseUIHandle;
				baseUI.Release(); 
			}
		}
	}

	public void CloseUIHandle(object sender, EnumObjectState newState, EnumObjectState oldState)
	{
		if (newState == EnumObjectState.Closing)
		{
			BaseUI baseUi = sender as BaseUI;
			dicOpenUIs.Remove(baseUi.GetUIType());
			baseUi.StateChanged -= CloseUIHandle;
		}
	}

	public void CloseUI(EnumUIType[] uiTypes)
	{
		for (int i = 0; i < uiTypes.Length; i++)
		{
			CloseUI(uiTypes[i]);
		}
	}

	public void CloseUIAll()
	{
		List<EnumUIType> listKey = new List<EnumUIType>(dicOpenUIs.Keys);
		
		CloseUI(listKey.ToArray());
		dicOpenUIs.Clear();
	}
	public void OpenUI(EnumUIType[] uiTypes)
	{
		OpenUI(false,uiTypes,null);
	}
	public void OpenUI(EnumUIType uiType)
	{
		EnumUIType[] uiTypes = new EnumUIType[1];
		uiTypes[0] = uiType;
		OpenUI(false,uiTypes,null);
	}
	public void OpenUI(EnumUIType uiType,params object[] uiParams)
	{
		EnumUIType[] uiTypes = new EnumUIType[1];
		uiTypes[0] = uiType;
		OpenUI(false,uiTypes,uiParams);
	}

	public void OpenUICloseOthers(EnumUIType[] uiTypes)
	{
		OpenUI(true,uiTypes,null);
	}
	public void OpenUICloseOthers(EnumUIType uiType)
	{
		EnumUIType[] uiTypes = new EnumUIType[1];
		uiTypes[0] = uiType;
		OpenUI(true,uiTypes,null);
	}
	public void OpenUICloseOthers(EnumUIType uiType, params object[] uiParams)
	{
		EnumUIType[] uiTypes = new EnumUIType[1];
		uiTypes[0] = uiType;
		OpenUI(true,uiTypes,uiParams);
	}
}
