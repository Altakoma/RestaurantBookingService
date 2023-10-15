﻿using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IFoodTypeRepository : IRepository<FoodType, ReadFoodTypeDTO>
    {
    }
}
