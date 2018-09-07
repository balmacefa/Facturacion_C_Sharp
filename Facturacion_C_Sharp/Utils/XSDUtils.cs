using Facturacion_C_Sharp.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Facturacion_C_Sharp.Utils
{
    public class XSDUtils
    {
        public struct Estado
        {
            private bool valido;
            private string mensajeError;

            public bool Valido
            {
                get => valido;
                set => valido = value;
            }
            public string MensajeError
            {
                get => mensajeError;
                set => mensajeError = value;
            }
            public Estado ( bool valido, string mensajeError ) : this( )
            {
                Valido = valido;
                MensajeError = mensajeError;
            }
        }

        //public static Estado ValidarXML ( Documento documento )
        //{
        //    byte[] file;
        //    switch( documento.TipoDocumento1 )
        //    {
        //        case Documento.TipoDocumento.Factura_Electronica:
        //            file = Properties.Resources.FacturaElectronica_V_4_2;
        //            break;
        //        case Documento.TipoDocumento.Nota_de_crédito:
        //            file = Properties.Resources.NotaCreditoElectronica_V_4_2;
        //            break;
        //        case Documento.TipoDocumento.Nota_de_débito:
        //            file = Properties.Resources.NotaDebitoElectronica_V_4_2;
        //            break;
        //        case Documento.TipoDocumento.Tiquete_Electrónico:
        //            file = Properties.Resources.TiqueteElectronico_V_4_2;
        //            break;
        //        default:
        //            file = Properties.Resources.FacturaElectronica_V_4_2;
        //            break;
        //    }

        //    XmlSchemaSet xsd = new XmlSchemaSet( );
        //    xsd.Add( documento.GetNameSpace( ).NamespaceName, XmlReader.Create( new MemoryStream( file ) ) );

        //    var xml = documento.DocumentoFirmado;
        //    xml.Schemas.Add( xsd );

        //    try
        //    {
        //        xml.Validate( null );
        //    } catch( XmlSchemaValidationException ex )
        //    {
        //        return new Estado( valido: false, mensajeError: ex.Message );
        //    }
        //    return new Estado( valido: true, mensajeError: "" );

        //}

        public static Estado ValidarXML ( Documento documento )
        {
            string file;
            switch( documento.TipoDocumento1 )
            {
                case Documento.TipoDocumento.Factura_Electronica:
                    file = Properties.Resources.FacturaElectronica_V_4_2;
                    break;
                case Documento.TipoDocumento.Nota_de_crédito:
                    file = Properties.Resources.NotaCreditoElectronica_V_4_2;
                    break;
                case Documento.TipoDocumento.Nota_de_débito:
                    file = Properties.Resources.NotaDebitoElectronica_V_4_2;
                    break;
                case Documento.TipoDocumento.Tiquete_Electrónico:
                    file = Properties.Resources.TiqueteElectronico_V_4_2;
                    break;
                default:
                    file = Properties.Resources.FacturaElectronica_V_4_2;
                    break;
            }


            XmlReader xmlReader = null;

            try
            {

                XmlReaderSettings settings = new XmlReaderSettings( );
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

                var steam = new MemoryStream( );
                documento.DocumentoFirmado.Save( steam );


                xmlReader = XmlReader.Create( steam, settings );
                while( xmlReader.Read( ) )
                    ;
            } catch( Exception ex )
            {
                return new Estado( valido: false, mensajeError: ex.Message );
            }
            return new Estado( valido: true, mensajeError: "" );
        }

        }
}
