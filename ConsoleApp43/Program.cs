using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://nbu.uz/exchange-rates/json/";
                string json = await client.GetStringAsync(apiUrl);

                JArray jsonArray = JArray.Parse(json);

                Console.WriteLine("Valyutani tanlang (kod orqali): ");
                string valyuta = Console.ReadLine();

                Console.WriteLine("Miqdorni kiriting: ");
                double amountInDollars = double.Parse(Console.ReadLine());

                for (int i = 0; i < jsonArray.Count; i++)
                {
                    if (valyuta == (string)jsonArray[i]["code"])
                    {
                        double rate = (double)jsonArray[i]["cb_price"];
                        double result = rate * amountInDollars / 1000;

                        Console.WriteLine($"UZS: {result:N2} so'm");
                        return;
                    }
                }

                Console.WriteLine("Bunday valyuta topilmadi.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xatolik yuz berdi: {ex.Message}");
        }
    }
}
