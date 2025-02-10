using System.Data;
using Microsoft.Data.SqlClient;
using MvcComicsTomas.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcComicsTomas.Repositories
{
    public class RepositoryComics
    {
        DataTable tablaComics;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public RepositoryComics()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=COMICS;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from COMICS";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);
            this.tablaComics = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            ad.Fill(this.tablaComics);
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable() select datos;

            List<Comic> comics = new List<Comic>();

            foreach (var row in consulta)
            {
                Comic comic = new Comic();
                comic.IdComic = row.Field<int>("IDCOMIC");
                comic.Nombre = row.Field<string>("NOMBRE");
                comic.Imagen = row.Field<string>("IMAGEN");
                comic.Descripcion = row.Field<string>("DESCRIPCION");
                comics.Add(comic);
            }
            return comics;
        }

        public void CreateComic(Comic comic)
        {
            int newId = this.tablaComics.AsEnumerable()
                          .Max(row => row.Field<int>("IDCOMIC")) + 1;

            string sql = "INSERT INTO COMICS (IDCOMIC, NOMBRE, IMAGEN, DESCRIPCION) VALUES (@IdComic, @Nombre, @Imagen, @Descripcion)";

            this.com.CommandText = sql;
            this.com.Parameters.Clear();
            this.com.Parameters.AddWithValue("@IdComic", newId);
            this.com.Parameters.AddWithValue("@Nombre", comic.Nombre);
            this.com.Parameters.AddWithValue("@Imagen", comic.Imagen);
            this.com.Parameters.AddWithValue("@Descripcion", comic.Descripcion);

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
        }

        public List<string> GetNombreComics()
        {
            var consulta = from datos in this.tablaComics.AsEnumerable() select datos;

            List<string> nombreComics = new List<string>();

            foreach (var row in consulta)
            {
                nombreComics.Add(row.Field<string>("NOMBRE"));
            }
            return nombreComics;
        }
        public async Task<Comic> GetComicNombreAsync(string nombre)
        {
            string sql = "select * from COMICS where NOMBRE=@nombre";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Comic comic = new Comic();
            await this.reader.ReadAsync();
            comic.IdComic = int.Parse(this.reader["IDCOMIC"].ToString());
            comic.Nombre = this.reader["NOMBRE"].ToString();
            comic.Imagen = this.reader["IMAGEN"].ToString();
            comic.Descripcion = this.reader["DESCRIPCION"].ToString();
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return comic;
        }
    }
}
