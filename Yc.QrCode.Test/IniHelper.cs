using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Collections;

namespace Yc.QrCode.Test
{
    public class IniHelper
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region 字段与属性
        private string _iniFileFullPath;
        public string ls_iniFileFullPath
        {
            set { _iniFileFullPath = value; }
            get { return _iniFileFullPath; }
        }
        #endregion

        /// <summary>
        /// 写入数据到INI文件中
        /// <PARAM name="iniSection">ini节点名称</PARAM>
        /// <PARAM name="iniKey">iniKEY名称</PARAM>
        /// <PARAM name="iniValue">写入的值</PARAM>
        /// </summary>
        public void Write(string iniSection, string iniKey, string iniValue)
        {
            WritePrivateProfileString(iniSection, iniKey, iniValue, this.ls_iniFileFullPath);
        }
        /// <summary>
        ///从ini文件中读取数据
        /// <PARAM name="iniSection">ini节点名称</PARAM>
        /// <PARAM name="iniKey">iniKEY名称</PARAM>
        /// <returns>指定ini节点或iniKEY的值</returns>
        /// </summary>
        public string Read(string iniSection, string iniKey)
        {
            StringBuilder resultValue = new StringBuilder(65535);
            int i = GetPrivateProfileString(iniSection, iniKey, "", resultValue, 65535, this.ls_iniFileFullPath);
            return resultValue.ToString();
        }
        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="section">要删除的节点名</param>
        /// <returns>删除成功或节点不存在，返回值都为真</returns>
        public bool DeleteSection(string section)
        {
            bool flag = false;//标志
            try
            {
                if (section.Trim().Length <= 0)
                {//找不到节点
                    flag = false;
                }
                else
                {
                    if (WritePrivateProfileString(section, null, null, this.ls_iniFileFullPath) == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 删除指定节点下的KEY值
        /// </summary>
        /// <param name="section">指定节点</param>
        /// <param name="key">指定节点下的key</param>
        /// <returns>删除成功或不存在时都返回true</returns>
        public bool DeleteKey(string section, string key)
        {
            bool flag = false;
            try
            {
                if (section.Trim().Length <= 0 || key.Trim().Length <= 0)
                {
                    flag = false;
                }
                else
                {
                    if (WritePrivateProfileString(section, key, null, this.ls_iniFileFullPath) == 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}
