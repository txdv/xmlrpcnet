using System;
using System.IO;

namespace CookComputing.XmlRpc
{

  public interface IHttpRequest
  {
    Stream InputStream { get; }
    string HttpMethod { get; }
  }

}
