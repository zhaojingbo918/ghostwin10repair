using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostWin10Repair
{

    public class FolderCompareHelper
    {
        /// <summary>
        /// 从目录中读取文件列表，包括当前目录和所有的子目录
        /// </summary>
        /// <param name="dir">当前目录</param>
        /// <returns></returns>
        public static List<CustomFileInfo> GetFilesFromFolder(string dir)
        {
            var list = new List<CustomFileInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            var files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);

            var dic = dirInfo.FullName + "\\";

            foreach (FileInfo theFile in files)
            {
                CustomFileInfo customFileInfo = new CustomFileInfo()
                {
                    FullName = theFile.FullName,
                    EditTime = theFile.LastWriteTime,
                    Size = theFile.Length,
                    RelativePath = theFile.FullName.Replace(dic, "")
                };
                list.Add(customFileInfo);
            }
            return list;
        }
    }
}
