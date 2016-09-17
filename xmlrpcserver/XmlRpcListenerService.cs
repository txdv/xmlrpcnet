#if !FX1_0

namespace CookComputing.XmlRpc
{
  using System;
  using System.Net;

  public abstract class XmlRpcListenerService : XmlRpcHttpServerProtocol
  {
    bool _sendChunked = false;

    public virtual void ProcessRequest(HttpListenerContext RequestContext)
    {
      try
      {
        IHttpRequest req = new XmlRpcListenerRequest(RequestContext.Request);
        IHttpResponse resp = new XmlRpcListenerResponse(RequestContext.Response);
        resp.SendChunked = _sendChunked;
        HandleHttpRequest(req, resp);
      }
      catch (Exception ex)
      {
        // "Internal server error"
        RequestContext.Response.StatusCode = 500;
        RequestContext.Response.StatusDescription = ex.Message;
      }
      finally
      {
        try
        {
          RequestContext.Response.OutputStream.Close();
        }
        catch (System.InvalidOperationException)
        {
          // Zev Beckerman reported that this exception was something being thrown
          // by the call to Close - following seemed to handle the problem
          try
          {
            RequestContext.Response.OutputStream.Flush();
            System.Threading.Thread.Sleep(10000);
            RequestContext.Response.OutputStream.Close();
          }
          catch (Exception)
          {
          }
        }
      }
    }

    public bool SendChunked
    {
      get { return _sendChunked; }
      set { _sendChunked = value; }
    }
  }
}

#endif
