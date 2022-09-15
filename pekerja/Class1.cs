using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pekerja
{
    internal class Class1
    {
        public ConnectionFactory connectionFactory;
        public IConnection connection;
        public IModel channel;
        private bool isReceiving = false;


        public void InitRMQConnection(string host = "rmq2.pptik.id", int port = 5672, string user = "TMDG2022", string pass = "TMDG2022", string vhost = "/TMDG2022")
        {
            connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = host;
            connectionFactory.Port = port;
            connectionFactory.UserName = user;
            connectionFactory.Password = pass;
            connectionFactory.VirtualHost = vhost;
        }
        public void CreateRMQConnection()
        {
            connection = connectionFactory.CreateConnection();
            Console.WriteLine("Koneksi " + (connection.IsOpen ? "Berhasil!" : "Gagal!"));
        }
        public void WaitingMessage(string queue_name)
        {
            using (channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue_name,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Pesan diterima: {0}", message);
                    int pesanku = Int32.Parse(message);
                    hountDown(pesanku);
                };
                channel.BasicConsume(queue: queue_name,
                autoAck: true,
                consumer: consumer);
                Console.WriteLine(" Tekan [enter] untuk memutus koneksi.");
                Console.ReadLine();
                Disconnect();
            }
        }
        public void hountDown(int start)
        {
            for (int i = start; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Done Computing");
        }

        public void Disconnect()
        {
            isReceiving = false;
            channel.Close();
            channel = null;
            Console.WriteLine("Channel ditutup!");
            if (connection.IsOpen)
            {
                connection.Close();
            }
            Console.WriteLine("Koneksi diputus!");
            connection.Dispose();
            connection = null;
        }


    }
}

