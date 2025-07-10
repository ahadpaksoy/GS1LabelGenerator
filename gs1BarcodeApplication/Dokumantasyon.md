## GS1 LABEL BUILDER DOKUMANTASYON

GS1 Label builder dinamik olaak GS1 standartı etiket, barcode ve QR kod oluşran web tabanlı bir araçtır. Önceden tanımlanmış veya kullanıcı tarafından yüklenmiş şablonları kullanarak karmaşık GS1 etiketleri oluşturur, gerçek zamanlı olarak bir önzileme sunar. Etiket, barcode, QR code'u PDF formatında hazırlar. Bu web tabanlı araç ASP.NET MVC (.Ner Framework) ve modern web teknolojilerinin birleşimi ile oluşurulmuştur.

<table>
  <tr>
    <td align="center"><strong>Input Page</strong></td>
    <td align="center"><strong>Submit Page</strong></td>
  </tr>
  <tr>
    <td>
      <img src="https://i.imgur.com/JuBF88p.png" alt="GS1 Label Builder Input Page" width="100%">
    </td>
    <td>
      <img src="https://i.imgur.com/bm4ac1d.png" alt="GS1 Label Builder in Submit Page" width="100%">
    </td>
  </tr>
</table>

---

### Özellikler

- **Preset-Driven UI** : Araç şablonları kullanarak yeni alanlar ekler, bu şekilde kullanıcı hatalarını minimize etmiş olur.
- **Dinamik Şablon Ekleme**: 
	- Web.config dosyasına gömülmüş olan şablonları otomatik olarak yükler.
	- Kullanıcılar JSON formatında oluşturdukları şablonları araca yükleyerek kullanabilirler.
- **Gerçek Zamanlı Önizleme**: Kullanıcıların her dolduruğu alandan sonra önizlemede olan QR kodu ve Etiket gerçek zamanlı olarak güncellenir.
- **AJAX Tabanlı From Yükleme** : Formun gönderilmesi eşzamansız olarak gerçekleştirilir ve sayfa yeniden yüklenmeden hızlı bir kullanıcı deneyimi sağlar.
- **Model-Tabanlı Sonuç**: Oluşturulan etiket, barcode ve QR kod temiz bir pop ile kullanıcıya sunulur.
- **PDF Çıktı**:  Oluturulan tüm bilgilerin oluğu A6 formatında yazdırılmaya hazır bir pdf oluşturulur.
- **Kullanıcı ve Sunucu Doğrulama** : Tarayıcıda ve sunucuda kullanıcı girdisini doğrulayarak veri bütünlüğünü sağlar.
- **Güvenlik**: CSRF saldırılarına karşı koymak için Antı-Forgery tokenleri kullanır.

---

### Teknoloji Yığını

- **Backend:**
    
    - **Framework:** ASP.NET MVC 5 (.NET Framework 4.6.1)
        
    - **Dil:** C#
        
    - **Barcode Oluşturma:** ZXing.Net
        
    - **QR Code Oluşturma:** QRCoder
        
    - **PDF Oluşturma:** iTextSharp (legacy)
        
    - **JSON:** Newtonsoft.Json
        
- **Frontend:**
    
    - **Dil:** JavaScript (ES6+)
        
    - **AJAX:** Native Fetch API
        
    - **UI Framework:** Bootstrap 5
        
    - **QR Code Önizleme:** qrcode.js
        
- **Geliştirme Ortamı:**
    
    - Visual Studio 2017/2019/2022
        
    - IIS Express

---

### Proje Mimarisi

- **Controllers/HomeController.cs**: Ana Controller yapısı.
	- Index() : Web.config'ten alıdığı şablonları ana görüntüye servis eder.
	- Submit() : Ajax endpoint'i. Form verisini alır, doğrulama yapar, etiket verisini oluşturur ve *JsonResult* döndürür.
	- ExportPdf() : base64 resim verisini alır ve indirilebilir bir PDF dosyası oluşturur.
- **Views/Home/Index.cshtml:** tüm uyuglama için tek sayfa arayüz kullanır. Tüm html, css ve kullanıcı tarafı jS kodlarını içerir.
- **Models/:** Veri modellerini içerir.
	- ProductLabelModel.cs : Olabilecek tüm GS1 alanlarını içeren ana model.
	- Gs1FieldInputModel.cs : Gelen form verilerin bağlandığı basit bir model.
- **Helpers/Gs1Builder.cs** : GS1 etiketinin oluşturan merkesi sunucu taraflı mantık bloğu burada bulunur.
- **Web.config** : Default şablonları *appSettings* alanında tutar.

---

### Defaul Şablon Konfigürasyonu

Şablonları *Web.config* dosyası içerinde bulunan *appSettings* alanına ekle. *key* *preset:* değeri ile başlamalıdır ve *value* içerindeki değerler virgülle ayrılmış *ProductsLableModel.cs* dosyasında bulunan modellerdeno oluşmalıdır, aralarında boşluk olmamalıdır.

```xml
<appSettings>
  <add key="preset:💊 Pharma Label" value="GTIN,batch_lotNumber,expirationDate,serialNumber" />
  <add key="preset:🍅 Food Label" value="GTIN,batch_lotNumber,bestBeforeDate,netWeightKg" />
</appSettings>
```

---

### Kişisel Şablon Yükleme Konfigürasyonu

Kullanıcı aşağıdaki yapıda gösterilen şekilde JSON dosyalarını yükleyebilir.

```json
{
  "My Custom Logistics Label": [
    "sscc",
    "GTIN",
    "countContained",
    "customerPONumber"
  ],
  "Another Custom Template": [
    "GTIN",
    "serialNumber"
  ]
}
```

---

### Kurulum ve Çalıştırma

1. Repoyu klonla
2. Visual Studio'da *(.snl)* dosyasını aç
3. Tüm NuGet paketlerini yenile (solutiona sağ tık ile tıkla -> "Restore NuGet Packages").
4. Eğer default presetler yoksa Web.config içerisine ekleyebilirsin. (Format için Web.config dosyasına bak.)
5. F5 tuşuna ya da "IIS Express" butonuna basarak uygulamayı çalıştır.

---

### Nasıl Kullanılır

1. **Uygun Şablonu Seç :** Dahili şablonlardan birini menuden seç. Gerekli alanlar otomatik olarak gelecektir.
2. **Kişisel Şablon Yükle (Opsiyonel) :** Kendi oluşturduğun JSON dosyasına doğru etiketleri koyduğundan emin ol. *Upload File* butonuna tıklayarak JSON dosyanı yükle. Eklenen şablonlar menude *X şablonu (Uploaded)* şeklinde gelecektir. Eğer eklediğin alanı göremiyorsan lütfen etiketleri kontrol et!
3. **Alanları Doldur :** Şablondaki alanları uygun şekilde doldur. Doğru şekilde doldurulan alanlar gerçek zamanlı şekilde GS1 etiketini ve QR kodu değiştirecektir.
4. **Oluştur :** Tüm alanları doldurduktan sonra *Generate Label & PDF* butonuna tıkla. Açılan pop-upta final barkodu, etiketi ve QR kodu oluşturulmuş olacaktır. *Download PDF* butonuna tıklayarak dosyayı indirebilirsin. *Close* butonu ile gelen pop-upı kapatabilirsin.
