using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class DocumentoIdentificacion : IXMLGenerador
    {
        public enum TipoIdentificacion
        {
            [Description("01")]
            Cedula_Fisica,
            [Description("02")]
            Cedula_Juridica,
            [Description("03")]
            DIMEX,
            [Description("04")]
            NITE
        }

        //attr_accessor :document_type, :id_number, :raw_id_number

        //validates :document_type, presence: true, inclusion: TYPES.keys
        //validates :id_number, presence: true, length: {is: 12}

        //        def initialize(args={ })

        //          @document_type = args[:type]
        //          @raw_id_number = args[:number]
        //          @id_number = "%012d" % args[:number]

        //        end

        private TipoIdentificacion tipoIdentificacion;

        //tama;a debe ser de 12
        private String numeroFormato12;

        private String numeroCrudo;

        public TipoIdentificacion TipoIdentificacion1 { get => tipoIdentificacion; set => tipoIdentificacion = value; }
        public string NumeroFormato12 { get => numeroFormato12; set => numeroFormato12 = value.PadLeft(12, '0'); }
        public string NumeroCrudo
        {
            get => numeroCrudo;
            set => numeroCrudo = value;
        }

        public DocumentoIdentificacion(TipoIdentificacion tipoIdentificacion, string numero)
        {
            this.tipoIdentificacion = tipoIdentificacion;
            this.numeroCrudo = numero;
            this.numeroFormato12 = numero.PadLeft(12, '0');
        }

        public XElement GenerarXML()
        {
            return new XElement("Identificacion",
                                new XElement("Tipo", tipoIdentificacion.ToDescriptionString()),
                                new XElement("Numero", numeroCrudo ) );

        }

    }
}
