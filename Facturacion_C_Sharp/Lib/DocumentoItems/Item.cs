using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Item : IXMLGenerador
    {
        //      validates :line_number, presence: true
        //      validates :quantity, presence: true, numericality: {greater_than: 0}
        //      validates :unit, presence: true
        //      validates :description, presence: true, length: {maximum: 160 }
        //      validates :unit_price, presence: true
        //      validates :total, presence: true
        //      validates :discount, numericality: { grater_than: 0}, if: ->{ discount.present? }
        //      validates :discount_reason, presence: true, if: ->{ discount.present? }
        //      validates :subtotal, presence: true
        //      validates :net_total, presence: true
        private String numeroLinea;
        private String cantidad;
        private String unidadMedida;
        private String detalle;
        private float precioUnitario;
        private float montoTotal;
        private float subTotal;
        private float montoTotalLinea;

        //pueden ser null o ""
        private String[] codigos;
        private float descuento;
        private String naturalezaDescuento;
        private Impuesto[] impuestos;
        private Exoneracion[] exoneraciones;

        public Item(string numeroLinea, string cantidad, string unidadMedida, string detalle, float precioUnitario, float montoTotal, float subTotal, float montoTotalLinea,
                    string[] codigos =null, float descuento=0f,
                    string naturalezaDescuento = "", Impuesto[] impuestos = null,
                    Exoneracion[] exoneraciones = null)
        {
            this.numeroLinea = numeroLinea;
            this.cantidad = cantidad;
            this.unidadMedida = unidadMedida;
            this.detalle = detalle;
            this.precioUnitario = precioUnitario;
            this.montoTotal = montoTotal;
            this.subTotal = subTotal;
            this.montoTotalLinea = montoTotalLinea;
            this.codigos = codigos;
            this.descuento = descuento;
            this.naturalezaDescuento = naturalezaDescuento;
            this.impuestos = impuestos;
            this.exoneraciones = exoneraciones;
        }

        public string NumeroLinea { get => numeroLinea; set => numeroLinea = value; }
        public string[] Codigos { get => codigos; set => codigos = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public float PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public float MontoTotal { get => montoTotal; set => montoTotal = value; }
        public float Descuento { get => descuento; set => descuento = value; }
        public float SubTotal { get => subTotal; set => subTotal = value; }
        public float MontoTotalLinea { get => montoTotalLinea; set => montoTotalLinea = value; }
        public string NaturalezaDescuento { get => naturalezaDescuento; set => naturalezaDescuento = value; }
        public Impuesto[] Impuestos { get => impuestos; set => impuestos = value; }
        public Exoneracion[] Exoneraciones { get => exoneraciones; set => exoneraciones = value; }

        public XElement GenerarXML()
        {

            var baseXML = new XElement("LineaDetalle");

            baseXML.Add(new XElement("NumeroLinea", numeroLinea));

            if (codigos != null)
            {
                foreach (var cod in codigos)
                {
                    var c = new XElement("Codigo",
                                         new XElement("Tipo", "01"),
                                         new XElement("Codigo", cod));
                    baseXML.Add(c);
                }
            }
            baseXML.Add(new XElement("Cantidad", cantidad));
            baseXML.Add(new XElement("UnidadMedida", unidadMedida));
            baseXML.Add(new XElement("Detalle", detalle));
            baseXML.Add(new XElement("PrecioUnitario", precioUnitario));
            baseXML.Add(new XElement("MontoTotal", montoTotal));

            if(descuento >0){
                baseXML.Add(new XElement("Discount", descuento));
            }

            if (!naturalezaDescuento.Equals(""))
            {
                baseXML.Add(new XElement("NaturalezaDescuento", naturalezaDescuento));
            }

            baseXML.Add(new XElement("SubTotal", subTotal));

            if (impuestos != null)
            {
                foreach (var imp in impuestos)
                {
                    baseXML.Add(imp.GenerarXML());
                }
            }

            if (exoneraciones != null)
            {
                foreach (var exo in exoneraciones)
                {
                    baseXML.Add(exo.GenerarXML());
                }
            }
            baseXML.Add(new XElement("MontoTotalLinea", montoTotalLinea));
            return baseXML;

        }

    }
}
