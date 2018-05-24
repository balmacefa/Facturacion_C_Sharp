using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Normativa : IXMLGenerador
    {
        private String numeroResolucion = "DGT-R-48-2016";
        private String fechaResolucion = "20-02-2017 13:22:22";

        public Normativa()
        {
        }
        public Normativa(string numeroResolucion, string fechaResolucion)
        {
            this.numeroResolucion = numeroResolucion;
            this.fechaResolucion = fechaResolucion;
        }

        public string NumeroResolucion { get => numeroResolucion; set => numeroResolucion = value; }
        public string FechaResolucion { get => fechaResolucion; set => fechaResolucion = value; }

        public XElement GenerarXML()
        {
            return new XElement("Normativa",
                                new XElement("NumeroResolucion", numeroResolucion),
                                new XElement("FechaResolucion", fechaResolucion));
        }
    }
}
