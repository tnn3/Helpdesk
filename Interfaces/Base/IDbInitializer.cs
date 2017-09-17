namespace Interfaces.Base
{
    public interface IDbInitializer
    {
        void Seed();
        void Migrate();
    }
}
