#if (!SILVERLIGHT)

using System;
using System.Diagnostics;
using System.IO;
using CookComputing.XmlRpc;

namespace CookComputing.XmlRpc
{
  public class Tracer : XmlRpcLogger
  {
    protected override void OnRequest(object sender, XmlRpcRequestEventArgs e)
    {
      DumpStream(e.RequestStream);
    }

    protected override void OnResponse(object sender, XmlRpcResponseEventArgs e)
    {
      DumpStream(e.ResponseStream);
    }

    private void DumpStream(Stream stm)
    {
      TextReader trdr = new StreamReader(stm);
      String s = trdr.ReadToEnd();
      Trace.WriteLine(s);
      stm.Position = 0;
    }
  }
}

#endif
