using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace sparkim_passview
{
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Spark IM Password Decrypter - Copyright 2012 Adam Caudill <adam@adamcaudill.com>");
        Console.WriteLine(string.Format("v{0} - https://github.com/adamcaudill/sparkim-passview",
          Assembly.GetExecutingAssembly().GetName().Version));
        Console.WriteLine();

        string search;
        if (args.Length == 0)
        {
          search = @"C:\";
        }
        else
        {
          search = string.Format(@"\\{0}\c$\", args[0]);
        }

        var paths = _GetPaths(search);
        foreach (var path in paths)
        {
          var settings = new Settings(path);
          var password = settings.GetPassword();

          if (password != null)
          {
            Console.WriteLine(string.Format("Result! User: '{0}' Pass: '{1}'", settings.GetUser(),
              Encryption.Decrypt(password)));
          }
        }

        Console.WriteLine("Done.");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    private static List<string> _GetPaths(string search)
    {
      const string FILE_NAME = @"AppData\Roaming\Spark\spark.properties";
      var ret = new List<string>();
      string working;

      //search "Users"
      working = Path.Combine(search, "Users");
      if (Directory.Exists(working))
      {
        foreach (var path in Directory.GetDirectories(working))
        {
          var file = Path.Combine(path, FILE_NAME);
          if (File.Exists(file))
          {
            ret.Add(file);
          }
        }
      }

      return ret;
    }
  }
}
