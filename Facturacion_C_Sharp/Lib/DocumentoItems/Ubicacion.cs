using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Ubicacion : IXMLGenerador
    {
        private String provincia;
        private String canton;
        private String distrito;
        private String otrasSegnas;

        //barrio puede estar en blanco
        private String barrio;

        public Ubicacion(string provincia, string canton, string distrito, string otrasSegnas,
                         string barrio = "")
        {
            this.provincia = provincia;
            this.canton = canton;
            this.distrito = distrito;
            this.otrasSegnas = otrasSegnas;
            this.barrio = barrio;
        }

        public string Provincia { get => provincia; set => provincia = value; }
        public string Canton { get => canton; set => canton = value; }
        public string Distrito { get => distrito; set => distrito = value; }
        public string OtrasSegnas { get => otrasSegnas; set => otrasSegnas = value; }
        public string Barrio { get => barrio; set => barrio = value; }

        //    validates :province, presence: true, length: { is: 1 }
        //    validates :county, presence: true, length: { is: 2 }
        //    validates :district, presence: true, length: { is: 2 }
        //    validates :neighborhood, length: { is: 2 }, allow_blank: true
        //    validates :others, presence: true, length: { maximum: 160 }

        //def initialize(args={ })
        //  @province = args[:province]
        //  @county = args[:county]
        //  @district = args[:district]
        //  @neighborhood = args[:neighborhood]
        //  @others = args[:others]
        //end


        public XElement GenerarXML()
        {
            var baseXML = new XElement("Ubicacion",
                                       new XElement("Provincia", provincia),
                                       new XElement("Canton", canton),
                                       new XElement("Distrito", distrito));
            if (!barrio.Equals(""))
            {
                baseXML.Add(new XElement("Barrio", barrio));
            }

            baseXML.Add(new XElement("OtrasSenas", otrasSegnas));

            return baseXML;
        }
    }
}
