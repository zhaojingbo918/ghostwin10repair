using System;

namespace GhostWin10Repair
{
    public class CcpFileInfo
    {
        private string m_FullName;

        public string FullName { get; set; }

        public DateTime EditTime { get; set; }

        /// <summary>
        /// 获取当前文件的大小（字节）。
        /// </summary>
        public double Size { get; set; }

        public string RelativePath { get; set; }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}