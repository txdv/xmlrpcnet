using System;
using System.Reflection;

namespace CookComputing.XmlRpc
{
  public class XmlRpcResponse
  {
    public XmlRpcResponse()
    {
      retVal = null;
    }
    public XmlRpcResponse(object retValue)
    {
      retVal = retValue;
    }

    public XmlRpcResponse(object retValue, MethodInfo mi)
    {
      retVal = retValue;
      MethodInfo = mi;
    }

    public Object retVal;
    public MethodInfo MethodInfo { get; set; }
  }
}
