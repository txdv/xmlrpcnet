// TODO: overriding default mapping action in a struct should not affect nested structs

namespace CookComputing.XmlRpc
{
  using System;
  using System.Collections;
  using System.Globalization;
  using System.IO;
  using System.Reflection;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Xml;
  using System.Collections.Generic;

  public class XmlRpcResponseSerializer : XmlRpcSerializer
  {
    public XmlRpcResponseSerializer() { }
    public XmlRpcResponseSerializer(XmlRpcFormatSettings settings) : base(settings) { }

    public void SerializeResponse(Stream stm, XmlRpcResponse response)
    {
      Object ret = response.retVal;
      if (ret is XmlRpcFaultException)
      {
        SerializeFaultResponse(stm, (XmlRpcFaultException)ret);
        return;
      }
      XmlWriter xtw = XmlRpcXmlWriter.Create(stm, base.XmlRpcFormatSettings);
      xtw.WriteStartDocument();
      xtw.WriteStartElement("", "methodResponse", "");
      xtw.WriteStartElement("", "params", "");
      xtw.WriteStartElement("", "param", "");
      var mappingActions = new MappingActions();
      mappingActions = GetTypeMappings(response.MethodInfo, mappingActions);
      mappingActions = GetReturnMappingActions(response, mappingActions);
      try
      {
        Serialize(xtw, ret, mappingActions);
      }
      catch (XmlRpcUnsupportedTypeException ex)
      {
        throw new XmlRpcInvalidReturnType(string.Format(
          "Return value is of, or contains an instance of, type {0} which "
          + "cannot be mapped to an XML-RPC type", ex.UnsupportedType));
      }
      WriteFullEndElement(xtw);
      WriteFullEndElement(xtw);
      WriteFullEndElement(xtw);
      xtw.Flush();
    }

    MappingActions GetReturnMappingActions(XmlRpcResponse response,
      MappingActions mappingActions)
    {
      var ri = response.MethodInfo != null ? response.MethodInfo.ReturnParameter : null;
      return GetMappingActions(ri, mappingActions);
    }
  }
}
