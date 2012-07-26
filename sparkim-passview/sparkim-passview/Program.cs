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

        Console.WriteLine(string.Format("Found {0} config files...", paths.Count));

        foreach (var path in paths)
        {
          var settings = new Settings(path);
          var password = settings.GetPassword();

          if (password != null)
          {
            try
            {
              Console.WriteLine(string.Format("Result! User: '{0}' Pass: '{1}'", 
                settings.GetUser(), Encryption.Decrypt(password)));
            }
            catch (Exception ex)
            {
              Console.WriteLine(string.Format("Error! User: '{0}' Error: '{1}'", 
                settings.GetUser(), ex.Message));
            }
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
      const string WIN7_FILE_NAME = @"AppData\Roaming\Spark\spark.properties";
      const string XP_FILE_NAME = @"Spark\spark.properties";
      var ret = new List<string>();

      //search "Users"
      _SearchPaths(Path.Combine(search, "Users"), WIN7_FILE_NAME, ret);

      //search "Documents and Settings"
      _SearchPaths(Path.Combine(search, "Documents and Settings"), XP_FILE_NAME, ret);

      return ret;
    }

    private static void _SearchPaths(string search, string fineName, List<string> ret)
    {
      if (Directory.Exists(search))
      {
        try
        {
          foreach (var path in Directory.GetDirectories(search))
          {
            var file = Path.Combine(path, fineName);
            if (File.Exists(file))
            {
              ret.Add(file);
            }
          }
        }
        catch (UnauthorizedAccessException)
        {
          //gooble gooble
        }
      }
    }
  }
}
