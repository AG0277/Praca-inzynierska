namespace api.Repositories.Abstractions
{
    public interface IBaseRepository<T>: IRepository
    {
        public Task<T?> FindByIdAsync(int id);
        public Task<IEnumerable<T>> FindAllAsync();   
        public Task<T> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<bool> DeleteAsync(int id);
        public Task SaveChangesAsync();
    }
}
