![sarman.jpg](../../sarman.jpg)
# API Tasarımı Rehberi
API tasarımı, istemci ve geliştirici için kullanıcı dostu, güvenli ve verimli bir deneyim sunmanın temelidir. 
Bu rehberde, bir API’nin nasıl tasarlanması gerektiğine ve HTTP yöntemlerinin doğru kullanımına dair yol gösterici bilgileri sizlerle paylaştım.

## HTTP Yöntemlerini Doğru Kullanın

HTTP yöntemleri, Client ile Server arasındaki iletişimi düzenler ve her bir yöntemin belirli bir amacı vardır.
Amacı dışında kullanılmamasına özen göstermek faydalı olacaktır. Özellikle junior arkadaşların bu konuda biraz daha dikkatli olması önemli; zamanla tecrübe kazanarak bu tür detaylara daha kolay alışacaklardır.

| HTTP Method | Açıklama                                                                                                                                      | Kullanım Senaryosu                                                                      | 
|-------------|-----------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| **GET**     | Sunucudan data almak için kullanılır. Güvensizdir ve hassas veri taşımaya uygun değildir. Şifreler ve tokenler bu yöntemle kullanılmamalıdır. | Kullanıcı bilgileri, ürün listeleri veya belirli bir veri almak. Ör: `/users/123`       | 
| **POST**    | Yeni bir data oluşturmak için kullanılır. Get işlemine göre daha güvenlidir.                                                                  | Yeni kullanıcı oluşturma, form gönderimi. Ör: `/users` ile kullanıcı ekleme.            | 
| **PUT**     | Var olan bir datayı tamamen günceller veya yeni bir data oluşturur.                                                                           | Kullanıcı bilgilerini güncelleme. Ör: `/users/123` ile tüm verileri değiştirme.         | 
| **PATCH**   | Var olan bir datanın belirli bir kısmını günceller.                                                                                           | Kullanıcının yalnızca adını güncelleme. Ör: `/users/123` ile `name` alanını değiştirme. | 
| **DELETE**  | Bir datayı siler.                                                                                                                             | Kullanıcı silme. Ör: `/users/123` ile kullanıcıyı kaldırma.                             | 
| **HEAD**    | GET ile aynıdır, ancak yalnızca başlık (header) bilgilerini döndürür.                                                                         | Data varlığını veya meta verilerini kontrol etme. Ör: `/users/123` için başlık alma.    | 
| **OPTIONS** | Sunucunun desteklediği HTTP yöntemlerini ve seçenekleri bildirir.                                                                             | CORS veya API yeteneklerini sorgulama. Ör: `/users` için izin verilen yöntemler.        | 

## API Tasarımı İçin İpuçları

1. **Anlamlı Endpoint'ler Kullanın**: URL'ler açık ve tahmin edilebilir olmalıdır. Örneğin, `/users` kullanıcı listesini, `/users/123` ise belirli bir kullanıcıyı temsil eder.
2. **Hata Mesajlarını Netleştirin**: Hatalar için standart HTTP durum kodları (ör. `404 Not Found`, `400 Bad Request`) ve açıklayıcı mesajlar kullanın.
3. **Versiyonlama Uygulayın**: API değişikliklerini yönetmek için versiyonlama kullanın (ör. `/v1/users`).
4. **Güvenliği Ön Planda Tutun**: Kimlik doğrulama (ör. OAuth, JWT) ve veri şifreleme (HTTPS) ile API'nizi koruyun.
5. **Dokümantasyon Sağlayın**: API'nizin nasıl kullanılacağını açıklayan kapsamlı bir dokümantasyon sunun (ör. OpenAPI/Swagger, Scalar).

## Endpointlerin Doğru ve Yanlış Kullanımı

Endpoint'ler, API'nin yapısal(structure) taşlarıdır. Anlamlı, tutarlı ve REST prensiplerine uygun olmalıdır.

### Doğru Kullanımlar
- **Anlamlı ve Hiyerarşik Yapı**:
    - `/users`: Tüm kullanıcıların listesini döndürür.
    - `/users/123`: Belirli bir kullanıcının detaylarını döndürür.
    - `/users/123/orders`: Belirli bir kullanıcının siparişlerini listeler.
    - **Neden Doğru?**: Kaynaklar arasında mantıksal bir hiyerarşi kurar ve tahmin edilebilir bir yapı sunar.
- **Fiil Yerine İsim Kullanımı**:
    - `/products` yerine `/get-products` kullanmak yerine, kaynak odaklı bir isimlendirme tercih edilmelidir.
    - **Neden Doğru?**: REST prensiplerine uygun olarak, URL'ler fiiller yerine kaynak isimlerini yansıtmalıdır.
- **Filtreleme ve Sıralama için Query Parametreleri**:
    - `/products?category=electronics&sort=price`: Elektronik ürünleri fiyat sırasına göre listeler.
    - **Neden Doğru?**: Query parametreleri, veri filtreleme ve sıralama gibi dinamik işlemleri destekler.
- **Durum Kodlarının Doğru Kullanımı**:
    - `1xx`: Bilgilendirmelerdir.
    - `2xx`: Başarılı istekleri belirtir fakat farklı anlamlar taşıyabilir.
      - `201 Created`: Kaynağın sunucu tarafında başarı ile oluşturulmasıdır.
      - `204 No Content`: İstek başarıyla alınmış olmasına rağmen içeriğin dönülmediği durumdur.
    - `3xx`: Yönlendirmelerdir.
      - `301 Moved Permanently`: Kaynağın kalıcı olarak taşınmasıdır.
      - `302 Moved Temporarily`: Kaynağın gecici olarak taşınmasıdır.
      - `306 Moved Temporarily`: Kodun artık kullanılmadığını belirtir.
      - `307 Temporary Redirect`: Kaynağı gecici olarak başka bir kaynağa yönlendirir.
    - `4xx`: İstemci hatalarıdır.
      - `400 Bad Request`: Gelen isteğin yapımızla uyuşmadığını bildirir.
      - `401 Unauthorized`: Kimlik doğrulaması gereklidir.
      - `403 Forbidden`: Erişim hakkı yoktur.
      - `404 Not Found`: İstek atılan kaynağın bulunamadığını bildirir.
      - `408 Request Timeout`: İsteğe ait zaman aşımıdır.
      - `409 Conflict`: Sektör içinden bir bilgi verelim. Bu durum kodu kayıtlı veri olduğu takdirde iletilir.
    - `5xx`: Sunucu taraflı hatalardır.
      - `502 Bad Gateway`: Gateway'den yanıt alınmadığı durumlarda gönderilir.
      - `503 Service Unavaible`: Servis'in hizmet vermediği durumlardır.
      - `504 Gateway Timeout`: Gateway'in zaman aşımına uğramasıdır.

### Yanlış Kullanımlar
-  **Result / Response Modelleri ?**:
  - Yanlış: Bad Request atılmış bir response için 200 olarak iletilmesi.
    - **Neden Yanlış**: Data Result yapınız var iyi güzel fakat bu yapı doğru status code dönüyor mu? buna cevap vermeniz gerekir. İstediğiniz kadar result yapınız olsun, result'ın doğru planning'i yapılmadığı takdirde yanlıştır.
- **Fiil Tabanlı Endpoint'ler**:
    - Yanlış: `/create-user`, `/update-product`, `/delete-order`. Muhammetim'in yayınında bahsettiğim konudur.
    - **Neden Yanlış?**: Fiiller, HTTP yöntemleri tarafından zaten tanımlanır (`POST`, `PUT`, `DELETE`). Endpoint'ler kaynakları temsil etmelidir.
    - **Doğru Alternatif**: `/users` (POST için), `/products/123` (PUT için), `/orders/123` (DELETE için).
- **Anlamsız veya Karmaşık URL'ler**:
    - Yanlış: `/api/v1/action/performUserDeletion/123`.
    - **Neden Yanlış?**: Karmaşık ve gereksiz kelimeler içerir, REST prensiplerine aykırıdır.
    - **Doğru Alternatif**: `DELETE /users/123`.
- **Query Parametrelerinin Yanlış Kullanımı**:
    - Yanlış: `/users/123?delete=true` (kullanıcıyı silmek için).
    - **Neden Yanlış?**: Silme işlemi bir `DELETE` yöntemiyle yapılmalıdır, query parametreleri bu amaçla kullanılmaz.
    - **Doğru Alternatif**: `DELETE /users/123`.
- **Tutarlı Olmayan İsimlendirme**:
    - Yanlış: `/users` ve `/customer` aynı kaynak için kullanılıyorsa.
    - **Neden Yanlış?**: Aynı kaynak için farklı isimler kafa karışıklığına yol açar.
    - **Doğru Alternatif**: Tek bir isimlendirme, örneğin `/users`, tutarlı bir şekilde kullanılmalıdır.

## Versiyonlama

API versiyonlama, API'nizin gelişimi sırasında mevcut client'ların kesintisiz çalışmasını sağlamak ve yeni özellikler veya değişiklikler sunmak için kritik bir stratejidir. Versiyonlama, API'nin eski sürümlerini desteklerken yeni sürümlerin tanıtılmasını mümkün kılar.

### 1. URL Tabanlı Versiyonlama
- Versiyon numarası, URL'nin bir parçası olarak belirtilir (ör. `/v1/users`, `/v2/users`).
- **Örnek**:
    - `GET /v1/users/123` (Sürüm 1)
    - `GET /v2/users/123` (Sürüm 2, yeni özellikler veya farklı veri yapısı ile).
- **Avantajlar**:
    - Basit ve anlaşılır bir yaklaşımdır.
    - İstemciler hangi sürümü kullandıklarını açıkça görebilir.
    - Tarayıcı ve önbellekleme sistemleriyle uyumludur.
- **Dezavantajlar**:
    - URL'ler uzayabilir ve karmaşıklaşabilir.
    - Eski sürümlerin uzun süre desteklenmesi, bakım yükünü artırabilir.
- **Kullanım Senaryosu**: Büyük ve köklü değişiklikler (breaking changes) içeren API'ler için uygundur. Örneğin, bir veri modelinin tamamen değişmesi durumunda.

### 2. Query Parametresi ile Versiyonlama
- Versiyon, bir query parametresi ile belirtilir (ör. `/users?version=1`).
- **Örnek**:
    - `GET /users?version=1`
    - `GET /users?version=2`
- **Avantajlar**:
    - URL yapısı temiz kalır.
    - Esnek bir şekilde farklı sürümler arasında geçiş yapılabilir.
- **Dezavantajlar**:
    - Versiyonun belirtilmemesi durumunda varsayılan davranış belirsiz olabilir.
    - Önbellekleme sistemlerinde sorunlara yol açabilir, çünkü aynı URL farklı sonuçlar döndürebilir.
- **Kullanım Senaryosu**: Küçük çaplı değişiklikler veya geçici test sürümleri için kullanılabilir, ancak uzun vadeli projelerde önerilmez.

### 3. Başlık (Header) Tabanlı Versiyonlama
- Versiyon, HTTP başlığında belirtilir (ör. `Accept: application/vnd.example.v1+json`).
- **Örnek**:
    - İstek: `GET /users` ile `Accept: application/vnd.example.v1+json`
    - Yanıt: Sürüm 1'e uygun veri.
- **Avantajlar**:
    - URL'ler temiz ve sabit kalır.
    - Daha karmaşık versiyonlama stratejileri (ör. medya türü versiyonlaması) için uygundur.
- **Dezavantajlar**:
    - istemci için uygulanması daha karmaşıktır.
    - Başlıkların doğru yapılandırılmaması hatalara yol açabilir.
- **Kullanım Senaryosu**: Medya türüne (ör. JSON, XML) dayalı farklı veri formatları sunan API'ler için idealdir.

### 4. İçerik Tabanlı Versiyonlama (Content Negotiation)
- İstemci, `Accept` başlığında istediği veri formatını ve sürümünü belirtir (ör. `Accept: application/vnd.example+json;version=1.0`).
- **Örnek**:
    - İstek: `GET /users` ile `Accept: application/vnd.example+json;version=1.0`
    - Yanıt: Sürüm 1.0 formatında veri.
- **Avantajlar**:
    - URL yapısını değiştirmez, REST prensiplerine daha uygundur.
    - Esnek ve güçlü bir yaklaşımdır.
- **Dezavantajlar**:
    - İstemci tarafında daha fazla yapılandırma gerektirir.
    - Dokümantasyon ve hata ayıklama süreçleri karmaşıklaşabilir.
- **Kullanım Senaryosu**: Gelişmiş API'ler ve farklı veri formatlarını destekleyen sistemler için uygundur.

### 5. Versiyonlama Olmadan Çalışma (Deprecation)
- API'de versiyonlama kullanılmaz; bunun yerine eski özellikler kademeli olarak kaldırılır (deprecation) ve istemci yeni yapıya geçiş yapar.
- **Örnek**:
    - Eski bir alan (`user_name`) kaldırılır ve yerine yeni bir alan (`full_name`) eklenir. İstemciye geçiş için süre tanınır.
- **Avantajlar**:
    - URL ve yapı basit kalır.
    - Bakım yükü azalır, çünkü yalnızca tek bir sürüm desteklenir.
- **Dezavantajlar**:
    - Köklü değişiklikler (breaking changes), İstemci için kesintilere yol açabilir.
    - Geçiş süreci iyi yönetilmezse kullanıcı deneyimi zarar görebilir.
- **Kullanım Senaryosu**: Küçük ölçekli veya iç kullanım için olan API'ler için uygundur, ancak büyük ve halka açık API'lerde risklidir.

### Küçük Versiyon Numaraları (örn. v1.4) Kullanımı
- Canlı yayında enes arkadaşımızın bahsettiği `v1.4` kullanımını pek doğru bulmuyorum. Küçük versiyonlar, geriye dönük uyumlu değişiklikler için fazla karmaşıklık yaratabilir ve bakım yükünü artırabilir. Bunun yerine, yalnızca ana versiyon numaraları (ör. v1, v2) kullanarak daha basit ve yönetilebilir bir yapı tercih edilmelidir.

### Versiyonlama İçin En İyi Uygulamalar
- **Bildirim Yapın**: Yeni bir sürüm yayınlandığında veya eski bir sürüm kaldırılmadan önce istemciye yeterli süre ve dokümantasyon sağlayın.
- **Eski Sürümleri Destekleyin**: Eski sürümleri belirli bir süre (ör. 6-12 ay) destekleyerek istemcinin geçiş yapmasını kolaylaştırın.
- **Dokümantasyonu Güncel Tutun**: Her sürüm için açık ve ayrıntılı dokümantasyon sağlayın.
- **Köklü Değişiklikleri Azaltın**: Mümkünse, mevcut API'yi bozmadan yeni özellikler ekleyin (ör. yeni alanlar eklemek yerine mevcut alanları kaldırmaktan kaçının).
- **Otomatik Testler Kullanın**: Farklı sürümlerin doğru çalıştığını doğrulamak için otomatik testler uygulayın.

# API Performansını İyileştirme Yöntemleri

- **Önbellekleme (Caching) ile Redis**: Sık erişilen veriler, Redis kullanılarak bellekte saklanır. Bu yöntem, veritabanı yükünü azaltır ve yanıt sürelerini hızlandırır. Örneğin, `GET /users/123` endpoint’inin sonucu, 60 saniyelik bir TTL ile Redis’te önbelleğe alınarak tekrarlanan isteklerde performans artırılır.
  - Alternatif olarak MemCache kullanılabilir. System Design aşamasında genellikle cache yöntemi için hangi teknolojinin kullanılacağına karar verilmiş olur. Bu doğrultuda entegrasyon sağlanmalıdır.

- **Yük Dengeleme (Load Balancing)**: İstekler, birden fazla sunucuya dağıtılır ve yük dengelenir. Bu, yüksek trafik koşullarında API’nin kesintisiz çalışmasını sağlar. NGINX veya AWS Elastic Load Balancer kullanılarak istekler eşit şekilde yönlendirilebilir.

- **Veritabanı Replikaları**: Okuma işlemleri için veritabanı kopyaları (replikalar) oluşturularak birincil veritabanının yükü hafifletilir ve okuma performansı artırılır. Örneğin, PostgreSQL’de okuma replikaları yapılandırılabilir.

- **Mikroservis Mimarisi**: Monolitik yapı yerine, küçük ve bağımsız servislerden oluşan bir mimari tercih edilir. Her servis, kendi görevine odaklanır ve HTTP/2 veya gRPC protokolleriyle hızlı iletişim kurulur. Örneğin, bir e-ticaret API’sinde kullanıcı ve sipariş işlemleri ayrı mikroservisler olarak tasarlanabilir.

- **Reverse Proxy**: İstemci istekleri sunuculara yönlendirilirken önbellekleme ve sıkıştırma işlemleri gerçekleştirilir. NGINX ile statik içerikler önbelleğe alınır ve Gzip sıkıştırmasıyla veri boyutu azaltılır.

- **Veritabanı Optimizasyonu**: Sorgu performansını artırmak için indeksleme ve sorgu önbelleklemesi kullanılır. Gereksiz sorgular ortadan kaldırılır. Örneğin, `GET /products` endpoint’i için tabloya indeks eklenerek sorgu süresi kısaltılır.

- **Veri Sıkıştırma (Compression)**: HTTP yanıtları, Gzip veya Brotli algoritmalarıyla sıkıştırılarak veri boyutu azaltılır. Büyük JSON yanıtlarının daha hızlı iletilmesiyle ağ gecikmesi düşürülür. Bu özellik, .NET Core ve diğer altyapılarda desteklenir.

- **Asenkron İşlemler**: Zaman alan işlemler, kuyruk sistemleriyle asenkron olarak işlenir. Örneğin, `POST /upload` endpoint’inde dosya işlemleri için RabbitMQ kullanılabilir.

- **Statik İçerik için CDN**: Statik kaynaklar (resimler, CSS, JS), Cloudflare veya Akamai gibi CDN’ler üzerinden dağıtılır. Coğrafi olarak yakın sunuculardan teslimat yapılarak istemciye ulaşma süresi kısaltılır.

- **Bağlantı Havuzu (Connection Pooling)**: Veritabanı bağlantıları, bağlantı havuzlarıyla yeniden kullanılır. Her istekte yeni bağlantı açma maliyeti ortadan kaldırılır.

- **N+1 Sorunu Çözümü**: Veritabanı sorgularında N+1 sorunu tespit edilir ve çözülür. Örneğin, `GET /orders` endpoint’inde her sipariş için ayrı kullanıcı sorgusu yerine `JOIN` kullanılarak sorgu sayısı azaltılır. Ayrıca, prosedürler veya view gibi yöntemler de uygulanabilir.

- **Sayfalandırma (Pagination)**: Büyük veri setleri, sayfalar halinde döndürülür. Örneğin, `GET /products?limit=20&offset=40` ile yalnızca 20 ürün listelenerek yanıt süresi ve kaynak tüketimi optimize edilir.

- **Rate Limiting ve Throttling**: Aşırı istekler kısıtlanarak sunucu yükü dengelenir. Örneğin, bir kullanıcıya dakikada 100 istek sınırı getirilerek kaynak tüketimi kontrol altına alınır. Sakın! Rate limit yapılan kullanıcı bilgilerini liste olarak tutmayın. Bu yöntem belleğinizi şişirir. Alternatif olarak redis üzerinde 60 saniyelik TTL verisi tutabilirsiniz.

- **Kod Optimizasyonu**: Verimli algoritmalar ve veri yapıları kullanılarak işlem süreleri kısaltılır. Gereksiz döngüler kaldırılır ve hash tabloları gibi hızlı yapılar tercih edilir.

- **İzleme ve Profil Oluşturma**: API performansı, Prometheus gibi araçlarla izlenir. Yavaş endpoint’ler analiz edilerek darboğazlar tespit edilir ve gerekli optimizasyonlar yapılır.

Tüm bilgileri uykulu hazırladım :D hakkınızı helal edin.