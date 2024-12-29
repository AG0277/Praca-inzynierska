using Domain.Abstractions;
using Logic.Logic.Abstractions;

namespace Logic.Logic.Base
{
    public class BaseLogic<Entity> : ILogic
    {
        private readonly IBaseRepository<Entity> _repository;

        public BaseLogic(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }

        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _repository.FindAllAsync();
        }

        public async Task<Entity> AddAsync(Entity entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<bool> UpdateAsync(Entity entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
