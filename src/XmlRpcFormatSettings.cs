using System;
using System.Collections.Generic;
using System.Text;

namespace CookComputing.XmlRpc
{
  public class XmlRpcFormatSettings
  {
    public int Indentation
    {
      get { return m_indentation; }
      set { m_indentation = value; }
    }
    int m_indentation = 2;

    public bool UseEmptyElementTags
    {
      get { return m_bUseEmptyElementTag; }
      set { m_bUseEmptyElementTag = value; }
    }
    bool m_bUseEmptyElementTag = true;

    public bool UseEmptyParamsTag
    {
      get { return m_bUseEmptyParamsTag; }
      set { m_bUseEmptyParamsTag = value; }
    }
    bool m_bUseEmptyParamsTag = true;

    public bool UseIndentation
    {
      get { return m_bUseIndentation; }
      set { m_bUseIndentation = value; }
    }
    bool m_bUseIndentation = true;

    public bool UseIntTag
    {
      get { return m_useIntTag; }
      set { m_useIntTag = value; }
    }
    bool m_useIntTag;

    public bool UseStringTag
    {
      get { return m_useStringTag; }
      set { m_useStringTag = value; }
    }
    bool m_useStringTag = true;

    public Encoding XmlEncoding
    {
      get { return m_encoding; }
      set { m_encoding = value; }
    }
    Encoding m_encoding = null;

    public bool OmitXmlDeclaration { get; set; }
  }
}
