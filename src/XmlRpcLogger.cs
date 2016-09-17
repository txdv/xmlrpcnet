using System;
using System.IO;
using System.Diagnostics;

namespace CookComputing.XmlRpc
{
  public abstract class XmlRpcLogger
  {
    public void SubscribeTo(IXmlRpcProxy proxy)
    {
      proxy.RequestEvent += new XmlRpcRequestEventHandler(OnRequest);
      proxy.ResponseEvent += new XmlRpcResponseEventHandler(OnResponse);
    }

    [Obsolete("This method is obsolete; use SubscribeTo instead")]
    public void Attach(IXmlRpcProxy proxy)
    {
      SubscribeTo(proxy);
    }

    public void UnsubscribeFrom(IXmlRpcProxy proxy)
    {
      proxy.RequestEvent -= new XmlRpcRequestEventHandler(OnRequest);
      proxy.ResponseEvent -= new XmlRpcResponseEventHandler(OnResponse);
    }

    [Obsolete("This method is obsolete; use UnsubcribeFrom instead")]
    public void Detach(IXmlRpcProxy proxy)
    {
      UnsubscribeFrom(proxy);
    }

    protected virtual void OnRequest(object sender, XmlRpcRequestEventArgs e)
    {
    }

    protected virtual void OnResponse(object sender, XmlRpcResponseEventArgs e)
    {
    }
  }
}
