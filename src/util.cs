namespace CookComputing.XmlRpc
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Text;

  public class Util
  {
    // protect constructor because static only class
    protected Util()
    {
    }

    static public void CopyStream(Stream src, Stream dst)
    {
      byte[] buff = new byte[4096];
      while (true)
      {
        int read = src.Read(buff, 0, 4096);
        if (read == 0)
          break;
        dst.Write(buff, 0, read);
      }
    }

    public static Stream StringAsStream(string S)
    {
      MemoryStream mstm = new MemoryStream();
      StreamWriter sw = new StreamWriter(mstm);
      sw.Write(S);
      sw.Flush();
      mstm.Seek(0, SeekOrigin.Begin);
      return mstm;
    }

#if (!COMPACT_FRAMEWORK && !SILVERLIGHT)
    public static void TraceStream(Stream stm)
    {
      TextReader trdr = new StreamReader(stm, new UTF8Encoding(), true, 4096);
      String s = trdr.ReadLine();
      while (s != null)
      {
        Trace.WriteLine(s);
        s = trdr.ReadLine();
      }
    }

    public static void DumpStream(Stream stm)
    {
      TextReader trdr = new StreamReader(stm);
       String s = trdr.ReadLine();
      while (s != null)
      {
        Trace.WriteLine(s);
        s = trdr.ReadLine();
      }
    }
#endif

//#if (!COMPACT_FRAMEWORK)
    public static Guid NewGuid()
    {
      return Guid.NewGuid();
    }
//#else
//   public static Guid NewGuid()
//    {
//      return OpenNETCF.GuidEx.NewGuid();
//    }
//#endif
  }

}
