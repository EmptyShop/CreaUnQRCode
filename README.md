# CreaUnQRCode
Generación de Códigos QR
## Porqué y Para Qué
Este proyecto lo implementé cuando aprendí algo de Net Core y además estaba aprendiendo a leer códigos QR, entonces se me ocurrió generar una aplicación que genere códigos QR para usarlos en mi aplicación de lectura con el teléfono. De paso, practiqué lo que había aprendido de Net Core.
## Cómo está Hecha la Aplicación
Usé Visual Studio 2022 para codificarla, la versión de Net Core que usé es la 6.0.202; también usé QRCoder 1.4.3. Y eso es todo.

Generé una aplicación web con un control de texto y un botón para generar el código QR. El código en c# que hace la magia es el siguiente:

```sh
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode(Mensaje, QRCodeGenerator.ECCLevel.Q);
BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);

byte[] qrCodeAsBitmapByteArr;

qrCodeAsBitmapByteArr = qrCode.GetGraphic(parametrosApp.PixelsBloque,
parametrosApp.DarkColor, parametrosApp.LightColor);

elQRCode = new String("data:image/png;base64," + Convert.ToBase64String(qrCodeAsBitmapByteArr));
```

La propiedad `Mensaje` de la clase principal está vinculada con el control de texto para escribir el mensaje que se va a codificar. La propiedad `elQRCode` de la clase principal es la que contiene el código QR en un formato PNG. En la parte HTML de la clase desplegamos el contenido de esta propiedad:

```sh
@{
    if (!String.IsNullOrEmpty(Model.elQRCode))
    {
        <div class="col-md-6">
            <p class="pl-4 pt-1 ml-3 mb-0 lead font-weight-bold">Tu mensaje en QRCode:</p>
            <img src="@Model.elQRCode" />
        </div>                    
    }
}
```
## Eso Fue Todo
Y ya, fue una aplicación sencilla pero muy didáctica. La librería QRCoder resultó muy rápida y ligera; además contiene varias opciones como colores, tamaños, tipos de imágenes, tipos de códigos QR. El único detalle adverso que encontré fue que para Net Core 6, la librería QRCoder sólo permite un reducido conjunto de opciones. Espero que en el futuro ya puedan soportar las últimas versiones de Net Core.

Les dejo la información que me fue muy útil para este proyecto:

  * [NuGet Package QRCoder](https://nugetmusthaves.com/Package/QRCoder)
  * [BitmapByteQRCode-Renderer in detail](https://github.com/codebude/QRCoder/wiki/Advanced-usage---QR-Code-renderers#24-bitmapbyteqrcode-renderer-in-detail)
  * [Why are some renderers missing](https://github.com/codebude/QRCoder/issues/361)

Gracias por interesarse en este proyecto. Hasta pronto.
