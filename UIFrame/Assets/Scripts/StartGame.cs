using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UIManager.Instance.OpenUI(EnumUIType.TestOne);
//		ResourcesManager.Instance.Init();
//		UIManager.Instance.Init();
//		GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TestOne")) ;
//		TestOne tt = go.GetComponent<TestOne>();
//		if (null != tt)
//		{
//			tt = go.AddComponent<TestOne>();
//		}
	}
	
	
}
