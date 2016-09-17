namespace CookComputing.XmlRpc
{
  using System;

  public enum NullMappingAction
  {
    Error,
    Ignore,
    Nil
  }

  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Struct
     | AttributeTargets.Property | AttributeTargets.Class)]
  public class XmlRpcNullMappingAttribute : Attribute
  {
    public XmlRpcNullMappingAttribute()
    {
    }

    public XmlRpcNullMappingAttribute(NullMappingAction action)
    {
      _action = action;
    }

    public NullMappingAction Action
    {
      get
      { return _action; }
    }

    public override string ToString()
    {
      string value = _action.ToString();
      return value;
    }

    private NullMappingAction _action = NullMappingAction.Error;
  }
}
