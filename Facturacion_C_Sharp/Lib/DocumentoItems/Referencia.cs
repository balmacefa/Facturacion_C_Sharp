using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Referencia :IXMLGenerador
    {
        public enum CodigoReferencia
        {
            [Description( "01" )]
            Anula_Documento_de_referencia = 01,
            [Description( "02" )]
            Corrige_texto_documento_de_referencia = 02,
            [Description( "03" )]
            Corrige_monto = 03,
            [Description( "04" )]
            Referencia_a_otro_documento = 04,
            [Description( "05" )]
            Sustituye_comprobante_provisional_por_contingencia = 05,
            [Description( "99" )]
            Otros = 99
        }

        public static CodigoReferencia StringToCodigoReferencia ( String codigo )
        {
            switch( codigo )
            {
                case "01":
                    return CodigoReferencia.Anula_Documento_de_referencia;
                case "02":
                    return CodigoReferencia.Corrige_texto_documento_de_referencia;
                case "03":
                    return CodigoReferencia.Corrige_monto;
                case "04":
                    return CodigoReferencia.Referencia_a_otro_documento;
                case "05":
                default:
                    return CodigoReferencia.Otros;
            }
        }

        //      validates :document_type, presence: true, inclusion: FE::Document::DOCUMENT_TYPES.keys
        //      validates :number, presence: true, length: {maximum: 50}
        //      validates :date, presence: true
        //      validates :code, presence: true, length: {is: 2}, inclusion: REFERENCE_CODES.keys
        //      validates :reason, presence: true, length: {maximum: 180}

        private Documento.TipoDocumento tipoDoc;
        //Clave Numerica
        private String numero;
        //DD-MM-YYYY HH:MM:SS
        private DateTime fechaEmision;
        private CodigoReferencia codigo;
        private String razon;

        public Referencia ( Documento.TipoDocumento tipoDoc, string numero, DateTime fechaEmision, CodigoReferencia codigo, string razon )
        {
            this.tipoDoc = tipoDoc;
            this.numero = numero;
            this.fechaEmision = fechaEmision;
            this.codigo = codigo;
            this.razon = razon;
        }

        public Documento.TipoDocumento TipoDoc
        {
            get => tipoDoc; set => tipoDoc = value;
        }
        public string Numero
        {
            get => numero; set => numero = value;
        }
        public DateTime FechaEmision
        {
            get => fechaEmision; set => fechaEmision = value;
        }
        public CodigoReferencia Codigo
        {
            get => codigo; set => codigo = value;
        }
        public string Razon
        {
            get => razon; set => razon = value;
        }

        public XElement GenerarXML ( )
        {
            return new XElement( "InformacionReferencia",
                                       new XElement( "TipoDoc", tipoDoc.ToDescriptionString( ) ),
                                       new XElement( "Numero", numero ),
                                       new XElement( "FechaEmision", fechaEmision.ToRfc3339String( ) ),
                                       new XElement( "Codigo", codigo.ToDescriptionString( ) ),
                                       new XElement( "Razon", razon ) );
        }
    }
}
