using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

using CookComputing.XmlRpc;

namespace CookComputing.XmlRpc
{
  public class XmlRpcClientFormatterSink : IClientChannelSink, IMessageSink
  {
    // constructors
    //
    public XmlRpcClientFormatterSink(
      IClientChannelSink NextSink)
    {
      m_next = NextSink;
    }

    // properties
    //
    public IClientChannelSink NextChannelSink
    {
      get { return m_next; }
    }

    public IMessageSink NextSink
    {
      get {  throw new NotSupportedException(); }
    }

    public IDictionary Properties
    {
      get {  return null; }
    }

    //  public methods
    //
    public IMessageCtrl AsyncProcessMessage(
      IMessage msg,
      IMessageSink replySink)
    {
      throw new NotSupportedException();
    }

    public void AsyncProcessRequest(
      IClientChannelSinkStack sinkStack,
      IMessage msg,
      ITransportHeaders headers,
      Stream stream)
    {
      throw new Exception("not implemented");
    }

    public void AsyncProcessResponse(
      IClientResponseChannelSinkStack sinkStack,
      object state,
      ITransportHeaders headers,
      Stream stream)
    {
      throw new Exception("not implemented");
    }

    public Stream GetRequestStream(
      IMessage msg,
      ITransportHeaders headers)
    {
      return null; // TODO: ???
    }

    public void ProcessMessage(
      IMessage msg,
      ITransportHeaders requestHeaders,
      Stream requestStream,
      out ITransportHeaders responseHeaders,
      out Stream responseStream)
    {
      responseHeaders = null;
      responseStream = null;
    }

    public IMessage SyncProcessMessage(
      IMessage msg
      )
    {
      IMethodCallMessage mcm = msg as IMethodCallMessage;
      try
      {
        Stream reqStm = null;
        ITransportHeaders reqHeaders = null;
        SerializeMessage(mcm, ref reqHeaders, ref reqStm);

        Stream respStm = null;
        ITransportHeaders respHeaders = null;
        m_next.ProcessMessage(msg, reqHeaders, reqStm,
          out respHeaders, out respStm);

        IMessage imsg = DeserializeMessage(mcm, respHeaders, respStm);
        return imsg;
      }
      catch(Exception ex)
      {
        return new ReturnMessage(ex, mcm);
      }
    }

    //  private methods
    //
    void SerializeMessage(
      IMethodCallMessage mcm,
      ref ITransportHeaders headers,
      ref Stream stream)
    {
      ITransportHeaders reqHeaders = new TransportHeaders();
      reqHeaders["__Uri"] = mcm.Uri;
      reqHeaders["Content-Type"] = "text/xml; charset=\"utf-8\"";
      reqHeaders["__RequestVerb"] = "POST";

      MethodInfo mi = (MethodInfo) mcm.MethodBase;
      string methodName = GetRpcMethodName(mi);
      XmlRpcRequest xmlRpcReq = new XmlRpcRequest(methodName, mcm.InArgs);
      // TODO: possibly call GetRequestStream from next sink in chain?
      // TODO: SoapClientFormatter sink uses ChunkedStream - check why?
      Stream stm = new MemoryStream();
      var serializer = new XmlRpcRequestSerializer();
      serializer.SerializeRequest(stm, xmlRpcReq);
      stm.Position = 0;

      headers = reqHeaders;
      stream = stm;
    }

    IMessage DeserializeMessage(
      IMethodCallMessage mcm,
      ITransportHeaders headers,
      Stream stream)
    {
      var deserializer = new XmlRpcResponseDeserializer();
      object tp = mcm.MethodBase;
      System.Reflection.MethodInfo mi = (System.Reflection.MethodInfo)tp;
      System.Type t = mi.ReturnType;
      XmlRpcResponse xmlRpcResp = deserializer.DeserializeResponse(stream, t);
      IMessage imsg = new ReturnMessage(xmlRpcResp.retVal, null, 0, null, mcm);
      return imsg;
    }

    string GetRpcMethodName(MethodInfo mi)
    {
      Attribute attr = Attribute.GetCustomAttribute(mi,
        typeof(XmlRpcMethodAttribute));
      // TODO: do methods need attribute?
      //      if (attr == null)
      //      {
      //        throw new Exception("missing method attribute");
      //      }
      string rpcMethod = "";
      if (attr != null)
      {
        XmlRpcMethodAttribute xrmAttr = attr as XmlRpcMethodAttribute;
        rpcMethod = xrmAttr.Method;
      }
      if (rpcMethod == "")
        rpcMethod = mi.Name;
      return rpcMethod;
    }

    // data
    //
    IClientChannelSink m_next;
  }
}
