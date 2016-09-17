namespace CookComputing.XmlRpc
{
  using System;
  using System.Web;
  using System.Web.SessionState;

  public class XmlRpcService : XmlRpcHttpServerProtocol,
                                IHttpHandler, IRequiresSessionState
  {
    // note: IRequiresSessionState specifies that the target HTTP handler
    // interface has read and write access to session-state values.
    // This is a marker interface only and has no methods.

    protected HttpContext Context { get { return context; } }

    void IHttpHandler.ProcessRequest(HttpContext RequestContext)
    {
      try
      {
        // store context for access from application code in derived classes
        context = RequestContext;
        // XmlRpc classes delegate to the corresponding Context classes
        XmlRpcHttpRequest httpReq = new XmlRpcHttpRequest(context.Request);
        XmlRpcHttpResponse httpResp = new XmlRpcHttpResponse(context.Response);
        HandleHttpRequest(httpReq, httpResp);
      }
      catch (Exception ex)
      {
        RequestContext.Response.StatusCode = 500;  // "Internal server error"
        RequestContext.Response.StatusDescription = ex.Message;
      }
    }

    bool IHttpHandler.IsReusable
    {
      get { return true; }
    }

    private HttpContext context;
  }
}
