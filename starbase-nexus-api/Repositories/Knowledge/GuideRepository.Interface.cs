using starbase_nexus_api.Entities.Knowledge;

namespace starbase_nexus_api.Repositories.Knowledge
{
    public interface IGuideRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Guide
    {
    }
}
