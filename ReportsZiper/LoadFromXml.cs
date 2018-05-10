using System.Text;
using System.Xml;
using System.IO;

namespace ReportsZiper
{
    public static class LoadFromXml
    {
        public static XmlDocument xmlDocument;


        public static LoadXmlResult mLoadFromXML()
        {
            LoadXmlResult loadXmlResult = new LoadXmlResult();

            xmlDocument = new XmlDocument();    //creates xml doc
            try
            {
                xmlDocument.Load("Config.xml"); //loads config file to xml doc
            }
            catch
            {
               
            }

            XmlNodeList pathsList = xmlDocument.GetElementsByTagName("path");
            foreach (XmlNode path in pathsList)      //creates a list of paths to folders
            {
                loadXmlResult.PathList.Add(path.InnerText.ToString());
            }

            /*
            XmlNodeList freq = xmlDocument.GetElementsByTagName("frequency");
            loadXmlResult.frequency = freq[0].InnerText.ToString();
            */

            return loadXmlResult;
        }
    }
}