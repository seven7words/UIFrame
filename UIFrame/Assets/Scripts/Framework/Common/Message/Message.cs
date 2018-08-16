using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Message:IEnumerable<KeyValuePair<string,object>>
{
    private Dictionary<string, object> dicDatas = null;

    public string Name { get; private set; }

    public object Sender { get; private set; }
    public object Content { get; set; }

    #region message[key] = value or data = message[key]

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="key"></param>
    public object this[string key] {
        get
        {
            if (null == dicDatas || !dicDatas.ContainsKey(key))
            {
                return null;
            }
            else
            {
                return dicDatas[key];
            }
        }
        set
        {
            if (null == dicDatas)
            {
                dicDatas = new Dictionary<string, object>();
            }

            if (dicDatas.ContainsKey(key))
            {
                dicDatas[key] = value;
            }
            else
            {
                dicDatas.Add(key,value);
            }
        } }

    #endregion
 
    #region IENumerable implementation

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        if (null == dicDatas)
        {
            yield break;
        }

        foreach (KeyValuePair<string,object> keyValuePair in dicDatas)
        {
            yield return keyValuePair;
        }
    }


    #endregion

    #region IEnumerable impletatation

    IEnumerator IEnumerable.GetEnumerator()
    {
        return dicDatas.GetEnumerator();
    }

    #endregion

    #region 构造函数

    public Message(string name, object sender, object content)
    {
        Name = name;
        Sender = sender;
        Content = content;
    }

    public Message(string name, object sender)
    {
        Name = name;
        Sender = sender;
        Content = null;
    }

    public Message(string name, object sender, object content, params object[] dicParam)
    {
        Name = name;
        Sender = sender;
        Content = content;
        if (dicParam.GetType() == typeof(Dictionary<string, object>))
        {
            foreach (object o in dicParam)
            {
                foreach (KeyValuePair<string,object> keyValuePair in o as Dictionary<string ,object>)
                {
                    this[keyValuePair.Key] = keyValuePair.Value;
                }
            }
        }
    }

    public Message(Message message)
    {
        Name = message.Name;
        Sender = message.Sender;
        Content = message.Content;
        foreach (KeyValuePair<string,object> keyValuePair in message.dicDatas)
        {
            this[keyValuePair.Key] = keyValuePair.Value;
        }
    }

    #endregion

    #region Add & Remove

    public void Remove(string key)
    {
        if (null != dicDatas && dicDatas.ContainsKey(key))
        {
            dicDatas.Remove(key);
        }
    }

    public void Add(string key, object value)
    {
        this[key] = value;
    }

    #endregion

    #region Send

    public void Send()
    {
        //MessageCenter SendMessage
        MessageCenter.Instance.SendMessage(this);
    }

    #endregion
   
}
