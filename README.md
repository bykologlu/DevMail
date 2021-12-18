# DevMail
.Net Core projeleri için düz metin veya Html template email gönderim kütüphanesi.

### Nuget ile Kurulum

...

### Yapılandırma

#### appsettings.json　

.Net Core Web veya API projenizde yer alan appsetting.json dosyasına aşağıdaki bloğu ekleyerek kendi email hesabınıza göre yapılandırmanız gerekmektedir.

```json
  "MailSetting": {
    "SenderMailAddress": "mymail@gmail.com",
    "DisplayName": "My Company",
    "Password": "mypassword"
    "Host": "smtp.gmail.com",
    "Port": 587
  }
```

#### Startup.cs

Mail gönderimi için kullanacağınız MailManager sınıfını ve mail yapılandırma ayarlarını appsettings.json'dan okuyabilmek için aşağıdaki gibi yapılandırmanız gerekmektedir.

```cs
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.Configure<MailSetting>(Configuration.GetSection("MailSetting"));
        services.AddSingleton<IMailService, MailManager>();        
        ...
    }
```

### Kullanım Örnekleri

İhtiyacınıza göre birden fazla kullanım örneği mevcuttur. Aşağıda bir kaç farklı kod bloğu ile çeşitli kullanım örnekleri gösterilmiştir.

```cs
    public class MyBusinessClass
    {
        private readonly IMailService _mailService;

        public MyBusinessClass(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task SendMail()
        {
            // Tek kişiye standart metin gönderim örneği
            SendResult example1 = await _mailService.SendEmailAsync(
                                                                new NewMail(
                                                                    "test@mail.com", 
                                                                    "Hoşgeldiniz.", 
                                                                    "Sitemize üye olduğunuz için teşekkür ederiz."
                                                                    ));
                                                                    
            // Birden fazla kişiye dosya eki ile birlikte standart metin gönderim örneği                                                                    
            SendResult example2 = await _mailService.SendEmailAsync(
                                                                new NewMail(
                                                                    new List<string> { "test@mail.com", "test2@mail.com" },                                                                    
                                                                    "Hoşgeldiniz",
                                                                    "Sitemize üye olduğunuz için teşekkür ederiz.",
                                                                    new List<string> { "filepath-1", "filepath-2" }
                                                                    ));

            // Tek kişiye kütüphane içinde hazır tanımlanmış kişiye özel düzenlenmiş Html gönderim                                                                    
            SendResult example3 = await _mailService.SendEmailAsync(
                                                                new NewMail(
                                                                    "test@mail.com",
                                                                    "Hoşgeldiniz.",
                                                                    new TemplateContent(EmailTemplates.EmailConfirmation)
                                                                    .AddCompanyName("ABC Yazılım")
                                                                    .AddMessage("Kaydınızı tamamlayabilmemiz için aşağıdaki <b>Hesabımı Onayla</b> butonuna tıklamanız gerekiyor.")
                                                                    .AddValidationCode("523463")
                                                                    .AddValidationLink("https://test.com")                                                                    
                                                                    ));
                                                                    
            // Birden fazla kişiye dosya eki ile kendi özel Html templateniz için gönderim örneği                                                                                                                                     
            NewMail newMail = new NewMail();
            newMail.AttachedFiles = new List<string> { "filepath-1", "filepath-2" };
            newMail.ReceiverMailAddresses = new List<string> { "test@mail.com", "test2@mail.com" };
            newMail.Subject = "Email Doğrulama";
            newMail.Content = new MailContent().UseTemplate("custom-template.html")
                                            .AddCompanyName("ABC Yazılım")
                                            .AddMessage("Kaydınızı tamamlayabilmemiz için aşağıdaki <b>Hesabımı Onayla</b> butonuna tıklamanız gerekiyor.")
                                            .AddValidationCode("523463")
                                            .AddValidationLink("https://test.com");

            SendResult example4 = await _mailService.SendEmailAsync(newMail);                                                                    
        }
    }
```

### Özel Template Hazırlama

Kendi templatelerinizi hazırlarken aşağıdaki belirteçleri tanımlayarak gönderim esnasında dinamik içerik güncellemesi yapabilirsiniz.


| Belirteç          | Değiştiren Metod                                            |
| ------------------| -----------------------------------------------------------:|
| {company_name}    | AddCompanyName("ABC Yazılım")                               |
| {company_logo}    | AddCompanyLogo("logo-file-path.jpg")                        |
| {validation_link} | AddValidationLink("https://test.com")                       |
| {validation_code} | AddValidationCode("123456")                                 |
| {message}         | AddMessage("Sitemize üye olduğunuz için teşekkür ederiz.")  |

### Sonuç

![](https://drive.google.com/uc?export=view&id=1XyisuAyJ7UX3SJYLUbJgS1cuTXMJ0SeR)

### Yapılacaklar
             
- Hoşgeldiniz mail HTML şablonu (EmailTemplates.Wellcome)
- Şifre sıfırlama HTML şablonu (EmailTemplates.PasswordReset)
- Fatura gönderim HTML şablonu 
- Özel gün kutlama HTML şablonu 



