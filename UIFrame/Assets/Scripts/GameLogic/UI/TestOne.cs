using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestOne : BaseUI {
    
    private Button btn;

	private void OnBtnClick()
	{
	//		GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TestTwo")) ;
	//	    TestTwo tt = go.GetComponent<TestTwo>();
	//		if (null != tt)
	//		{
	//			tt = go.AddComponent<TestTwo>();
	//		}
		UIManager.Instance.OpenUICloseOthers(EnumUIType.TestTwo);
		//Close();
	}
	
	private void Close()
	{
		DestroyImmediate(gameObject);
	}
	// Use this for initialization
	public override EnumUIType GetUIType()
	{
		return EnumUIType.TestOne;
	}

	void Start ()
	{
		btn = transform.Find("Panel/Button").GetComponent<Button>();
		btn.onClick.AddListener(OnBtnClick);
	}
	
	
}
