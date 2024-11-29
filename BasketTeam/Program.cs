using BasketTeam;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Notice.Warning.Types;

namespace basket_team
{
    internal class Program
    {
        public static Connect conn = new Connect();

        public static void HozzaAdas()
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

        public static void Torles()
        {
            Console.Write("Add meg a törölni kívánt játékos azonosítóját (Id): ");
            int id = int.Parse(Console.ReadLine());

            conn.Connection.Open();

            string sql = "DELETE FROM Player WHERE Id = @Id";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();

            conn.Connection.Close();

            Console.WriteLine("A játékos törölve az adatbázisból.");
        }



        public static void KiIras()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM Player";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("\nJátékosok listája:");
            while (dr.Read())
            {
                Console.WriteLine($"Id: {dr.GetInt32(0)}, Név: {dr.GetString(1)}, Magasság: {dr.GetInt32(2)} cm, Súly: {dr.GetInt32(3)} kg, Hozzáadva: {dr.GetDateTime(4)}");
            }

            dr.Close();
            conn.Connection.Close();
        }
        public static void Frissit()
        {
            conn.Connection.Open();

            Console.Write("Add meg a frissítendő játékos ID-ját: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Add meg az új nevet: ");
            string name = Console.ReadLine();

            Console.Write("Add meg az új magasságot (cm): ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Add meg az új súlyt (kg): ");
            int weight = int.Parse(Console.ReadLine());

            string sql = "UPDATE Player SET Name = @Name, Height = @Height, Weight = @Weight WHERE Id = @Id;";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Height", height);
            cmd.Parameters.AddWithValue("@Weight", weight);
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
            conn.Connection.Close();

            Console.WriteLine("A játékos adatai sikeresen frissítve!");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nKosárlabda Csapat Kezelő");
                Console.WriteLine("1. Új játékos hozzáadása");
                Console.WriteLine("2. Játékos törlése");
                Console.WriteLine("3. Játékosok listázása");
                Console.WriteLine("4 - Játékos frissítése");
                Console.WriteLine("5. Kilépés");
                Console.Write("Válassz egy opciót: ");

                string valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                        HozzaAdas();
                        break;
                    case "2":
                        Torles();
                        break;
                    case "3":
                        KiIras();
                        break;
                    case "4":
                        Frissit();
                        break;
                    case "5":
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        break;
                }
            }
        }
    }
}