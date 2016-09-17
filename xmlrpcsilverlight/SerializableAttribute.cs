namespace CookComputing.XmlRpc
{
  using System;

  [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
  public class SerializableAttribute : Attribute
  {
  }
}
