
using System;
using System.Collections.Generic;

public class ModuleManager:Singleton<ModuleManager>
{
    private Dictionary<string, BaseModule> dicModules = null;
    public override void Init()
    {
        base.Init();
        dicModules = new Dictionary<string, BaseModule>();
    }

    #region Get Module

    public BaseModule Get(string key)
    {
        if (dicModules.ContainsKey(key))
        {
            return dicModules[key];
        }
        return null;
    }

    public T Get<T>() where T:BaseModule
    {
        Type t = typeof(T);
        //return Get(t.ToString()) as T;
        if (dicModules.ContainsKey(t.ToString()))
        {
            return dicModules[t.ToString()] as T;
        }
        return null;
    }

    #endregion


    #region Register

    public void Register(BaseModule module)
    {
        Type t = module.GetType();
        Register(t.ToString(),module);
    }

    public void Register(string key, BaseModule module)
    {
        if (!dicModules.ContainsKey(key))
        {
            dicModules.Add(key,module);
        }
    }

    #endregion
   

    #region Unregister
    public void UnRegister( BaseModule module)
    { 
        Type t = module.GetType();
        
        UnRegister(t.ToString());
    }

    public void UnRegister(string key)
    {
        if (dicModules.ContainsKey(key))
        {
            BaseModule module = dicModules[key];
            module.Release();
            dicModules.Remove(key);
            module = null;
        }
    }
    public void UnRegisterAll()
    {
        List<string> listKey = new List<string>(dicModules.Keys);
        //多消耗内存，会增加一个装箱拆箱操作
        foreach (string s in listKey)
        {
            UnRegister(s);
        }
        dicModules.Clear();
    }

    

    #endregion
   
}
