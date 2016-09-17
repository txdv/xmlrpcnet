namespace CookComputing.XmlRpc
{
  using System;
  using System.IO;
  using System.Reflection;

  public class XmlRpcRequest
  {
    public XmlRpcRequest()
    {
    }

    public XmlRpcRequest(string methodName, object[] parameters, MethodInfo methodInfo)
    {
      method = methodName;
      args = parameters;
      mi = methodInfo;
    }

    public XmlRpcRequest(string methodName, object[] parameters,
      MethodInfo methodInfo, string XmlRpcMethod, Guid proxyGuid)
    {
      method = methodName;
      args = parameters;
      mi = methodInfo;
      if (XmlRpcMethod != null)
        method = XmlRpcMethod;
      proxyId = proxyGuid;
      if (mi != null)
        ReturnType = mi.ReturnType;
    }

    public XmlRpcRequest(string methodName, Object[] parameters)
    {
      method = methodName;
      args = parameters;
    }

    public String method = null;
    public Object[] args = null;
    public MethodInfo mi = null;
    public Guid proxyId;
    static int _created;
    public int number = System.Threading.Interlocked.Increment(ref _created);
    public Type ReturnType;
  }
}
