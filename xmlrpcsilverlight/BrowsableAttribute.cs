namespace CookComputing.XmlRpc
{
  using System;

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
  public class BrowsableAttribute : Attribute
  {
    public BrowsableAttribute()
    {
    }

    public BrowsableAttribute(bool val)
    {
    }
  }
}
