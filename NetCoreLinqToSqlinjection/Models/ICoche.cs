namespace NetCoreLinqToSqlinjection.Models
{
    public interface ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }
        void Acelerar();
        void Frenar();
    }
}
