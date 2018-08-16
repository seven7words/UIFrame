using System.Collections;
using System.Collections.Generic;
public class Message:IEnumerable<KeyValuePair<string,object>>
{
    private Dictionary<string, object> dicDatas = null;
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
