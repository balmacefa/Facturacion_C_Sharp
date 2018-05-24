using System;
using System.Xml.Linq;
using Facturacion_C_Sharp.Utils;

namespace Facturacion_C_Sharp.Lib.DocumentoItems
{
    public class Receptor : IXMLGenerador
    {
        //        validates :name, presence: true, length: { maximum: 80 }
        //        validates :comercial_name, length: { maximum: 80 }
        //        validates :foreign_id_number, length: { maximum: 20 }

        private String nombre;
        private DocumentoIdentificacion identificacion;
        private String identificacionExtranjer;
        private String nombreComercial;
        private Ubicacion ubicacion;
        private TelefonoBase telefono;
        private TelefonoBase fax;
        private String email;

        public Receptor(string nombre,
                        DocumentoIdentificacion identificacion = null, string identificacionExtranjer="", string nombreComercial="",
                        Ubicacion ubicacion = null, TelefonoBase telefono = null, TelefonoBase fax = null, string email = "")
        {
            this.nombre = nombre;
            this.identificacion = identificacion;
            this.identificacionExtranjer = identificacionExtranjer;
            this.nombreComercial = nombreComercial;
            this.ubicacion = ubicacion;
            this.telefono = telefono;
            this.fax = fax;
            this.email = email;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public DocumentoIdentificacion Identificacion { get => identificacion; set => identificacion = value; }
        public string IdentificacionExtranjer { get => identificacionExtranjer; set => identificacionExtranjer = value; }
        public string NombreComercial { get => nombreComercial; set => nombreComercial = value; }
        public Ubicacion Ubicacion { get => ubicacion; set => ubicacion = value; }
        public TelefonoBase Telefono { get => telefono; set => telefono = value; }
        public TelefonoBase Fax { get => fax; set => fax = value; }
        public string Email { get => email; set => email = value; }

        public XElement GenerarXML()
        {
            var baseXML = new XElement("Receptor",
                                new XElement("Nombre", nombre));

            if (identificacion != null)
            {
                baseXML.Add(identificacion.GenerarXML());
            }

            if (!identificacionExtranjer.Equals(""))
            {
                baseXML.Add(new XElement("IdentificacionExtranjer", identificacionExtranjer));
            }

            if (!nombreComercial.Equals(""))
            {
                baseXML.Add(new XElement("NombreComercial", nombreComercial));
            }

            if (ubicacion != null)
            {
                baseXML.Add(ubicacion.GenerarXML());
            }

            if (telefono != null)
            {
                baseXML.Add(telefono.GenerarXML());
            }

            if (fax != null)
            {
                baseXML.Add(fax.GenerarXML());
            }

            if (!email.Equals(""))
            {
                baseXML.Add(new XElement("CorreoElectronico", email));
            }

            return baseXML;
        }
    }
}