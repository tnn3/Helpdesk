namespace Interfaces
{
    public interface IDbInitializer
    {
        void Seed();
        void Migrate();
    }
}
