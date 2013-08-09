using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CSScriptNpp
{
    public class IniFile
    {
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retval, int size, string file);

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string value, string file);

        protected string file;

        public IniFile()
        {
        }

        public IniFile(string file)
        {
            this.file = file;
        }

        public void SetValue<T>(string section, string key, T value)
        {
            WritePrivateProfileString(section, key, value.ToString(), file);
        }

        public T GetValue<T>(string section, string key, T defaultValue, int size = 255)
        {
            try
            {
                var retval = new StringBuilder(size);
                GetPrivateProfileString(section, key, defaultValue.ToString(), retval, size, file);
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(retval.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }
    }

}