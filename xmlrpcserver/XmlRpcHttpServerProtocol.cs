using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace CookComputing.XmlRpc
{
  public class XmlRpcHttpServerProtocol : XmlRpcServerProtocol,
    IHttpRequestHandler
  {
    public void HandleHttpRequest(
      IHttpRequest httpReq,
      IHttpResponse httpResp)
    {
      // GET has its own handler because it can be used to return a
      // HTML description of the service
      if (httpReq.HttpMethod == "GET")
      {
        XmlRpcServiceAttribute svcAttr = (XmlRpcServiceAttribute)
          Attribute.GetCustomAttribute(GetType(), typeof(XmlRpcServiceAttribute));
        if (svcAttr != null && svcAttr.AutoDocumentation == false)
        {
          HandleUnsupportedMethod(httpReq, httpResp);
        }
        else
        {
          bool autoDocVersion = true;
          if (svcAttr != null)
            autoDocVersion = svcAttr.AutoDocVersion;
          HandleGET(httpReq, httpResp, autoDocVersion);
        }
        return;
      }
      // calls on service methods are via POST
      if (httpReq.HttpMethod != "POST")
      {
        HandleUnsupportedMethod(httpReq, httpResp);
        return;
      }
      //Context.Response.AppendHeader("Server", "XML-RPC.NET");
      // process the request
      Stream responseStream = Invoke(httpReq.InputStream);
      httpResp.ContentType = "text/xml";
      if (!httpResp.SendChunked)
      {
        httpResp.ContentLength = responseStream.Length;
      }
      Stream respStm = httpResp.OutputStream;
      Util.CopyStream(responseStream, respStm);
      respStm.Flush();
    }

    protected void HandleGET(
      IHttpRequest httpReq,
      IHttpResponse httpResp,
      bool autoDocVersion)
    {
      using (MemoryStream stm = new MemoryStream())
      {
        using (HtmlTextWriter wrtr = new HtmlTextWriter(new StreamWriter(stm)))
        {
          XmlRpcDocWriter.WriteDoc(wrtr, this.GetType(), autoDocVersion);
          wrtr.Flush();
          httpResp.ContentType = "text/html";
          httpResp.StatusCode = 200;
          if (!httpResp.SendChunked)
          {
            httpResp.ContentLength = stm.Length;
          }
          stm.Position = 0;
          Stream respStm = httpResp.OutputStream;
          Util.CopyStream(stm, respStm);
          respStm.Flush();
        }
      }
    }

    protected void HandleUnsupportedMethod(
      IHttpRequest httpReq,
      IHttpResponse httpResp)
    {
      // RFC 2068 error 405: "The method specified in the Request-Line
      // is not allowed for the resource identified by the Request-URI.
      // The response MUST include an Allow header containing a list
      // of valid methods for the requested resource."
      //!! add Allow header
      httpResp.StatusCode = 405;
      httpResp.StatusDescription = "Unsupported HTTP verb";
    }

  }
}
