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
  using System.Diagnostics;
  using System.Collections.Generic;


  struct Fault
  {
    public int faultCode;
    public string faultString;
  }

  public class XmlRpcResponseDeserializer : XmlRpcDeserializer
  {
    public XmlRpcResponse DeserializeResponse(Stream stm, Type svcType)
    {
      if (stm == null)
        throw new ArgumentNullException("stm",
          "XmlRpcSerializer.DeserializeResponse");
      if (AllowInvalidHTTPContent)
      {
        Stream newStm = new MemoryStream();
        Util.CopyStream(stm, newStm);
        stm = newStm;
        stm.Position = 0;
        while (true)
        {
          // for now just strip off any leading CR-LF characters
          int byt = stm.ReadByte();
          if (byt == -1)
            throw new XmlRpcIllFormedXmlException(
              "Response from server does not contain valid XML.");
          if (byt != 0x0d && byt != 0x0a && byt != ' ' && byt != '\t')
          {
            stm.Position = stm.Position - 1;
            break;
          }
        }
      }
      XmlReader xmlRdr = XmlRpcXmlReader.Create(stm);
      return DeserializeResponse(xmlRdr, svcType);
    }

    public XmlRpcResponse DeserializeResponse(TextReader txtrdr, Type svcType)
    {
      if (txtrdr == null)
        throw new ArgumentNullException("txtrdr",
          "XmlRpcSerializer.DeserializeResponse");
      XmlReader xmlRdr = XmlRpcXmlReader.Create(txtrdr);
      return DeserializeResponse(xmlRdr, svcType);
    }

    public XmlRpcResponse DeserializeResponse(XmlReader rdr, Type returnType)
    {
      try
      {

        IEnumerator<Node> iter = new XmlRpcParser().ParseResponse(rdr).GetEnumerator();
        iter.MoveNext();
        if (iter.Current is FaultNode)
        {
          var xmlRpcException = DeserializeFault(iter);
          throw xmlRpcException;
        }
        if (returnType == typeof(void) || !iter.MoveNext())
          return new XmlRpcResponse { retVal = null };
        var valueNode = iter.Current as ValueNode;
        object retObj = MapValueNode(iter, returnType, new MappingStack("response"),
          MappingAction.Error);
        var response = new XmlRpcResponse { retVal = retObj };
        return response;
      }
      catch (XmlException ex)
      {
        throw new XmlRpcIllFormedXmlException("Response contains invalid XML", ex);
      }
    }

    private XmlRpcException DeserializeFault(IEnumerator<Node> iter)
    {
      MappingStack faultStack = new MappingStack("fault response");
      // TODO: use global action setting
      MappingAction mappingAction = MappingAction.Error;
      XmlRpcFaultException faultEx = ParseFault(iter, faultStack, // TODO: fix
        mappingAction);
      throw faultEx;
    }

    XmlRpcFaultException ParseFault(
    IEnumerator<Node> iter,
    MappingStack parseStack,
    MappingAction mappingAction)
    {
      iter.MoveNext();  // move to StructValue
      Type parsedType;
      var faultStruct = MapHashtable(iter, null, parseStack, mappingAction,
        out parsedType) as XmlRpcStruct;
      object faultCode = faultStruct["faultCode"];
      object faultString = faultStruct["faultString"];
      if (faultCode is string)
      {
        int value;
        if (!Int32.TryParse(faultCode as string, out value))
          throw new XmlRpcInvalidXmlRpcException("faultCode not int or string");
        faultCode = value;
      }
      return new XmlRpcFaultException((int)faultCode, (string)faultString);
    }

    struct FaultStruct
    {
      public int faultCode;
      public string faultString;
    }

    struct FaultStructStringCode
    {
      public string faultCode;
      public string faultString;
    }

  }
}


