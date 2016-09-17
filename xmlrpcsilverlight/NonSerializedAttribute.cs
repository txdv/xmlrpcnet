namespace CookComputing.XmlRpc
{
  using System;

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
  public class NonSerializedAttribute : Attribute
  {
    public NonSerializedAttribute()
    {
    }
  }
}
