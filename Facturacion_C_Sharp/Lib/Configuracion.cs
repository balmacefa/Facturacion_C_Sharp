using System;
namespace Facturacion_C_Sharp.Lib
{
    public class Configuracion
    {
        private String api_username;
        private String api_password;
        private String key_path;
        private String key_password;


        private String xml_sin_firmar_path;
        private String xml_firmado_path;

        private String api_client_id = "api-stag";
        private String documents_endpoint = "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v1";
        private String authentication_endpoint = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";

        public Configuracion(string api_username, string api_password, string key_path, string key_password)
        {
            this.api_username = api_username;
            this.api_password = api_password;
            this.key_path = key_path;
            this.key_password = key_password;
        }

        public Configuracion(string api_username, string api_password, string key_path, string key_password, string xml_sin_firmar_path, string xml_firmado_path)
        {
            this.api_username = api_username;
            this.api_password = api_password;
            this.key_path = key_path;
            this.key_password = key_password;
            this.xml_sin_firmar_path = xml_sin_firmar_path;
            this.xml_firmado_path = xml_firmado_path;
        }

        public string Api_username { get => api_username; set => api_username = value; }
        public string Api_password { get => api_password; set => api_password = value; }
        public string Key_path { get => key_path; set => key_path = value; }
        public string Key_password { get => key_password; set => key_password = value; }
        public string Xml_sin_firmar_path { get => xml_sin_firmar_path; set => xml_sin_firmar_path = value; }
        public string Xml_firmado_path { get => xml_firmado_path; set => xml_firmado_path = value; }
        public string Api_client_id { get => api_client_id; set => api_client_id = value; }
        public string Documents_endpoint { get => documents_endpoint; set => documents_endpoint = value; }
        public string Authentication_endpoint { get => authentication_endpoint; set => authentication_endpoint = value; }
    }
}
