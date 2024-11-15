using BasketTeam;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basket_team
{
    internal class Program
    {
        public static Connect conn = new Connect();


        public static void uj()
        {
            Console.Write("Add meg a játékos nevét: ");
            string name = Console.ReadLine();

            Console.Write("Add meg a játékos magasságát (cm): ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Add meg a játékos súlyát (kg): ");
            int weight = int.Parse(Console.ReadLine());

            conn.Connection.Open();

            string sql = "INSERT INTO Player (Name, Height, Weight, CreatedTime) VALUES (@Name, @Height, @Weight, NOW())";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Height", height);
            cmd.Parameters.AddWithValue("@Weight", weight);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            Console.WriteLine("Az új játékos sikeresen hozzáadva az adatbázishoz!");
        }
        public static void GetAllData()
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM player";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("Játékosok listája:");

            while (dr.Read())
            {
                var player = new
                {
                    Id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Height = dr.GetInt32(2),
                    Weight = dr.GetInt32(3),
                    CreatedTime = dr.GetDateTime(4),
                };

                Console.WriteLine($"Játékos adatok: {player.Name}, {player.Height}cm, {player.Weight}kg, {player.CreatedTime}");
            }

            dr.Close();
            conn.Connection.Close();
        }

        static void Main(string[] args)
        {
            GetAllData();
            uj();
        }
    }
}