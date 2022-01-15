namespace Catelog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectingString 
        { 
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}