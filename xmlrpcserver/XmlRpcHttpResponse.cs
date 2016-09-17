using System;
using System.IO;

namespace CookComputing.XmlRpc
{
  public class XmlRpcHttpResponse : CookComputing.XmlRpc.IHttpResponse
  {
    public XmlRpcHttpResponse(System.Web.HttpResponse response)
    {
      m_resp = response;
    }

    string IHttpResponse.ContentType
    {
      get { return m_resp.ContentType; }
      set { m_resp.ContentType = value; }
    }
    TextWriter IHttpResponse.Output { get { return m_resp.Output; } }

    Stream IHttpResponse.OutputStream { get { return m_resp.OutputStream; } }

    bool IHttpResponse.SendChunked
    {
      get { return true; }
      set { throw new NotImplementedException(); }
    }

    int IHttpResponse.StatusCode
    {
      get { return m_resp.StatusCode; }
      set { m_resp.StatusCode = value; }
    }

    string IHttpResponse.StatusDescription
    {
      get { return m_resp.StatusDescription; }
      set { m_resp.StatusDescription = value; }
    }

    Int64 IHttpResponse.ContentLength
    {
      set { throw new NotImplementedException(); }
    }

    private System.Web.HttpResponse m_resp;
  }
}
