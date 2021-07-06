namespace Catalog.Settings
{
    public class MongoDBSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ConnectionString { get { return $"mongodb://{this.User}:{this.Password}@{this.Host}:{this.Port}"; } }
    }
}
