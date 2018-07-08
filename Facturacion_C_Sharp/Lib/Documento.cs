using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using Facturacion_C_Sharp.Lib.DocumentoItems;
using Facturacion_C_Sharp.Utils;
using Newtonsoft.Json.Linq;

namespace Facturacion_C_Sharp.Lib
{
    public class Documento
    {
        public enum CondicionVenta
        {
            [Description( "01" )]
            Contado = 01,
            [Description( "02" )]
            Crédito = 02,
            [Description( "03" )]
            Consignación = 03,
            [Description( "04" )]
            Apartado = 04,
            [Description( "05" )]
            Arrendamiento_con_Opción_de_Compra = 5,
            [Description( "06" )]
            Arrendamiento_en_Función_Financiera = 6,
            [Description( "99" )]
            Otros = 99
        }

        public enum MedioPago
        {
            [Description( "01" )]
            Efectivo = 01,
            [Description( "02" )]
            Tarjeta = 02,
            [Description( "03" )]
            Cheque = 03,
            [Description( "04" )]
            Transferencia = 04,
            [Description( "05" )]
            Recaudado_por_Terceros,
            [Description( "99" )]
            Otros = 99
        }

        public enum TipoDocumento
        {
            [Description( "01" )]
            Factura_Electronica = 01,
            [Description( "02" )]
            Nota_de_débito = 2,
            [Description( "03" )]
            Nota_de_crédito = 03,
            [Description( "04" )]
            Tiquete_Electrónico = 04,
            [Description( "05" )]
            Nota_de_despacho = 05,
            [Description( "06" )]
            Contrato = 06,
            [Description( "07" )]
            Procedimiento = 07,
            [Description( "08" )]
            Comprobante_Emitido_en_Contingencia = 08,
            [Description( "99" )]
            Otros = 99
        }

        public enum SituacionDocumento
        {
            [Description( "1" )]
            Normal = 1,
            [Description( "2" )]
            Contingencia = 2,
            [Description( "3" )]
            Sin_Internet = 3
        }

        //    validates :date, presence: true
        //    validates :number, presence: true
        //    validates :issuer, presence: true
        //    validates :condition, presence: true, inclusion: CONDITIONS.keys
        //    validates :credit_term, presence: true, if: ->{condition.eql? ("02")}
        //    validates :payment_type, presence: true, inclusion: PAYMENT_TYPES.keys
        //    validates :document_type, presence: true, inclusion: DOCUMENT_TYPES.keys
        //    validates :document_situation, presence: true, inclusion: DOCUMENT_SITUATION.keys
        //    validates :summary, presence: true
        //    validates :regulation, presence: true
        //    validates :security_code, presence: true, length: {is: 8}
        //    validates :references, presence: true, if: -> {document_type.eql? ("02") || document_type.eql? ("03")}

        private DateTime fechaEmision;
        private Emisor emisor;
        private Receptor receptor;
        private CondicionVenta condicionVenta;
        private String plazoCredito;
        private MedioPago medioPago;
        private Normativa normativa = new Normativa( );
        private String numero;
        private TipoDocumento tipoDocumento;
        private String codigoSeguridad;
        private Item[] items;
        private Referencia[] referencias = null;
        private ResumenFactura resumenFactura;
        private SituacionDocumento situacionDocumento;

        private String pais = "506";
        private String sede = "001";
        //Terminal o punto de venta
        private String terminalPuntodeVenta = "00001";


        //DocumentoFirmado Base64
        private String documentoFirmadoBase64 = "";

        public Documento ( DateTime fechaEmision,
                         Emisor emisor,
                         CondicionVenta condicionVenta,
                         MedioPago medioPago,
                         String numero,
                         TipoDocumento tipoDocumento,
                         String codigoSeguridad,
                         Item[] items,
                         ResumenFactura resumenFactura,
                         SituacionDocumento situacionDocumento,

                         //Parametros opcionales
                         Receptor receptor = null,
                         Normativa normativa = null,
                         Referencia[] referencias = null,
                         string plazoCredito = "",
                         string pais = "506",
                         string sede = "001",
                         string terminalPuntodeVenta = "00001" )
        {
            if( normativa == null )
            {
                normativa = new Normativa( );
            }
            this.fechaEmision = fechaEmision;
            this.emisor = emisor;
            this.receptor = receptor;
            this.condicionVenta = condicionVenta;
            this.plazoCredito = plazoCredito;
            this.medioPago = medioPago;
            this.numero = numero;
            this.tipoDocumento = tipoDocumento;
            this.codigoSeguridad = codigoSeguridad;
            this.items = items;
            this.referencias = referencias;
            this.resumenFactura = resumenFactura;
            this.situacionDocumento = situacionDocumento;
            this.pais = pais;
            this.sede = sede;
            this.terminalPuntodeVenta = terminalPuntodeVenta;
        }

        public DateTime FechaEmision
        {
            get => fechaEmision; set => fechaEmision = value;
        }
        public Emisor Emisor
        {
            get => emisor; set => emisor = value;
        }
        public Receptor Receptor
        {
            get => receptor; set => receptor = value;
        }
        public CondicionVenta CondicionVenta1
        {
            get => condicionVenta; set => condicionVenta = value;
        }
        public string PlazoCredito
        {
            get => plazoCredito; set => plazoCredito = value;
        }
        public MedioPago MedioPago1
        {
            get => medioPago; set => medioPago = value;
        }
        public Normativa Normativa
        {
            get => normativa; set => normativa = value;
        }
        public String Numero
        {
            get => numero; set => numero = value;
        }
        public TipoDocumento TipoDocumento1
        {
            get => tipoDocumento; set => tipoDocumento = value;
        }
        public String CodigoSeguridad
        {
            get => codigoSeguridad; set => codigoSeguridad = value;
        }
        public Item[] Items
        {
            get => items; set => items = value;
        }
        public Referencia[] Referencias
        {
            get => referencias; set => referencias = value;
        }
        public ResumenFactura ResumenFactura
        {
            get => resumenFactura; set => resumenFactura = value;
        }
        public SituacionDocumento SituacionDocumento1
        {
            get => situacionDocumento; set => situacionDocumento = value;
        }
        public string Pais
        {
            get => pais; set => pais = value;
        }
        public string Sede
        {
            get => sede; set => sede = value;
        }
        public string TerminalPuntodeVenta
        {
            get => terminalPuntodeVenta; set => terminalPuntodeVenta = value;
        }
        public string DocumentoFirmadoBase64
        {
            get => documentoFirmadoBase64;
            set => documentoFirmadoBase64 = value;
        }

        public String NumeroConsecutivo ( )
        {
            return sede + terminalPuntodeVenta + tipoDocumento.ToDescriptionString( ) + numero.PadLeft( 10, '0' );
        }

        public String ClaveNumerica ( )
        {
            var fecha = String.Format( "{0:ddMMyy}", fechaEmision );
            return pais + fecha + emisor.Identificacion.NumeroFormato12 + NumeroConsecutivo( ) + situacionDocumento.ToDescriptionString( ) + codigoSeguridad.PadLeft( 8, '0' );
        }
        public String TagDocumento ( )
        {
            switch( tipoDocumento )
            {
                case TipoDocumento.Factura_Electronica:
                    return "FacturaElectronica";
                case TipoDocumento.Nota_de_crédito:
                    return "NotaCreditoElectronica";
                case TipoDocumento.Nota_de_débito:
                    return "NotaDebitoElectronica";
                case TipoDocumento.Tiquete_Electrónico:
                    return "TiqueteElectronico";
                default:
                    return "ERROR TIPO DE DOCUMENTO";
            }
        }


        public XElement RootDocumento ( )
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            XNamespace xmlns;

            switch( tipoDocumento )
            {
                case TipoDocumento.Factura_Electronica:
                    xmlns = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica";
                    break;
                case TipoDocumento.Nota_de_crédito:
                    xmlns = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica";
                    break;
                case TipoDocumento.Nota_de_débito:
                    xmlns = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaDebitoElectronica";
                    break;
                case TipoDocumento.Tiquete_Electrónico:
                    xmlns = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/tiqueteElectronico";
                    break;
                default:
                    xmlns = "ERROR TIPO DE DOCUMENTO";
                    break;
            }

            XElement root = new XElement( xmlns + TagDocumento( ) );

            root.Add( new XAttribute( "xmlns", xmlns.NamespaceName ),
                     new XAttribute( XNamespace.Xmlns + "xsi", xsi.NamespaceName ),
                     new XAttribute( XNamespace.Xmlns + "xsd", xsd.NamespaceName ) );

            return root;
        }



        public XDocument OptenerXML_Nofirmado ( )
        {
            var root = RootDocumento( );

            root.Add( new XElement( "Clave", ClaveNumerica( ) ),
                     new XElement( "NumeroConsecutivo", NumeroConsecutivo( ) ),
                     new XElement( "FechaEmision", fechaEmision.ToRfc3339String( ) ) );

            root.Add( emisor.GenerarXML( ) );

            if( receptor != null )
            {
                root.Add( receptor.GenerarXML( ) );
            }

            root.Add( new XElement( "CondicionVenta", condicionVenta.ToDescriptionString( ) ) );

            if( !plazoCredito.Equals( "" ) && condicionVenta == CondicionVenta.Crédito )
            {
                root.Add( new XElement( "PlazoCredito", plazoCredito ) );
            }

            root.Add( new XElement( "MedioPago", medioPago.ToDescriptionString( ) ) );

            var detalleServicio = new XElement( "DetalleServicio" );
            foreach( var item in items )
            {
                detalleServicio.Add( item.GenerarXML( ) );
            }
            root.Add( new XElement( detalleServicio ) );

            root.Add( resumenFactura.GenerarXML( ) );

            if( referencias != null )
            {
                foreach( var r in referencias )
                {
                    root.Add( r.GenerarXML() );
                }
            }

            root.Add( normativa.GenerarXML( ) );

            XDocument doc = new XDocument(
                new XDeclaration( "1.0", "UTF-8", null ),
                root
            );

            RemoverNameSpaceVacios( doc );

            return doc;
        }

        private void RemoverNameSpaceVacios ( XDocument doc )
        {
            foreach( var node in doc.Root.Descendants( ) )
            {
                // If we have an empty namespace...
                if( node.Name.NamespaceName == "" )
                {
                    // Remove the xmlns='' attribute. Note the use of
                    // Attributes rather than Attribute, in case the
                    // attribute doesn't exist (which it might not if we'd
                    // created the document "manually" instead of loading
                    // it from a file.)
                    node.Attributes( "xmlns" ).Remove( );
                    // Inherit the parent namespace instead
                    node.Name = node.Parent.Name.Namespace + node.Name.LocalName;
                }
            }
        }

        public JObject JsonPayload ( )
        {
            dynamic payload = new JObject( );
            payload.clave = ClaveNumerica( );
            payload.fecha = fechaEmision.ToRfc3339String( );

            dynamic jemisor = new JObject( );
            jemisor.tipoIdentificacion = emisor.Identificacion.TipoIdentificacion1.ToDescriptionString( );
            jemisor.numeroIdentificacion = emisor.Identificacion.NumeroFormato12;

            payload.emisor = jemisor;

            if( receptor.Identificacion != null )
            {
                dynamic jreceptor = new JObject( );
                jreceptor.tipoIdentificacion = receptor.Identificacion.TipoIdentificacion1.ToDescriptionString( );
                jreceptor.numeroIdentificacion = receptor.Identificacion.NumeroFormato12;

                payload.receptor = jreceptor;
            }

            //Cargar archivo
            if( !documentoFirmadoBase64.Equals( "" ) )
            {
                payload.comprobanteXml = documentoFirmadoBase64;
            } else
            {
                throw new ExecpcionFacturacionHacienda( "No se ha firmado el Documento" );
            }

            return payload;
        }

        public void FirmarDocumento ( Configuracion configuracion )
        {
            documentoFirmadoBase64 = FirmadorXML.Firmar( this, configuracion.RutaLlaveCriptografica, configuracion.PinLlaveCriptografica );
        }

    }
}