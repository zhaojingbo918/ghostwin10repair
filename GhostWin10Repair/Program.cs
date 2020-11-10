using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostWin10Repair
{
    class Program
    {
        static void Main(string[] args)
        {
            //string sourceFolder = ccpPackageSettingInfo.DirectoryName;
            //string targetFolder = System.IO.Path.Combine(AVCServerConfiguration.Instance.ApplicationsFolder, appname);
            var source = @"SourceFiles\Windows";
            var target = @"C:\Windows";

            RepairFolder(source, target);

         
        }



        static void RepairFolder(string sourceFolder, string targetFolder)
        {
            targetFolder = targetFolder.Replace(@"C:\", @"D:\test1110\");
            List<CcpFileInfo> sourceFiles = FolderCompareHelper.GetFilesFromFolder(sourceFolder);

            //1,复制文件
            for (int i = 0; i < sourceFiles.Count; i++)
            {
                var file = sourceFiles[i];
                string targetFilepath = System.IO.Path.Combine(targetFolder, file.RelativePath);

                if (!File.Exists(targetFilepath))
                {
                    CreateDir(targetFilepath);
                    File.Copy(file.FullName, targetFilepath, true);
                }
            }

        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="filefullpath"></param>
        private static void CreateDir(string filefullpath)
        {
            try
            {
                string dirpath = filefullpath.Substring(0, filefullpath.LastIndexOf('\\'));

                if (Directory.Exists(dirpath))
                {
                    return;
                }

                string[] pathes = dirpath.Split('\\');
                if (pathes.Length > 1)
                {
                    string path = pathes[0];
                    for (int i = 1; i < pathes.Length; i++)
                    {
                        path += "\\" + pathes[i];
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建目录失败{ex.Message}");
            }
        }
    }
}
