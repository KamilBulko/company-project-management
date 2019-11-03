using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Xml;

namespace DAL
{
    public class Class1
    {

        public XmlReader GetXmlReader()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            string scoresFile = HttpContext.Current.Server.MapPath("projects.xml");
            XmlReader reader = XmlReader.Create(scoresFile, settings);
            return reader;
        }

    }

}
