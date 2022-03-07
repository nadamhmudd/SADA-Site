﻿using SADA.Core.Models;

namespace SADA.Core.Interfaces.Repositories;
public interface IProductRepository : IBaseRepository<Product>
{
    public void Update(Product entity);
}
