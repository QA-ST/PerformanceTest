using SmartBuildProductionAutomation.Helper;
using System;
using System.IO;

namespace SmartBuildAutomation.Helper
{
    public class FolderPath : BaseClass
    {
        private static string SolutionRoot()
        {
            var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (var i = 0; i < 3 && dirInfo.Parent != null; ++i)
            {
                dirInfo = dirInfo.Parent;
            }

            return dirInfo.FullName;
        }

        public static bool CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string Download()
        {
            var path = SolutionRoot();
            return Path.Combine(path, "Folder", "Downloads");
        }

        public static string PerformanceReport()
        {
            var path = SolutionRoot();
            return Path.Combine(path, "Excel Sheet And txt file");
        }
    }
}