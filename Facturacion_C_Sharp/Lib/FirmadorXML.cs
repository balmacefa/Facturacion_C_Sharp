using System;
using System.Diagnostics;

namespace Facturacion_C_Sharp.Lib
{
    public class FirmadorXML
    {
//https://github.com/CRLibre/API_Hacienda/blob/4d1c2ca3384817b3cfcf886586eb034e2a55133e/api/contrib/signXML/Firmadohaciendacr.php
        public static void ExecuteCommand(string command)
        {
            Process proc = new System.Diagnostics.Process();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/C " + command;
            }
            else
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = "-c \" " + command + " \"";
            }

            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.WaitForExit();
        }

        public static void Firmar(String javafile_path, Configuracion conf)
        {
            var null_device = Environment.OSVersion.Platform == PlatformID.Win32NT ? "/nul" : "/dev/null";
            var s = " ";

            var comando = "java -jar "
            + javafile_path
            + s + conf.Key_path
            + s + conf.Key_password
            + s + conf.Xml_sin_firmar_path
            + s + conf.Xml_firmado_path
            + s + "1>" + null_device
            + s + "2>" + null_device;

            ExecuteCommand(comando);

            //system("java -jar #{FE.bin}/signer/signer.jar #{@key_file} #{@password} #{@path} #{@out_path} 1>#{null_device} 2>#{null_device}")
        }
    }
}
