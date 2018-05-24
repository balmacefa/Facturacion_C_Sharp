using System;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Emisor : IXMLGenerador
    {
        //        validates :name, presence: true, length: { maximum: 80 }
        //        validates :identification_document, presence: true
        //        validates :comercial_name, length: {maximum: 80}
        //        validates :location, presence: true
        //        validates :email, presence: true, format: {with: /\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*/}
        private String nombre;
        private DocumentoIdentificacion identificacion;
        private String nombreComercial = "";
        private Ubicacion ubicacion;
        private TelefonoBase telefono = null;
        private TelefonoBase fax = null;
        private String email;

        public string Nombre { get => nombre; set => nombre = value; }
        public DocumentoIdentificacion Identificacion { get => identificacion; set => identificacion = value; }
        public string NombreComercial { get => nombreComercial; set => nombreComercial = value; }
        public Ubicacion Ubicacion { get => ubicacion; set => ubicacion = value; }
        public TelefonoBase Telefono { get => telefono; set => telefono = value; }
        public TelefonoBase Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }

        public Emisor(string nombre,
                      DocumentoIdentificacion identificacion,
                      Ubicacion ubicacion,
                      string email,
                      TelefonoBase telefono = null,
                      TelefonoBase fax = null,
                      string nombreComercial = "")
        {
            this.nombre = nombre;
            this.identificacion = identificacion;
            this.nombreComercial = nombreComercial;
            this.ubicacion = ubicacion;
            this.telefono = telefono;
            this.fax = fax;
            this.email = email;
        }

        public Emisor(string nombre, DocumentoIdentificacion identificacion, Ubicacion ubicacion, string email)
        {
            this.nombre = nombre;
            this.identificacion = identificacion;
            this.ubicacion = ubicacion;
            this.email = email;
        }

        public XElement GenerarXML()
        {
            var baseXML = new XElement("Emisor",
                                new XElement("Nombre", nombre),
                                identificacion.GenerarXML());
            
            if(!nombreComercial.Equals("")){
                baseXML.Add(new XElement("NombreComercial", nombreComercial));
            }

            baseXML.Add(ubicacion.GenerarXML());

            if(telefono!= null){
                baseXML.Add(telefono.GenerarXML());
            }

            if (fax != null)
            {
                baseXML.Add(fax.GenerarXML());
            }

            baseXML.Add(new XElement("CorreoElectronico", email));
            
            return baseXML;
        }

    }
}
