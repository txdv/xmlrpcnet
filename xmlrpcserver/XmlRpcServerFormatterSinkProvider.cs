using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

using CookComputing.XmlRpc;

namespace CookComputing.XmlRpc
{
  public class XmlRpcServerFormatterSinkProvider : IServerFormatterSinkProvider
  {
    // constructors
    public XmlRpcServerFormatterSinkProvider(
      IDictionary properties,
      ICollection providerData)
    {
      // can use properties to pass in custom attributes from the config
      // file which can then be passed to sink constructor as required
    }

    public XmlRpcServerFormatterSinkProvider()
    {
      // can use properties to pass in custom attributes from the config
      // file which can then be passed to sink constructor as required
    }

    // properties
    //
    public IServerChannelSinkProvider Next
    {
      get { return m_next; }
      set { m_next = value; }
    }

    // public methods
    //
    public IServerChannelSink CreateSink(
      IChannelReceiver channel)
    {
      IServerChannelSink scs = null;
      if (m_next != null)
      {
        scs = m_next.CreateSink(channel);
      }
      return new XmlRpcServerFormatterSink(scs);
    }

    public void GetChannelData(IChannelDataStore channelData)
    {
      // TODO: not required???
    }

    // data
    //
    IServerChannelSinkProvider m_next;
  }
}
