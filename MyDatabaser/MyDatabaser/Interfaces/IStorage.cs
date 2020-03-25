namespace MyDatabaser.Interfaces
{
    public interface IStorage
    {
        void Set(string key, string value);
        string Get(string key);
    }
}
