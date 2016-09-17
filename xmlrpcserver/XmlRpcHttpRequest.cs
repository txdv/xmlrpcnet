using System;
using System.IO;

namespace CookComputing.XmlRpc
{
  public class XmlRpcHttpRequest : CookComputing.XmlRpc.IHttpRequest
  {
    public XmlRpcHttpRequest(System.Web.HttpRequest request)
    {
      m_req = request;
    }

    public Stream InputStream { get { return m_req.InputStream; } }

    public string HttpMethod { get { return m_req.HttpMethod; } }

    private System.Web.HttpRequest m_req;
  }
}
