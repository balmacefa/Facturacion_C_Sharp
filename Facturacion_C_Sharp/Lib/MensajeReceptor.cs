using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Facturacion_C_Sharp.Lib.DocumentoItems;
using Facturacion_C_Sharp.Utils;
using Newtonsoft.Json.Linq;
using static Facturacion_C_Sharp.Lib.Documento;

namespace Facturacion_C_Sharp.Lib
{
    public class MensajeReceptor
    {

        public enum EstadoMensajeReceptor
        {
            [Description( "1" )]
            Aceptado = 1,
            [Description( "2" )]
            AceptadoParcialmente = 2,
            [Description( "3" )]
            Rechazado = 3
        }

        private String clareDocRespuesta_MR;
        private string numeroReceptor_MR;
        private DocumentoIdentificacion emisorAlQueSeResponde_MR;
        private DateTime fechaEmision_MR;
        private EstadoMensajeReceptor estadoMensajeReceptor_MR;
        private string detalleMensajeReceptor_MR;
        private decimal montoTotalImpuesto_MR;
        private decimal totalFactura_MR;


        private DocumentoIdentificacion numeroCedulaReceptor_Nosotros_MR;

        private String pais = "506";
        private String sede = "001";
        //Terminal o punto de venta
        private String terminalPuntodeVenta = "00001";
        private String codigoSeguridad;
        private TipoDocumento tipoDocumento;

        public string ClareDocRespuesta
        {
            get => clareDocRespuesta_MR;
            set => clareDocRespuesta_MR = value;
        }
        public string NumeroReceptor_MR
        {
            get => numeroReceptor_MR;
            set => numeroReceptor_MR = value;
        }
        public DocumentoIdentificacion Emisor_MR
        {
            get => emisorAlQueSeResponde_MR;
            set => emisorAlQueSeResponde_MR = value;
        }
        public DateTime FechaEmision_MR
        {
            get => fechaEmision_MR;
            set => fechaEmision_MR = value;
        }
        public EstadoMensajeReceptor EstadoMensajeReceptor_MR
        {
            get => estadoMensajeReceptor_MR;
            set => estadoMensajeReceptor_MR = value;
        }
        public string DetalleMensajeReceptor_MR
        {
            get => detalleMensajeReceptor_MR;
            set => detalleMensajeReceptor_MR = value;
        }
        public DocumentoIdentificacion NumeroCedulaReceptor_MR
        {
            get => numeroCedulaReceptor_Nosotros_MR;
            set => numeroCedulaReceptor_Nosotros_MR = value;
        }
        public string Pais
        {
            get => pais;
            set => pais = value;
        }
        public string Sede
        {
            get => sede;
            set => sede = value;
        }
        public string TerminalPuntodeVenta
        {
            get => terminalPuntodeVenta;
            set => terminalPuntodeVenta = value;
        }
        public string CodigoSeguridad
        {
            get => codigoSeguridad;
            set => codigoSeguridad = value;
        }
        public TipoDocumento TipoDocumento
        {
            get => tipoDocumento;
            set => tipoDocumento = value;
        }
        public decimal MontoTotalImpuesto
        {
            get => montoTotalImpuesto_MR;
            set => montoTotalImpuesto_MR = value;
        }
        public decimal TotalFactura
        {
            get => totalFactura_MR;
            set => totalFactura_MR = value;
        }

        public MensajeReceptor ( string clareDocRespuesta,
                                 DocumentoIdentificacion emisorAlQueSeResponde_MR,
                                 DateTime fechaEmision_MR,
                                 EstadoMensajeReceptor estadoMensajeReceptor_MR,
                                 string detalleMensajeReceptor_MR,
                                 decimal montoTotalImpuesto,
                                 decimal totalFactura,
                                 DocumentoIdentificacion numeroCedulaReceptor_Nosotros_MR,
                                 string numeroReceptor_MR )
        {
            this.clareDocRespuesta_MR = clareDocRespuesta;
            this.numeroReceptor_MR = numeroReceptor_MR;
            this.emisorAlQueSeResponde_MR = emisorAlQueSeResponde_MR;
            this.fechaEmision_MR = fechaEmision_MR;
            this.estadoMensajeReceptor_MR = estadoMensajeReceptor_MR;
            this.detalleMensajeReceptor_MR = detalleMensajeReceptor_MR;
            this.numeroCedulaReceptor_Nosotros_MR = numeroCedulaReceptor_Nosotros_MR;

            switch( estadoMensajeReceptor_MR )
            {
                case EstadoMensajeReceptor.Aceptado:
                    tipoDocumento = TipoDocumento.Aceptación_del_comprobante_electrónico;
                    break;
                case EstadoMensajeReceptor.AceptadoParcialmente:
                    tipoDocumento = TipoDocumento.Aceptación_parcial_del_comprobante_electrónico;
                    break;
                case EstadoMensajeReceptor.Rechazado:
                    tipoDocumento = TipoDocumento.Rechazo_del_comprobante_electrónico;
                    break;
                default:
                    tipoDocumento = TipoDocumento.Otros;
                    break;
            }
        }

        public String NumeroConsecutivo ( )
        {
            return sede + terminalPuntodeVenta + tipoDocumento.ToDescriptionString( ) + numeroReceptor_MR.PadLeft( 10, '0' );
        }

        public String ClaveNumerica ( )
        {
            var fecha = String.Format( "{0:ddMMyy}", fechaEmision_MR );
            return pais + fecha + numeroCedulaReceptor_Nosotros_MR.NumeroFormato12 + NumeroConsecutivo( ) + estadoMensajeReceptor_MR.ToDescriptionString( ) + codigoSeguridad.PadLeft( 8, '0' );
        }

        public String TagDocumento ( )
        {
            return "MensajeReceptor";
        }

        public XNamespace GetNameSpace ( )
        {
            XNamespace xmlns;
            xmlns = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor";
            return xmlns;
        }

        //TODO:
        public XElement RootDocumento ( )
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            var xmlns = GetNameSpace( );

            XElement root = new XElement( xmlns + TagDocumento( ) );

            root.Add( new XAttribute( "xmlns", xmlns.NamespaceName ),
                     new XAttribute( XNamespace.Xmlns + "xsi", xsi.NamespaceName ),
                     new XAttribute( XNamespace.Xmlns + "xsd", xsd.NamespaceName ) );

            return root;
        }

        public XDocument OptenerXML_Nofirmado ( )
        {
            var root = RootDocumento( );

            root.Add( new XElement( "Clave", clareDocRespuesta_MR ),
                     new XElement( "NumeroCedulaEmisor", emisorAlQueSeResponde_MR.NumeroCrudo ),
                     new XElement( "FechaEmision", fechaEmision_MR.ToRfc3339String( ) ),
                     new XElement( "Mensaje", estadoMensajeReceptor_MR.ToDescriptionString( ) ),
                     new XElement( "DetalleMensaje", detalleMensajeReceptor_MR ),
                     new XElement( "MontoTotalImpuesto", montoTotalImpuesto_MR ),
                     new XElement( "TotalFactura", totalFactura_MR ),
                     new XElement( "NumeroCedulaReceptor", numeroCedulaReceptor_Nosotros_MR.NumeroCrudo ),
                     new XElement( "NumConsecutivoReceptor", ClaveNumerica( ) ) );


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
            payload.fecha = fechaEmision_MR.ToRfc3339String( );

            dynamic jemisor = new JObject( );
            jemisor.tipoIdentificacion = emisorAlQueSeResponde_MR.TipoIdentificacion1.ToDescriptionString( );
            jemisor.numeroIdentificacion = emisorAlQueSeResponde_MR.NumeroFormato12;

            payload.emisor = jemisor;

            dynamic jreceptor = new JObject( );
            jreceptor.tipoIdentificacion = numeroCedulaReceptor_Nosotros_MR.TipoIdentificacion1.ToDescriptionString( );
            jreceptor.numeroIdentificacion = numeroCedulaReceptor_Nosotros_MR.NumeroFormato12;

            payload.receptor = jreceptor;

            payload.consecutivoReceptor = consecutivoReceptor;
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
            var docFirmado = FirmadorXML.Firmar( this, configuracion.RutaLlaveCriptografica, configuracion.PinLlaveCriptografica );

            //guargar en base 64
            byte[] asBytes = docFirmado.GetDocumentBytes( );
            documentoFirmadoBase64 = Convert.ToBase64String( asBytes ).Replace( "\n", "" );

            //Guardar xml firmado
            documentoFirmado = new XmlDocument( );
            documentoFirmado.Load( new MemoryStream( asBytes ) );
        }
    }
}

