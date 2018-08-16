using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//UIManager.Instance.OpenUI(EnumUIType.TestOne);
//		ResourcesManager.Instance.Init();
//		UIManager.Instance.Init();
//		GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TestOne")) ;
//		TestOne tt = go.GetComponent<TestOne>();
//		if (null != tt)
//		{
//			tt = go.AddComponent<TestOne>();
//		}
		float tm = System.Environment.TickCount;
		for (int i = 1; i <1000 ; i++)
		{
			GameObject go = null;
//			go = Instantiate(Resources.Load<GameObject>("Prefabs/Cube"));
//			go.transform.position = UnityEngine.Random.insideUnitSphere * 10;
//			
//			go = ResourcesManager.Instance.Load("Prefabs/Cube") as GameObject;
//			go.transform.position = UnityEngine.Random.insideUnitSphere * 10;
			
//			ResourcesManager.Instance.LoadAsyncInstance("Prefabs/Cuba", (obj) =>
//			{
//				go = obj as GameObject;
//				go.transform.position = UnityEngine.Random.insideUnitSphere * 10;
//			});
			
			ResourcesManager.Instance.LoadCoroutineInstance("Prefabs/Cuba", (obj) =>
			{
				go = obj as GameObject;
				go.transform.position = UnityEngine.Random.insideUnitSphere * 10;
			});
		}
		Debug.Log("Times" + (System.Environment.TickCount-tm)*1000);
	}
	
	
}
