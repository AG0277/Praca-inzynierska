using Logic.Logic.Abstractions;

namespace api.Logic.Abstractions.Base
{
    public interface IBaseLogic<Entity>:ILogic
    {
        Task<Entity?> GetByIdAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> RemoveAsync(int id);
    }
}
