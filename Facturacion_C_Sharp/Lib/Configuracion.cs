using System;
namespace Facturacion_C_Sharp.Lib
{
    public class Configuracion
    {
        private String api_username;
        private String api_password;

        private String rutaLlaveCriptografica;
        private String pinLlaveCriptografica;

        private String api_client_id = "api-stag";
        private String documents_endpoint = "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1";
        private String authentication_endpoint = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";

        public Configuracion ( string api_username, string api_password, string rutaLlaveCriptografica, string pinLlaveCriptografica )
        {
            this.api_username = api_username;
            this.api_password = api_password;
            this.rutaLlaveCriptografica = rutaLlaveCriptografica;
            this.pinLlaveCriptografica = pinLlaveCriptografica;
        }

        public string Api_username
        {
            get => api_username;
            set => api_username = value;
        }
        public string Api_password
        {
            get => api_password;
            set => api_password = value;
        }
        public string RutaLlaveCriptografica
        {
            get => rutaLlaveCriptografica;
            set => rutaLlaveCriptografica = value;
        }
        public string PinLlaveCriptografica
        {
            get => pinLlaveCriptografica;
            set => pinLlaveCriptografica = value;
        }
        public string Api_client_id
        {
            get => api_client_id;
            set => api_client_id = value;
        }
        public string Documents_endpoint
        {
            get => documents_endpoint;
            set => documents_endpoint = value;
        }
        public string Authentication_endpoint
        {
            get => authentication_endpoint;
            set => authentication_endpoint = value;
        }
    }
}
