using System;

namespace CookComputing.XmlRpc
{

  public interface IHttpRequestHandler
  {
    void HandleHttpRequest(IHttpRequest httpReq, IHttpResponse httpResp);
  }

}
