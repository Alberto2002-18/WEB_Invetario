namespace API.Utilidad
{
    public class Reponse<T>
    {
        public bool status { get; set; }
        public T value { get; set; }
        public string msg { get; set; }
    }
}
