# Virtual Server Project
<h1> Sunucu İstek Yoğunluğunun Multithread İle Kontrolü</h1>

<p>Projemiz, en başta bir 10.000 kapasiteli bir Ana Sunucu (Main Thread) ve 2 adet 5.000 kapasiteli Alt Sunuculardan (Sub Thread) oluşmaktadır. <br><br>
Projemizde, 10.000 kapasiteli Ana Sunucumuza (Main Thread) dışarıdan 500 milisaniyede bir 1 ile 100 arasında rastgele sayı üretilerek istek gelmektedir. 200 milisaniyede bir de 1 ile 50 arasında rastgele sayı üretilerek sunucuda bekleyen istekler cevaplandırılır.<br><br>
Her bir Alt Sunucuların (Sub Thread) kapasiteleri 5.000’dir. Projenin en başında 2 adet Alt Sunucu (Sub Thread) bulunmaktadır.<br><br>
Alt Sunucular (Sub Thread) ise, Ana Sunucudan (Main Thread) 500 milisaniyede bir 1 ile 50 arasında rastgele sayı üretilerek, Ana Sunucuda (Main Thread) bulunan henüz cevaplandırılamamış istekleri cevaplamak üzere almaktadır. Alt Sunucular da 300 milisaniyede bir, 1 ile 50 arasında rastgele sayı üretilerek, Ana Sunucudan aldığı istekleri cevaplamaktadır.<br><br>
Alt Sunucular (Sub Thread), Kapasitelerinin %70 doluluk oranına ulaştığı zaman, yeni bir adet Alt Sunucu (Sub Thread) oluşturulmaktadır. Yeni oluşturulan Alt Sunucuya %70 doluluk oranına ulaşmış Alt Sunucudaki verilerin yarısı aktarılmaktadır. Böylece Alt Sunucular tam doluluk oranına ulaşamayıp yeni Alt Sunucu oluşturulmasıyla beraber, Ana Sunucudaki (Main Thread) cevaplanmamış veriler daha hızlı bir şekilde cevaplanması sağlanıp sunuculardaki yoğunluk hafifletilip zamandan kazanılmıştır.<br><br>
Eğer herhangi bir alt sunucunun kapasitesi %0 a ulaşır ise mevcut olan alt sunucu sistemden kaldırılır. En az iki alt sunucu sürekli çalışır durumda bekler.
</p>
