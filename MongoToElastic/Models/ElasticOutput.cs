namespace MongoToElastic.Models
{
    public class ElasticOutput<T>
    {
        public List<T> ModelList { get; set; }

        public bool ConnectionSuccessful { get; set; }

        public string ErrorMsg { get; set; }
    }
}
