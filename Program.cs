using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace hayvanatBahcesi_sql
{
    internal class Program
    {
        private static string connectionString = "Server=ICLALA\\SQLEXPRESS;Initial Catalog=Zoo;Integrated Security=True"; //Veri tabanina baglanti stringi
        private static string query = ""; //sql sorgu stringi
        private static SqlConnection sqlConnection; //Sql baglanti tanimlaması
        private static SqlCommand sqlCommand; //sqlin komut nesnesi (sqlde yapilacaklari kontrol ettigimiz tanımlama)
        public static string mainCim = " cimcim";

        static void Main(string[] args)
        {
            //Add();
            //Delete();
            //Update();
            List();
            Console.ReadLine();

        }

        public static void Add()
        {   // soldaki parantezin icindekiler bizim kolon isimleri ve sağ parantezdekiler kolonlara atanack değerler
            query = "INSERT INTO ZooPark (Name, Age, Habitat, Race, Extinct) VALUES (@Name, @Age, @Habitat, @Race, @Extinct)"; 
            Console.WriteLine("Hayvanin adi: ");
            string name = Console.ReadLine();
            Console.WriteLine("HAyvanin yasi: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Yasam Alani: ");
            string habitat = Console.ReadLine();
            Console.WriteLine("Tur: ");
            string race = Console.ReadLine();
            Console.WriteLine("Nesli Tukenmekte Olan Hayvan mi?: ");
            bool extinct = Convert.ToBoolean(Console.ReadLine());
            using (sqlConnection = new SqlConnection(connectionString)) //sql bağlantisini yapmak için sql connection nesnesine bağlanti stringimizi göndeririz
            {
                sqlConnection.Open(); //sql bağlantısını açıyo ki böylece biz işleö yapabilelim
                using (sqlCommand = new SqlCommand(query, sqlConnection)) //sql commend nesnesine yapacağımız sorguyu ve bağlantiyi gönderiyor
                {
                    sqlCommand.Parameters.AddWithValue("@Name", name);
                    sqlCommand.Parameters.AddWithValue("@Age", age);
                    sqlCommand.Parameters.AddWithValue("@Habitat", habitat);
                    sqlCommand.Parameters.AddWithValue("@Race", race);
                    sqlCommand.Parameters.AddWithValue("@Extinct", extinct); //Kolonlere atanacak değerlere kullanıcıdan alınan değerleri parametre olarak gönderiyoruz
                    sqlCommand.ExecuteNonQuery();//bu işlemden kac satirin etkilendiğini gösterir

                }

                sqlConnection.Close();//sql bağlantisini kapatır


            }

        }

        public static void Delete()
        {
            Console.WriteLine("Silmek istediginiz blogun ID'sini giriniz:");
            int Id = Convert.ToInt32(Console.ReadLine());
            query = "DELETE FROM ZooPark WHERE ID = @ID";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (sqlCommand = new SqlCommand(query, sqlConnection)) //komut
                {
                    sqlCommand.Parameters.AddWithValue("@ID", Id);
                    int rowsAffected = sqlCommand.ExecuteNonQuery(); //bu sorgudan kac verinin etkilendigini bize donduruyor

                    if (rowsAffected > 0) Console.WriteLine("Id'ye ait blok icerisindeki veriler silindi.");

                    else Console.WriteLine("Bulunamadi");

                }

                sqlConnection.Close();

            }

        }

        public static void Update()
        {
            Console.WriteLine("Guncellemek istediginiz blogun ID'sini giriniz:");
            int Id = Convert.ToInt32(Console.ReadLine());
            query = "UPDATE ZooPark SET Age = @Age WHERE ID = @ID";

            Console.WriteLine("Yeni yasi giriniz");
            int newAge = Convert.ToInt32(Console.ReadLine());
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@ID", Id);
                    sqlCommand.Parameters.AddWithValue("@Age", newAge);
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0) Console.WriteLine("Basariyla guncellendi");

                    else Console.WriteLine("Hata");
                }
                sqlConnection.Close();
            }

        }

        public static void List()
        {
            query = "SELECT * FROM ZooPark";
            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            string Name = reader.GetString(1);
                            int Age = Convert.ToInt32(reader["Age"]);
                            string Habitat = reader.GetString(3);
                            string Race = reader["Race"].ToString();
                            bool Extinct = Convert.ToBoolean(reader["Extinct"]);
                            Console.WriteLine($"Id: {id}\n Name: {Name}\n Age: {Age}\n Habitat: {Habitat}\n Race: {Race}\n Extinct: {Extinct}");
                        }
                        Console.ReadKey();
                    }

                }
            }
        }
    }
}