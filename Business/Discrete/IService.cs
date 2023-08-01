namespace Business.Discrete
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Get(Func<T, bool> filter);
        Task Add(T Entity);
        Task Delete(int id);
        Task Update(T entity);
    }
}
