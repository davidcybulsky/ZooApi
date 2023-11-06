﻿using Zoo.Entities;

namespace ZooApi.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Guid Create(T entity);
        T Read(Guid id);
        void Update(Guid id, T entity);
        void Delete(Guid id);
    }
}
