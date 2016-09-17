using System;
using System.IO;

namespace CookComputing.XmlRpc
{
  public class RequestResponseLogger : XmlRpcLogger
  {
    string _directory = ".";

    public string Directory
    {
      get { return _directory; }
      set { _directory = Path.GetDirectoryName(value + "/"); }
    }

    protected override void OnRequest(object sender, XmlRpcRequestEventArgs e)
    {
      string fname = string.Format("{0}/{1}-{2:0000}-request-{3}.xml",
        _directory, DateTime.Now.Ticks, e.RequestNum, e.ProxyID);
      FileStream fstm = new FileStream(fname, FileMode.Create);
      Util.CopyStream(e.RequestStream, fstm);
      fstm.Close();
    }

    protected override void OnResponse(object sender, XmlRpcResponseEventArgs e)
    {
      string fname = string.Format("{0}/{1}-{2:0000}-response-{3}.xml",
        _directory, DateTime.Now.Ticks, e.RequestNum, e.ProxyID);
      FileStream fstm = new FileStream(fname, FileMode.Create);
      Util.CopyStream(e.ResponseStream, fstm);
      fstm.Close();
    }
  }
}
