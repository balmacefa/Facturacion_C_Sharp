using System;
using System.ComponentModel;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class TelefonoBase : IXMLGenerador
    {
        public enum TipoTelefono
        {
            [Description("Telefono")]
            Telefono,
            [Description("Fax")]
            Fax
        }
        private TipoTelefono tipoTelefono;
        private String codigoPais;
        private String numTelefono;

        public TelefonoBase(TipoTelefono tipoTelefono, string codigoPais, string numTelefono)
        {
            this.tipoTelefono = tipoTelefono;
            this.codigoPais = codigoPais;
            this.numTelefono = numTelefono;
        }

        public TipoTelefono TipoTelefono1 { get => tipoTelefono; set => tipoTelefono = value; }
        public string CodigoPais { get => codigoPais; set => codigoPais = value; }
        public string NumTelefono { get => numTelefono; set => numTelefono = value; }

        //private bool Validar()
        //{
        //    return codigoPais.legth > 20;//   /\d+/
        //    return numTelefono.legth > 3;
        //}

        public XElement GenerarXML()
        {
            return new XElement(tipoTelefono.ToDescriptionString(),
                                new XElement("CodigoPais", codigoPais),
                                new XElement("NumTelefono", numTelefono));
        }
    }
}
