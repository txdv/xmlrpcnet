namespace CookComputing.XmlRpc
{
  using System;
#if (!COMPACT_FRAMEWORK)
  using System.Runtime.Serialization;
#endif

  // used to return server-side errors to client code - also can be
  // thrown by Service implmentation code to return custom Fault Responses
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
  [Serializable]
#endif
  public class XmlRpcFaultException :
#if (!SILVERLIGHT)
    ApplicationException
#else
    Exception
#endif
  {
    // constructors
    //
    public XmlRpcFaultException(int TheCode, string TheString)
      : base("Server returned a fault exception: [" + TheCode.ToString() +
              "] " + TheString)
    {
      m_faultCode = TheCode;
      m_faultString = TheString;
    }
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
    // deserialization constructor
    protected XmlRpcFaultException(
      SerializationInfo info,
      StreamingContext context)
      : base(info, context)
    {
      m_faultCode = (int)info.GetValue("m_faultCode", typeof(int));
      m_faultString = (String)info.GetValue("m_faultString", typeof(string));
    }
#endif
    // properties
    //
    public int FaultCode
    {
      get { return m_faultCode; }
    }

    public string FaultString
    {
      get { return m_faultString; }
    }
#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
    // public methods
    //
    public override void GetObjectData(
      SerializationInfo info,
      StreamingContext context)
    {
      info.AddValue("m_faultCode", m_faultCode);
      info.AddValue("m_faultString", m_faultString);
      base.GetObjectData(info, context);
    }
#endif
    // data
    //
    int m_faultCode;
    string m_faultString;
  }
}
