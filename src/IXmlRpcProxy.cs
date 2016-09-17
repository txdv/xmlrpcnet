using System;
using System.ComponentModel;
using System.Collections;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CookComputing.XmlRpc
{
  public interface IXmlRpcProxy
  {
    bool AllowAutoRedirect { get; set; }

    void AttachLogger(XmlRpcLogger logger);

#if (!COMPACT_FRAMEWORK && !SILVERLIGHT && !SILVERLIGHT)
    X509CertificateCollection ClientCertificates { get; }
#endif

#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
    string ConnectionGroupName { get; set; }
#endif

    CookieContainer CookieContainer { get; }

    [Browsable(false)]
    ICredentials Credentials { get; set; }

#if (!COMPACT_FRAMEWORK && !FX1_0 && !SILVERLIGHT)
    bool EnableCompression { get; set;}

    bool Expect100Continue { get; set; }

    bool UseNagleAlgorithm { get; set; }
#endif

    [Browsable(false)]
    WebHeaderCollection Headers { get; }

    Guid Id { get; }

    int Indentation { get; set; }

    bool KeepAlive { get; set; }

    XmlRpcNonStandard NonStandard { get; set; }

    bool PreAuthenticate { get; set; }

#if (!SILVERLIGHT)
    [Browsable(false)]
    System.Version ProtocolVersion { get; set; }
#endif

#if (!SILVERLIGHT)
    [Browsable(false)]
    IWebProxy Proxy { get; set; }
#endif

    [Browsable(false)]
    CookieCollection ResponseCookies { get; }

    [Browsable(false)]
    WebHeaderCollection ResponseHeaders { get; }

    int Timeout { get; set; }

    string Url { get; set; }

    bool UseEmptyElementTags { get; set; }

    bool UseEmptyParamsTag { get; set; }

    bool UseIndentation { get; set; }

    bool UseIntTag { get; set; }

    bool UseStringTag { get; set; }

    string UserAgent { get; set; }

    [Browsable(false)]
    Encoding XmlEncoding { get; set; }

    string XmlRpcMethod { get; set; }

    // introspecton methods
    string[] SystemListMethods();
    object[] SystemMethodSignature(string MethodName);
    string SystemMethodHelp(string MethodName);

    // events
    event XmlRpcRequestEventHandler RequestEvent;
    event XmlRpcResponseEventHandler ResponseEvent;

  }
}
