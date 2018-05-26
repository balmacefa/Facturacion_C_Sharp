# Features

Esta librería es una traducción de https://github.com/apokalipto/facturacr para C# .net.

Esta librería implementa los procesos de facturación electrónica del Ministerio de Hacienda de Costa Rica en Ruby. Puede ser utilizada 
como punto de partida para realizar la integración necesaria con el Ministerio de Hacienda.

Actualmente cuenta con las siguientes características:

- Generación de XML
-   Firmado de XML ( utilizando https://github.com/ctt-gob-es/FirmaXadesNet45 )
-   Comunicación con el API del ministerio de hacienda

## Consideraciones importantes

- El formado de las fechas debe ser "yyyy-MM-dd'T'HH:mm:sszzz"
- Emisor->Identificacion->Numero debe ser en formato crudo y no en 12 digitos como en el numero de secuencia

## Implementación

####C#　

## Configurar las variales
```c#
            var user = "**************";
            var userPass = "**********";

            var pin = "****";
            var p12 = "C:\\Users\\$$$$$\\Desktop\\060339051236.p12";

            var config = new Configuracion(user, userPass, p12, pin);
            var FH = new FacturacionHacienda(config);
```

## Crear un objeto emisor y receptor
```c#
            //Emisor
            var cedula = new DocumentoIdentificacion(DocumentoIdentificacion.TipoIdentificacion.Cedula_Fisica, "202220222");
            var telefono = new TelefonoBase(TelefonoBase.TipoTelefono.Telefono, "506", "88888888");
            var ubicacion = new Ubicacion("1", "01", "01", "OTRO");
            var email = "*****@***.com";

            var emisor = new Emisor("NOMBRE EMISOR", cedula, ubicacion, email);

            //Receptor
            var cedulaReceptor = new DocumentoIdentificacion(DocumentoIdentificacion.TipoIdentificacion.Cedula_Fisica, "101110111");
            var receptor = new Receptor("NOMBRE RECEPTOR", identificacion: cedulaReceptor);
```
## Crear items y tipo de documento
```c#
            var items = new List<Item>();
            items.Add(new Item("1", "1", "Sp", "Desarrollo de Software y Mantenimiento", 300, 300, 300, 300, new string[] { "001" }));

            var tax = new Impuesto(Impuesto.CodigoImpuesto.Impuesto_General_sobre_las_Ventas, 13, (float)(100 * 0.13));

            items.Add(new Item("2", "2", "Unid", "Impresora de POS", 50, 100, 100, 113, new string[] { "002" }, impuestos: new 
Impuesto[] { tax }));

            var resumenFac = new ResumenFactura(codigoMoneda: "USD", tipoCambio: 575, totalServExentos: 300, totalMercanciasGravadas: 
100, totalExento: 300, totalGravado: 100, totalVenta: 400, totalVentaNeta: 400, totalImpuesto: 13, totalComprobante: 413);

            var numeroFac = "24";
            var secCod = "3";
            var factura = new Documento(DateTime.Now, emisor, Documento.CondicionVenta.Contado,
                                        Documento.MedioPago.Efectivo, numeroFac, Documento.TipoDocumento.Factura_Electronica,
                                        secCod, items.ToArray(), resumenFac, Documento.SituacionDocumento.Normal, receptor);
```
## Firmar doc y enviar
```c#
            //firmar XML
            FirmadorXML.Firmar(config);

            //Enviar a Hacienda
            var esEnviado = FH.EnviarDocumento(factura, xmlFirmado);

            //Espera a Hacienda
            System.Threading.Thread.Sleep(2500);

            //Optener el estado de la factura
            var estado = FH.EstadoDocumento(factura.ClaveNumerica());
            Console.WriteLine(estado);
```

## TODO
- Verificar el XML contra el XSD

## Contacto
Fabián Balmaceda Rescia
fabianbalmaceda2@gmail.com
Telegram: @balmacefa



