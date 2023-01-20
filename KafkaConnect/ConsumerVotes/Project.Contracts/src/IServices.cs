﻿namespace Project.Contracts.src
{
    public interface IServices<T> where T : class
    {
        Task<T> Create(T entity);
        Task<T> GetOne(int id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
    }
}
