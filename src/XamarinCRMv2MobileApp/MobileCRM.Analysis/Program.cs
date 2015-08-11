//
//  Copyright 2014  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MyExpenses.Analysis
{
  class Program
  {
    static void Main(string[] args)
    {
      var path = Environment.CurrentDirectory;
      for (int i = 0; i < 3; i++)
      {
        path = Path.Combine(Path.GetDirectoryName(path), string.Empty);
      }
      var projects = new List<Solution> {


				new Solution {
					Name = "Android",
					ProjectFiles = new List<string> {
						Path.Combine(path, "MobileCRM.Android/MobileCRM.Android.csproj"),
						Path.Combine(path, "MobileCRM.Shared/MobileCRM.Shared.csproj")
					},
				},

				new Solution {
					Name = "iOS",
					ProjectFiles = new List<string> {
						Path.Combine(path, "MobileCRM.iOS/MobileCRM.iOS.csproj"),
						Path.Combine(path, "MobileCRM.Shared/MobileCRM.Shared.csproj")
					},
				},

                new Solution {
					Name = "WP8",
					ProjectFiles = new List<string> {
						Path.Combine(path, "MobileCRM.WindowsPhone/MobileCRM.WindowsPhone.csproj"),
						Path.Combine(path, "MobileCRM.Shared/MobileCRM.Shared.csproj")
					},
				},

			};


      new Program().Run(projects);

      Console.ReadLine();
    }

    class Solution
    {
      public string Name = "";
      public List<string> ProjectFiles = new List<string>();
      public List<FileInfo> CodeFiles = new List<FileInfo>();
      public override string ToString()
      {
        return Name;
      }

      public int UniqueLinesOfCode
      {
        get
        {
          return (from f in CodeFiles
                  where f.Solutions.Count == 1
                  select f.LinesOfCode).Sum();
        }
      }

      public int SharedLinesOfCode
      {
        get
        {
          return (from f in CodeFiles
                  where f.Solutions.Count > 1
                  select f.LinesOfCode).Sum();
        }
      }

      public int TotalLinesOfCode
      {
        get
        {
          return (from f in CodeFiles
                  select f.LinesOfCode).Sum();
        }
      }
    }

    class FileInfo
    {
      public string Path = "";
      public List<Solution> Solutions = new List<Solution>();
      public int LinesOfCode = 0;
      public override string ToString()
      {
        return Path;
      }
    }

    Dictionary<string, FileInfo> _files = new Dictionary<string, FileInfo>();

    void AddRef(string path, Solution sln)
    {

      if (_files.ContainsKey(path))
      {
        _files[path].Solutions.Add(sln);
        sln.CodeFiles.Add(_files[path]);
      }
      else
      {
        var info = new FileInfo { Path = path, };
        info.Solutions.Add(sln);
        _files[path] = info;
        sln.CodeFiles.Add(info);
      }
    }

    void Run(List<Solution> solutions)
    {
      //
      // Find all the files
      //
      foreach (var sln in solutions)
      {
        foreach (var projectFile in sln.ProjectFiles)
        {
          var dir = Path.GetDirectoryName(projectFile);
          var projectName = Path.GetFileNameWithoutExtension(projectFile);
          var doc = XDocument.Load(projectFile);
          var q = from x in doc.Descendants()
                  let e = x as XElement
                  where e != null
                  where e.Name.LocalName == "Compile"
                  where e.Attributes().Any(a => a.Name.LocalName == "Include")
                  select e.Attribute("Include").Value;
          foreach (var inc in q)
          {
            //skip over some things that are added automatically
            if (inc.Contains("Resource.designer.cs") || //auto generated
                inc.Contains("DebugTrace.cs") || //not needed mvvmcross
                inc.Contains("LinkerPleaseInclude.cs") || //not needed mvvmcross
                inc.Contains("AssemblyInfo.cs") || //in every place
              inc.Contains("Bootstrap.cs") || //not needed mvvmcross
              inc.Contains(".designer.cs") || //auto generated, not code
              inc.Contains(".Designer.cs") || //Android designer file
              inc.Contains("App.xaml.cs") || //generic WP setup
                            inc.EndsWith(".xaml") ||
                            inc.EndsWith(".xml") ||
                            inc.EndsWith(".axml"))
            {
              continue;
            }

            var inc2 = inc.Replace("\\", Path.DirectorySeparatorChar.ToString());
            AddRef(Path.GetFullPath(Path.Combine(dir, inc2)), sln);
          }
        }
      }

      //
      // Get the lines of code
      //
      foreach (var f in _files.Values)
      {
        try
        {
          var lines = File.ReadAllLines(f.Path).ToList();

          f.LinesOfCode = lines.Count;
        }
        catch (Exception ex)
        {
        }
      }

      //
      // Output
      //
      Console.WriteLine("app\ttotal\tunique\tshared\tunique%\tshared%");
      foreach (var sln in solutions)
      {

        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4:p}\t{5:p}",
          sln.Name,
          sln.TotalLinesOfCode,
          sln.UniqueLinesOfCode,
          sln.SharedLinesOfCode,
          sln.UniqueLinesOfCode / (double)sln.TotalLinesOfCode,
          sln.SharedLinesOfCode / (double)sln.TotalLinesOfCode);
      }
      Console.WriteLine(string.Empty);
      Console.WriteLine("DONE");
    }
  }
}