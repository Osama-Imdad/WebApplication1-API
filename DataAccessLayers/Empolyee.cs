namespace DataAccessLayers
{
    public class Empolyee
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string contactNum { get; set; }
    }
    public class Response<T> { 
    public string status { get; set; }
    public string message { get; set; }
    public List<Empolyee> data { get; set; }
    
    }
}