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

        public static List<CcpFileInfo> CompairFolder(string path1, string path2)
        {
            try
            {
                System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(path1);
                System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(path2);

                IEnumerable<CcpFileInfo> list1 = GetFilesFromFolder(path1);
                IEnumerable<CcpFileInfo> list2 = GetFilesFromFolder(path2);

                list1 = list1.OrderBy(p => p.FullName);
                list2 = list2.OrderBy(p => p.FullName);

                FileCompare myFileCompare = new FileCompare();
                var queryCommonFiles = list1.Intersect(list2, myFileCompare).ToList();


                var filesInList1Only = list1.Except(queryCommonFiles).ToList();

                return filesInList1Only;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<CcpFileInfo>();
        }



        /// <summary>
        /// 从目录中读取文件列表，包括当前目录和所有的子目录
        /// </summary>
        /// <param name="dir">当前目录</param>
        /// <returns></returns>
        public static List<CcpFileInfo> GetFilesFromFolder(string dir)
        {
            var list = new List<CcpFileInfo>();
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            var files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);

            var dic = dirInfo.FullName + "\\";

            foreach (FileInfo theFile in files)
            {
                CcpFileInfo ccpFileInfo = new CcpFileInfo()
                {
                    FullName = theFile.FullName,
                    EditTime = theFile.LastWriteTime,
                    Size = theFile.Length,
                    RelativePath = theFile.FullName.Replace(dic, "")
                };
                list.Add(ccpFileInfo);
            }
            return list;
        }

        // This implementation defines a very simple comparison  
        // between two FileInfo objects. It only compares the name  
        // of the files being compared and their length in bytes.  
        private class FileCompare : System.Collections.Generic.IEqualityComparer<CcpFileInfo>
        {
            public FileCompare() { }

            public bool Equals(CcpFileInfo f1, CcpFileInfo f2)
            {


                bool result = (f1.RelativePath == f2.RelativePath &&
                        f1.Size == f2.Size && f1.EditTime == f2.EditTime);

                return result;
            }

            // Return a hash that reflects the comparison criteria. According to the   
            // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
            // also be equal. Because equality as defined here is a simple value equality, not  
            // reference identity, it is possible that two or more objects will produce the same  
            // hash code.  
            public int GetHashCode(CcpFileInfo fi)
            {
                string s = String.Format("{0}{1}", fi.RelativePath, fi.Size);
                return s.GetHashCode();
            }
        }
    }
}
