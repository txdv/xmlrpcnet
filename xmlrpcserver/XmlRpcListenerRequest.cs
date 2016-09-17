#if !FX1_0

namespace CookComputing.XmlRpc
{
  using System;
  using System.IO;
  using System.Net;

  public class XmlRpcListenerRequest : CookComputing.XmlRpc.IHttpRequest
  {
    public XmlRpcListenerRequest(HttpListenerRequest request)
    {
      _request = request;
    }

    public Stream InputStream
    {
      get { return _request.InputStream; }
    }

    public string HttpMethod
    {
      get { return _request.HttpMethod; }
    }

    private HttpListenerRequest _request;
  }
}

#endif
