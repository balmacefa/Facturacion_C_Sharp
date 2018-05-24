using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Facturacion_C_Sharp.Lib
{
    public class EstadoDocumento
    {
        private IRestResponse response;
        private XDocument repuestaXML;


        private String claveNumerica;
        private DateTime fecha;
        //["RECIBIDO", "PROCESANDO", "ACEPTADO", "RECHAZADO", "ERROR"]
        //"400"
        private String estadoEnHacienda;
        private String mensajeHacienda;

        public EstadoDocumento(IRestResponse response)
        {
            this.response = response;

            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {

                JObject json = JObject.Parse(response.Content);

                claveNumerica = json["clave"].ToString();
                fecha = DateTime.Parse(json["fecha"].ToString());
                estadoEnHacienda = json["ind-estado"].ToString().ToUpper();

                if (json["respuesta-xml"] != null)
                {
                    var xml64 = json["respuesta-xml"].ToString();
                    var string64 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(xml64));
                    repuestaXML = XDocument.Parse(string64);
                    mensajeHacienda = (from el in repuestaXML.Descendants() where el.Name.LocalName == "DetalleMensaje" select el).First().Value;
                }
            }
            else
            {
                estadoEnHacienda = "400";
                mensajeHacienda = "ERROR CON LA PETICION, es probable que no se encuentre la factura en el servidor de Hacienda";
            }

        }

        public override string ToString()
        {
            var ln = System.Environment.NewLine;
            return "Clave: " + claveNumerica + ln + "Fecha: " + fecha.ToLongTimeString() + ln + "Estado: " + estadoEnHacienda + ln + "Mensaje: " + mensajeHacienda;
        }

        public IRestResponse Response { get => response; }
        public XDocument RepuestaXML { get => repuestaXML; }
        public string ClaveNumerica { get => claveNumerica; }
        public DateTime Fecha { get => fecha; }
        public string EstadoEnHacienda { get => estadoEnHacienda; }
        public string MensajeHacienda { get => mensajeHacienda; }
    }
}
