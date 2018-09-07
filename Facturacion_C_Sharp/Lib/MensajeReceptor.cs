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

        private String clareDocRespuesta;
        private string numeroReceptor_MR;
        private DocumentoIdentificacion emisor_MR;
        private DateTime fechaEmision_MR;
        private EstadoMensajeReceptor estadoMensajeReceptor_MR;
        private string detalleMensajeReceptor_MR;


        private DocumentoIdentificacion numeroCedulaReceptor_MR;

        private String pais = "506";
        private String sede = "001";
        //Terminal o punto de venta
        private String terminalPuntodeVenta = "00001";
        private String codigoSeguridad;
        private TipoDocumento tipoDocumento;

        public string ClareDocRespuesta
        {
            get => clareDocRespuesta;
            set => clareDocRespuesta = value;
        }
        public string NumeroReceptor_MR
        {
            get => numeroReceptor_MR;
            set => numeroReceptor_MR = value;
        }
        public DocumentoIdentificacion Emisor_MR
        {
            get => emisor_MR;
            set => emisor_MR = value;
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
            get => numeroCedulaReceptor_MR;
            set => numeroCedulaReceptor_MR = value;
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

        public MensajeReceptor ( string clareDocRespuesta,
                                 string numeroReceptor_MR,
                                 DocumentoIdentificacion emisor_MR,
                                 DateTime fechaEmision_MR,
                                 EstadoMensajeReceptor estadoMensajeReceptor_MR,
                                 string detalleMensajeReceptor_MR,
                                 DocumentoIdentificacion numeroCedulaReceptor_MR )
        {
            this.clareDocRespuesta = clareDocRespuesta;
            this.numeroReceptor_MR = numeroReceptor_MR;
            this.emisor_MR = emisor_MR;
            this.fechaEmision_MR = fechaEmision_MR;
            this.estadoMensajeReceptor_MR = estadoMensajeReceptor_MR;
            this.detalleMensajeReceptor_MR = detalleMensajeReceptor_MR;
            this.numeroCedulaReceptor_MR = numeroCedulaReceptor_MR;

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
            return pais + fecha + numeroCedulaReceptor_MR.NumeroFormato12 + NumeroConsecutivo( ) + estadoMensajeReceptor_MR.ToDescriptionString( ) + codigoSeguridad.PadLeft( 8, '0' );
        }
    }
}

