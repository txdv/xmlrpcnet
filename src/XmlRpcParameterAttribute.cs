namespace CookComputing.XmlRpc
{
  using System;

  [AttributeUsage(AttributeTargets.Parameter)]
  public class XmlRpcParameterAttribute : Attribute
  {
    public XmlRpcParameterAttribute()
    {
    }

    public XmlRpcParameterAttribute(string name)
    {
      this.name = name;
    }

    public string Name
    {
      get { return name; }
    }

    public string Description
    {
      get { return description; }
      set { description = value; }
    }

    public override string ToString()
    {
      string value = "Description : " + description;
      return value;
    }

    private string name = "";
    private string description = "";
  }
}
