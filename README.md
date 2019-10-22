# C-Sharp-Color-Detection-IP-Camera
IP Kameradan yayınlanan görüntüdeki renklerin algılanması

Prototipin Çalışması Çalışabilmesi için 2 Adet Frameworke ihtiyaç vardır
1-AForge.Video
2-Microsoft.VisualBasic

AForge.Video NuGet Package Olarak Projeye Dahil Edilebilmektedir.
Microsoft.VisualBasic ise 'Add Reference' kısmından eklenebilir.

Son Olarak Using Satırları Eklenmelidir.

using AForge.Video;
using Microsoft.VisualBasic;  

Görüntü aktarımı için Android Bir Cep Telefonuna IP WebCam adlı uygulamayı yükledim. Bu uygulama IP üzerinden kamera görüntüsüne erişim sağlıyor.

https://play.google.com/store/apps/details?id=com.pas.webcam
