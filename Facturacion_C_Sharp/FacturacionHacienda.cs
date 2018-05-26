using System;
using Facturacion_C_Sharp.Lib;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Facturacion_C_Sharp
{
    public class ExecpcionFacturacionHacienda : Exception
    {
        public ExecpcionFacturacionHacienda()
        {
        }

        public ExecpcionFacturacionHacienda(string message)
            : base(message)
        {
        }

        public ExecpcionFacturacionHacienda(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ExDocumentoNoAceptado : Exception
    {
        public ExDocumentoNoAceptado()
        {
        }

        public ExDocumentoNoAceptado(string message)
            : base(message)
        {
        }

        public ExDocumentoNoAceptado(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class FacturacionHacienda
    {
        RestClient restClient = new RestClient("https://api.comprobanteselectronicos.go.cr/");

        private String token;

        private Configuracion configuracion;

        //Revisar en caso de error, almacena la ultima respuesta de peticion al server
        private IRestResponse response;

        private String mensajeError;

        public FacturacionHacienda(Configuracion configuracion)
        {
            this.configuracion = configuracion;
        }

        public void Autenticar()
        {
            var request = new RestRequest(configuracion.Authentication_endpoint, Method.POST);
            request.AddParameter("name", "value");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", configuracion.Api_username);
            request.AddParameter("password", configuracion.Api_password);
            request.AddParameter("client_id", configuracion.Api_client_id);
            request.AddParameter("client_secret", "");
            request.AddParameter("scope", "");

            // execute the request
            response = restClient.Execute(request);
            var status = response.StatusCode;

            if (status != System.Net.HttpStatusCode.OK)
            {
                mensajeError = response.ErrorMessage;
                throw new ExecpcionFacturacionHacienda("Error autentificacion: " + response.ErrorMessage);
            }
            JObject json = JObject.Parse(response.Content);

            token = json["access_token"].ToString();
        }

        public Configuracion Configuracion { get => configuracion; set => configuracion = value; }
        public IRestResponse Response { get => response; set => response = value; }
        public string MensajeError
        {
            get => mensajeError;
            set => mensajeError = value;
        }

        public void GuardarXML_NO_Firmado(Documento doc, String pathXML_NO_Firmado)
        {
            doc.OptenerXML_Nofirmado().Save(pathXML_NO_Firmado, System.Xml.Linq.SaveOptions.DisableFormatting);
        }

        public bool EnviarDocumento(Documento documento, String pathXML)
        {
            Autenticar();
            var request = new RestRequest(configuracion.Documents_endpoint + "/recepcion", Method.POST);
            request.AddHeader("Authorization", "bearer " + token);

            //request.AddJsonBody(documento.JsonPayload(pathXML).ToString());

            request.AddHeader("Accept", "application/json");
            //request.Parameters.Clear();
            request.AddParameter("application/json", documento.JsonPayload(pathXML).ToString(), ParameterType.RequestBody);


            // execute the request
            response = restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public EstadoDocumento EstadoDocumento(String claveNumerica)
        {
            Autenticar();
            var request = new RestRequest(configuracion.Documents_endpoint + "/recepcion/"+claveNumerica, Method.GET);
            request.AddHeader("Authorization", "bearer " + token);
            request.AddHeader("content_type", "json");
            // execute the request
            response = restClient.Execute(request);
            return new EstadoDocumento(response);
        }

        public string ErroresEnvio ( )
        {
            return mensajeError;
        }

        //public EstadoDocumento ObtenerEstadoDocumento(String claveNumerica)
        //{
        //    Autenticar();
        //}
    }
}