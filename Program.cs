using Auto.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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

        public static void addNewCar()
        {
            conn.Connection.Open();
            string brand, type, license;
            int date;

            Console.Write("Kérem az autó márkáját: ");
            brand = Console.ReadLine();
            Console.Write("Kérem az autó típusát: ");
            type = Console.ReadLine();
            Console.Write("Kérem az autó motorszámát: ");
            license = Console.ReadLine();
            Console.Write("Kérem az autó gyártási évét: ");
            date = int.Parse(Console.ReadLine());

            string sql = $"INSERT INTO `cars`(`Brand`, `Type`, `License`, `Date`) VALUES ('{brand}','{type}','{license}',{date})";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        public static void updatecar() {

            int date;
            int id;
            conn.Connection.Open();

            Console.WriteLine("Kérem az azonosítóját");
            id= int.Parse(Console.ReadLine());
            Console.WriteLine("Kérem a gyártási évet");
            date = int.Parse(Console.ReadLine());

            string sql = $"UPDATE `cars` SET `Date`={date} WHERE id={id} ";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Clone();
        }

        public static void deletecar()
        {


            int id;
            conn.Connection.Open();

            Console.WriteLine("Kérem az azonosítóját");
            id = int.Parse(Console.ReadLine());


            string sql = $"DELETE FROM `cars` WHERE id={id} ";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Clone();
        }
        static void Main(string[] args)
        {
            feltolt();
            foreach (var car in cars) 
            {
                Console.WriteLine($"Autó gyártója: {car.Brand}, motorszáma: {car.License}");
            }
            //addNewCar();
            //updatecar();
            deletecar();
            Console.ReadLine();
        }
    }
}
