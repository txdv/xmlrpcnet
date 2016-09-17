using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace CookComputing.XmlRpc
{
  public class XmlRpcClientFormatterSinkProvider : IClientFormatterSinkProvider
  {
    // constructors
    //
    public XmlRpcClientFormatterSinkProvider(
      IDictionary properties,
      ICollection providerData)
    {
      // this constructor required when registering via a config file
    }

    public XmlRpcClientFormatterSinkProvider()
    {
      // this constructor used when registering provider programmatically
    }

    // public methods
    //
    public IClientChannelSink CreateSink(
      IChannelSender channel,
      string url,
      object remoteChannelData)
    {
      IClientChannelSink ccs = null;
      if (m_next != null)
      {
        ccs = m_next.CreateSink(channel, url, remoteChannelData);
        if (ccs == null)
          return null;
      }
      return new XmlRpcClientFormatterSink(ccs);
    }

    public IClientChannelSinkProvider Next
    {
      get { return m_next; }
      set { m_next = value; }
    }

    // data
    //
    IClientChannelSinkProvider m_next;
  }
}

