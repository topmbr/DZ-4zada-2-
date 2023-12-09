using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp98
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                // Встановлюємо адресу сервера та порт для підключення
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                Console.WriteLine("Підключено до сервера...");

                // Отримуємо NetworkStream для читання та запису даних
                NetworkStream stream = client.GetStream();

                // Введення запиту від користувача
                Console.Write("Введіть 'date' або 'time': ");
                string request = Console.ReadLine();

                // Відправляємо запит серверу
                byte[] requestData = Encoding.ASCII.GetBytes(request);
                await stream.WriteAsync(requestData, 0, requestData.Length);

                // Отримуємо відповідь від серверу
                byte[] responseData = new byte[256];
                int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
                string response = Encoding.ASCII.GetString(responseData, 0, bytesRead);
                Console.WriteLine($"Отримано відповідь: {response}");

                // Закриваємо з'єднання
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

    }
}