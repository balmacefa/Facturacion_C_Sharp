using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Impuesto : IXMLGenerador
    {
        public enum CodigoImpuesto
        {
            [Description("01")]
            Impuesto_General_sobre_las_Ventas = 01,

            [Description("02")]
            Impuesto_Selectivo_de_Consumo = 02,

            [Description("03")]
            Impuesto_Único_a_los_combustibles = 03,

            [Description("04")]
            Impuesto_específico_de_bebidas_alcohólicas = 04,

            [Description("05")]
            Impuesto_Específico_sobre_las_bebidas_envasadas_sin_contenido_alcóholico_y_jabones_de_tocador = 05,

            [Description("06")]
            Impuesto_a_los_Productos_de_Tabaco = 06,

            [Description("07")]
            Servicio = 07,

            [Description("08")]
            Impuesto_General_sobre_las_ventas_diplomáticos = 08,

            [Description("09")]
            Impuesto_general_sobre_las_ventas_Compras_autorizadas = 09,

            [Description("10")]
            Impuesto_general_sobre_las_ventas_instituciones_públicas_y_otros_organismos = 10,

            [Description("11")]
            Impuesto_Selectivo_de_Consumo_Compras_Autorizadas = 11,

            [Description("12")]
            Impuesto_específico_al_cemento = 12,

            [Description("98")]
            Otros98 = 98,

            [Description("99")]
            Otros99 = 99
        }


        //El codigo siempre debe estar presente
        private CodigoImpuesto codigo;
        private float tarifa;
        private float monto;

        private Exoneracion exoneracion;

        public Impuesto(CodigoImpuesto codigo, float tarifa, float monto, Exoneracion exoneracion = null)
        {
            this.codigo = codigo;
            this.tarifa = tarifa;
            this.monto = monto;
            this.exoneracion = exoneracion;
        }

        public CodigoImpuesto Codigo { get => codigo; set => codigo = value; }
        public float Tarifa { get => tarifa; set => tarifa = value; }
        public float Monto { get => monto; set => monto = value; }
        public Exoneracion Exoneracion { get => exoneracion; set => exoneracion = value; }

        public XElement GenerarXML()
        {
            XElement impuesto;

            impuesto = new XElement("Impuesto",
                    new XElement("Codigo", codigo.ToDescriptionString()),
                    new XElement("Tarifa", tarifa),
                    new XElement("Monto", monto));

            if (exoneracion != null)
            {
                impuesto.Add(exoneracion.GenerarXML());
            }

            return impuesto;
        }
    }
}
