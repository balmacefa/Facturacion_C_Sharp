using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Exoneracion : IXMLGenerador
    {
        public enum TipoDocumento
        {
            [Description("01")]
            Compras_Autorizadas,

            [Description("02")]
            Ventas_exentas_a_diplomáticos,

            [Description("03")]
            Orden_de_Compra_Instituciones_Públicas_y_otros_organismos,

            [Description("04")]
            Exenciones_Dirección_General_de_Hacienda,

            [Description("05")]
            Zonas_Francas,

            [Description("99")]
            Otros
        }

        //attr_accessor :document_type, :document_number, :institution, :date, :total_tax, :percentage, :net_total

        //validates :document_type, presence: true, inclusion: DOCUMENT_TYPES.keys
        //validates :document_number, presence: true
        //validates :institution, presence: true
        //validates :date, presence: true
        //validates :total_tax,presence: true
        //validates :percentage, presence: true

        private TipoDocumento tipoDocumento;
        private int numeroDocumento;
        private String nombreInstitucion;
        private DateTime fechaEmision;
        private decimal montoImpuesto;
        private decimal totalNeto;

        //Dispensable
        private decimal porcentajeCompra;

        public TipoDocumento TipoDocumento1 { get => tipoDocumento; set => tipoDocumento = value; }
        public int NumeroDocumento { get => numeroDocumento; set => numeroDocumento = value; }
        public string NombreInstitucion { get => nombreInstitucion; set => nombreInstitucion = value; }
        public DateTime FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public decimal MontoImpuesto { get => montoImpuesto; set => montoImpuesto = value; }
        public decimal TotalNeto { get => totalNeto; set => totalNeto = value; }
        public decimal PorcentajeCompra { get => porcentajeCompra; set => porcentajeCompra = value; }

        public Exoneracion(TipoDocumento tipoDocumento, int numeroDocumento, string nombreInstitucion, DateTime fechaEmision, decimal montoImpuesto, decimal totalNeto)
        {
            this.tipoDocumento = tipoDocumento;
            this.numeroDocumento = numeroDocumento;
            this.nombreInstitucion = nombreInstitucion;
            this.fechaEmision = fechaEmision;
            this.montoImpuesto = montoImpuesto;
            this.totalNeto = totalNeto;

            this.porcentajeCompra = (montoImpuesto / totalNeto) * 100;
        }

        public XElement GenerarXML()
        {
            var exoneracion = new XElement("Exoneracion",
                                                  new XElement("TipoDocumento", tipoDocumento.ToDescriptionString()),
                                                  new XElement("NumeroDocumento", numeroDocumento),
                                                  new XElement("NombreInstitucion", nombreInstitucion),
                                           new XElement("FechaEmision", fechaEmision.ToRfc3339String()),
                                                  new XElement("MontoImpuesto", montoImpuesto),
                                                  new XElement("PorcentajeCompra", porcentajeCompra)
                                                 );
            return exoneracion;
        }
    }
}
