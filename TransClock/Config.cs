using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransClock
{
    public class Config
    {
        public Config(string StrFile)
        {
            _StrFile = StrFile;
            _Doc.Load(_StrFile);
        }
        public string GetSettingDefault(string StrKey, string StrDefault)
        {
            XmlNode Node = _Doc.SelectSingleNode("configuration/appSettings/add[@key='" + StrKey + "']");
            if (Node == null)
                return StrDefault;
            return ReadWithDefault(Node.Attributes["value"].Value, StrDefault);
        }
        public void SetSetting(string StrKey, string StrValue)
        {
            XmlNode Node = _Doc.SelectSingleNode("configuration/appSettings/add[@key='" + StrKey + "']");
            Node.Attributes["value"].Value = StrValue;
            _Doc.Save(_StrFile);
        }
        string _StrFile;
        XmlDocument _Doc = new XmlDocument();

        public static string ReadWithDefault(string StrValue, string StrDefault)
        {
            return (StrValue == null) ? StrDefault : StrValue;
        }
    }
}
