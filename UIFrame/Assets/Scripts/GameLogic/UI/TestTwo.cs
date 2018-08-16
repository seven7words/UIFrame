using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTwo : BaseUI {

	
	private Button btn;

	private void OnBtnClick()
	{
//		GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TestOne")) ;
//		TestOne tt = go.GetComponent<TestOne>();
//		if (null != tt)
//		{
//			tt = go.AddComponent<TestOne>();
//		}
		UIManager.Instance.OpenUICloseOthers(EnumUIType.TestOne);
		//Close();
	}

	private void Close()
	{
		DestroyImmediate(gameObject);
	}
	// Use this for initialization
	public override EnumUIType GetUIType()
	{
		return EnumUIType.TestTwo;
	}

	void Start ()
	{
		btn = transform.Find("Panel/Button").GetComponent<Button>();
		btn.onClick.AddListener(OnBtnClick);
	}
}
