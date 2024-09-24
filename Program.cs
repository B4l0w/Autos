using Auto.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto
{
    public class Program
    {
       public static Connect conn = new Connect();
        public static List<Car> cars = new List<Car>();

        static void feltolt()
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM `cars`";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                Car car = new Car();
                car.Id = reader.GetInt32(0);
                car.Brand = reader.GetString(1);
                car.Type = reader.GetString(2);
                car.License = reader.GetString(3);
                car.Date = reader.GetInt32(4);

                cars.Add(car);
            }
            reader.Read();
            conn.Connection.Close();
        }
        static void Main(string[] args)
        {
            feltolt();
            foreach (var car in cars) 
            {
                Console.WriteLine($"Autó gyártója: {car.Brand}, motorszáma: {car.License}");
            }
            Console.ReadLine();
        }
    }
}
