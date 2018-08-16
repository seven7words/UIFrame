
    using System.Collections.Generic;

public class MessageCenter:Singleton<MessageCenter>
{
    private Dictionary<string, List<MessageEvent>> dicMessageEvent = null;
    
    public override void Init()
    {
        dicMessageEvent = new Dictionary<string, List<MessageEvent>>();
    }

    #region Add &Remove Listener

    public void AddListener(string messageName,MessageEvent messageEvent)
    {
        List<MessageEvent> list = null;
        if (dicMessageEvent.ContainsKey(messageName))
        {
            list = dicMessageEvent[messageName];
        }
        else
        {
            list = new List<MessageEvent>();
            dicMessageEvent.Add(messageName,list);
        }
        list.Add(messageEvent);

    }

    public void RemoveListener(string messageName, MessageEvent messageEvent)
    {
        if (dicMessageEvent.ContainsKey(messageName))
        {
            List<MessageEvent> list = dicMessageEvent[messageName];
            if (list.Contains(messageEvent))
            {
                list.Remove(messageEvent);
            }

            if (list == null || list.Count<=0)
            {
                dicMessageEvent.Remove(messageName);
            }
        }
    }
    public void AddListener(MessageType messageType,MessageEvent messageEvent)
    {
        AddListener(messageType.ToString(),messageEvent);
    }
    public void RemoveListener(MessageType messageType, MessageEvent messageEvent)
    {
        RemoveListener(messageType.ToString(),messageEvent);
    }

    public void RemoveAllListener()
    {
        dicMessageEvent.Clear();
    }
    #endregion
  

    #region SendMessage

    public void SendMessage(Message message)
    {
        DoMessageDispatcher(message);
    }
    public void SendMessage(string name,object sender)
    {
        SendMessage(new Message(name,sender));
    }
    public void SendMessage(string name,object sender,object content)
    {
        SendMessage(new Message(name,sender,content));
    }
    public void SendMessage(string name,object sender,object content,params object[] dicParams)
    {
        SendMessage(new Message(name,sender,content,dicParams));
    }

    private void DoMessageDispatcher(Message message)
    {
        if (dicMessageEvent == null || !dicMessageEvent.ContainsKey(message.Name))
        {
            return;
        }

        List<MessageEvent> list = dicMessageEvent[message.Name];
        for (int i = 0; i < list.Count; i++)
        {
            MessageEvent messageEvent = list[i];
            if (null != messageEvent)
            {
                messageEvent(message);
            }
        }
    }
    #endregion
  

}
