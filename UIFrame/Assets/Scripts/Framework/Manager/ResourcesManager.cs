
using UnityEngine;

public class ResourcesManager :Singleton<ResourcesManager>
{

    public override void Init()
    {
        base.Init();
        Debug.Log("ResourcesManager : Singleton<ResourcesManager> Init ");
    }

    public void test()
    {
        Debug.Log("ResourcesManager  test ");
     
    }
}