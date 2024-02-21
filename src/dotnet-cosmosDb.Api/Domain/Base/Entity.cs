namespace dotnet_cosmosDb.Api.Domain.Base
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
    }
}
