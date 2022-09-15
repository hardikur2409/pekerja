using pekerja;
Class1 rmq = new Class1();
Console.WriteLine("Tekan tombol apapun untuk inisialisasi RMQ parameters.");
Console.ReadKey();
rmq.InitRMQConnection(); // inisialisasi parameter (secara default) untuk koneksi ke server RMQ
Console.WriteLine("Tekan tombol apapun untuk membuka koneksi ke RMQ.");
Console.ReadKey();
rmq.CreateRMQConnection(); // memulai koneksi dengan RMQ
Console.Write("Masukkan nama queue channel untuk menerima pesan melalui RMQ.\n>> ");
string queue_name = Console.ReadLine();
Console.WriteLine("Menunggu pesan masuk...");
rmq.WaitingMessage(queue_name);