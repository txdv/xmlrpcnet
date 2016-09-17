namespace CookComputing.XmlRpc
{
  using System;

  public enum EnumMapping
  {
    Number,
    String
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Struct
     | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Parameter
     | AttributeTargets.Method | AttributeTargets.ReturnValue
     | AttributeTargets.Interface)]
  public class XmlRpcEnumMappingAttribute : Attribute
  {
    public XmlRpcEnumMappingAttribute()
    {
    }

    public XmlRpcEnumMappingAttribute(EnumMapping mapping)
    {
      _mapping = mapping;
    }

    public EnumMapping Mapping
    {
      get
      { return _mapping; }
    }

    public override string ToString()
    {
      string value = _mapping.ToString();
      return value;
    }

    private EnumMapping _mapping = EnumMapping.Number;
  }
}
