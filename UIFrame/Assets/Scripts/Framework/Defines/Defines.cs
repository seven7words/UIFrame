using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Global delegate

	
public delegate void StateChangeEvent(object ui,EnumObjectState newState,EnumObjectState oldState);

public delegate void MessageEvent(Message message);
#endregion
#region 全局枚举

/// <summary>
/// Enum Object State
/// </summary>
public enum EnumObjectState
{
	None,
	Initial,
	Loading,
	Ready,
	Disabled,
	Closing
}

public enum EnumUIType:int
{
	None = -1,
	TestOne = 0,
	TestTwo = 1,
}
#endregion

public class UIPathDefines
{
	/// <summary>
	/// UI预设
	/// </summary>
	public const string UI_PREFAB = "Prefabs/";
	/// <summary>
	/// UI小控件预设
	/// </summary>
	public const string UI_CONTROLS_PREFAB = "UIPrefabs/Control/";
	/// <summary>
	/// ui子界面预设
	/// </summary>
	public const string UI_SUBUI_PREFAB = "UIPrefabs/SubUI/";
	/// <summary>
	/// icon路径
	/// </summary>
	public const string UI_ICON_PATH = "UI/Icon/";

	public static string GetPrefabsPathByType(EnumUIType uiType)
	{
		string path = string.Empty;
		switch (uiType)
		{
			case EnumUIType.TestOne:
				path = UI_PREFAB + "TestUIOne";
				break;
			case EnumUIType.TestTwo:
				path = UI_PREFAB + "TestUITwo";
				break;
			default:
				Debug.Log("Not Find EnumUIType  type"+uiType.ToString());
				break;
					
					
		}

		return path;
	}
	
	public static System.Type GetUIScriptByType(EnumUIType uiType)
	{
		System.Type scriptType = null;
		switch (uiType)
		{
			case EnumUIType.TestOne:
				scriptType = typeof(TestOne);
				break;
			case EnumUIType.TestTwo:
				scriptType =  typeof(TestTwo);
				break;
			default:
				Debug.Log("Not Find EnumUIType  type"+uiType.ToString());
				break;
					
					
		}

		return scriptType;
	}
}

public class Defines  {
	public Defines()
	{
		
	}
	
}
