using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace sparkim_passview
{
  internal class Settings
  {
    private Dictionary<string, string> _data = new Dictionary<string, string>();
    
    public Settings(string file)
    {
      var ret = File.ReadAllLines(file);

      foreach (var s in ret)
      {
        var val = s.Replace(@"\=", "¢");

        if (s.Contains("="))
        {
          var key = val.Split('=')[0].Replace("¢", @"=");
          var value = val.Split('=')[1].Replace("¢", @"=");

          _data.Add(key, value);
        }
      }
    }
    
    public string GetUser()
    {
      if (_data.ContainsKey("username"))
        return _data["username"];
      else
        return null;
    }

    public string GetServer()
    {
      if (_data.ContainsKey("server"))
        return _data["server"];
      else
        return null;
    }

    public string GetPassword()
    {
      var key = "password" + Encryption.Encrypt(string.Format("{0}@{1}", GetUser(), GetServer()));

      if (_data.ContainsKey(key))
        return _data[key];
      else
        return null;
    }
  }
}
