using System;
using System.Xml.Linq;

namespace Facturacion_C_Sharp.Utils
{
    public interface IXMLGenerador
    {
        XElement GenerarXML();
    }
}
