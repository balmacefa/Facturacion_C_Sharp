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

        private String claveDocRespuestaDocumentoTercero;
        private string numeroConsetutivoReceptorNosotros;
        private DocumentoIdentificacion emisorAlQueSeRespondeTercero;
        private DateTime fechaEmisionNosotros;
        private EstadoMensajeReceptor estadoMensajeReceptorNosotros;
        private string detalleMensajeReceptorNosotros;
        private decimal montoTotalImpuestoDocumentoTercero;
        private decimal totalFacturaDocumentoTercero;


        private DocumentoIdentificacion numeroCedulaReceptor_Nosotros;

        private String pais = "506";
        private String sede = "001";
        //Terminal o punto de venta
        private String terminalPuntodeVenta = "00001";
        private String codigoSeguridad;
        private TipoDocumento tipoDocumento;


        //DocumentoFirmado Base64
        private String documentoFirmadoBase64 = "";
        private XmlDocument documentoFirmado;

        public string ClaveDocRespuestaDocumentoTercero
        {
            get => claveDocRespuestaDocumentoTercero;
            set => claveDocRespuestaDocumentoTercero = value;
        }
        public string NumeroConsetutivoReceptorNosotros
        {
            get => numeroConsetutivoReceptorNosotros;
            set => numeroConsetutivoReceptorNosotros = value;
        }
        public DocumentoIdentificacion EmisorAlQueSeRespondeTercero
        {
            get => emisorAlQueSeRespondeTercero;
            set => emisorAlQueSeRespondeTercero = value;
        }
        public DateTime FechaEmisionNosotros
        {
            get => fechaEmisionNosotros;
            set => fechaEmisionNosotros = value;
        }
        public EstadoMensajeReceptor EstadoMensajeReceptorNosotros
        {
            get => estadoMensajeReceptorNosotros;
            set => estadoMensajeReceptorNosotros = value;
        }
        public string DetalleMensajeReceptorNosotros
        {
            get => detalleMensajeReceptorNosotros;
            set => detalleMensajeReceptorNosotros = value;
        }
        public decimal MontoTotalImpuestoDocumentoTercero
        {
            get => montoTotalImpuestoDocumentoTercero;
            set => montoTotalImpuestoDocumentoTercero = value;
        }
        public decimal TotalFacturaDocumentoTercero
        {
            get => totalFacturaDocumentoTercero;
            set => totalFacturaDocumentoTercero = value;
        }
        public DocumentoIdentificacion NumeroCedulaReceptor_Nosotros
        {
            get => numeroCedulaReceptor_Nosotros;
            set => numeroCedulaReceptor_Nosotros = value;
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
        public string DocumentoFirmadoBase64
        {
            get => documentoFirmadoBase64;
            set => documentoFirmadoBase64 = value;
        }
        public XmlDocument DocumentoFirmado
        {
            get => documentoFirmado;
            set => documentoFirmado = value;
        }

        public MensajeReceptor ( string claveDocRespuestaDocumentoTercero,
                                 DocumentoIdentificacion emisorAlQueSeRespondeTercero,
                                 DateTime fechaEmisionNosotros,
                                 EstadoMensajeReceptor estadoMensajeReceptorNosotros,
                                 decimal totalFacturaDocumentoTercero,
                                 DocumentoIdentificacion numeroCedulaReceptor_Nosotros,
                                 string numeroConsetutivoReceptorNosotros,

                                 //opcionales
                                 decimal montoTotalImpuestoDocumentoTercero = -1,
                                 string detalleMensajeReceptorNosotros = "" )
        {
            this.claveDocRespuestaDocumentoTercero = claveDocRespuestaDocumentoTercero;
            this.numeroConsetutivoReceptorNosotros = numeroConsetutivoReceptorNosotros;
            this.emisorAlQueSeRespondeTercero = emisorAlQueSeRespondeTercero;
            this.fechaEmisionNosotros = fechaEmisionNosotros;
            this.estadoMensajeReceptorNosotros = estadoMensajeReceptorNosotros;
            this.detalleMensajeReceptorNosotros = detalleMensajeReceptorNosotros;
            this.numeroCedulaReceptor_Nosotros = numeroCedulaReceptor_Nosotros;
            this.montoTotalImpuestoDocumentoTercero = montoTotalImpuestoDocumentoTercero;
            this.totalFacturaDocumentoTercero = totalFacturaDocumentoTercero;
            switch( estadoMensajeReceptorNosotros )
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
            return sede + terminalPuntodeVenta + tipoDocumento.ToDescriptionString( ) + numeroConsetutivoReceptorNosotros.PadLeft( 10, '0' );
        }

        public String NumeroConsecutivoReceptor ( )
        {
            var fecha = String.Format( "{0:ddMMyy}", fechaEmisionNosotros );
            return pais + fecha + numeroCedulaReceptor_Nosotros.NumeroFormato12 + NumeroConsecutivo( ) + estadoMensajeReceptorNosotros.ToDescriptionString( ) + codigoSeguridad.PadLeft( 8, '0' );
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

            root.Add( new XElement( "Clave", claveDocRespuestaDocumentoTercero ) );
            root.Add( new XElement( "NumeroCedulaEmisor", emisorAlQueSeRespondeTercero.NumeroCrudo ) );
            root.Add( new XElement( "FechaEmision", fechaEmisionNosotros.ToRfc3339String( ) ) );
            root.Add( new XElement( "Mensaje", estadoMensajeReceptorNosotros.ToDescriptionString( ) ) );

            //opcional
            if( detalleMensajeReceptorNosotros != "" )
            {
                root.Add( new XElement( "DetalleMensaje", detalleMensajeReceptorNosotros ) );
            }
            if( this.montoTotalImpuestoDocumentoTercero >= 0 )
            {
                root.Add( new XElement( "MontoTotalImpuesto", montoTotalImpuestoDocumentoTercero ) );
            }

            root.Add( new XElement( "TotalFactura", totalFacturaDocumentoTercero ) );
            root.Add( new XElement( "NumeroCedulaReceptor", numeroCedulaReceptor_Nosotros.NumeroCrudo ) );
            root.Add( new XElement( "NumConsecutivoReceptor", NumeroConsecutivoReceptor( ) ) );


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
            payload.clave = claveDocRespuestaDocumentoTercero;
            payload.fecha = fechaEmisionNosotros.ToRfc3339String( );

            dynamic jemisor = new JObject( );
            jemisor.tipoIdentificacion = emisorAlQueSeRespondeTercero.TipoIdentificacion1.ToDescriptionString( );
            jemisor.numeroIdentificacion = emisorAlQueSeRespondeTercero.NumeroFormato12;

            payload.emisor = jemisor;

            dynamic jreceptor = new JObject( );
            jreceptor.tipoIdentificacion = numeroCedulaReceptor_Nosotros.TipoIdentificacion1.ToDescriptionString( );
            jreceptor.numeroIdentificacion = numeroCedulaReceptor_Nosotros.NumeroFormato12;

            payload.receptor = jreceptor;

            payload.consecutivoReceptor = NumeroConsecutivoReceptor( );
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

