using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XHFrameWork;

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
	
	/*private void Close()
	{
		DestroyImmediate(gameObject);
	}*/
	// Use this for initialization
	public override EnumUIType GetUIType()
	{
		return EnumUIType.TestOne;
	}

	void Start ()
	{
		/*btn = transform.Find("Panel/Button").GetComponent<Button>();
		btn.onClick.AddListener(OnBtnClick);*/
		/*EventTriggerListener.Get(transform.Find("Panel/Button").gameObject).SetEventHandle(EnumTouchEventType.OnClick,Close);
		*/

		
		EventTriggerListener.Get(transform.Find("Panel/Button").gameObject).SetEventHandle(EnumTouchEventType.OnClick,Close,1,"1234");

	}

	protected override void OnAwake()
	{
		MessageCenter.Instance.AddListener("AutoUpdateGold",UpdateGold);
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		MessageCenter.Instance.RemoveListener("AutoUpdateGold",UpdateGold);
	}

	private void UpdateGold(Message message)
	{
		int gold =(int) message["gold"];
		Debug.Log(gold);
	}
	private void Close(GameObject _listener, object _args, params object[] _params)
	{
		int i = (int) _params[0];
		string s = (string) _params[1];
		Debug.Log(i);
		Debug.Log(s);
		UIManager.Instance.OpenUICloseOthers(EnumUIType.TestTwo);
	}
	
}
