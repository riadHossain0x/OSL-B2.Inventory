﻿using OSL_B2.Inventory.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholeSale.Repository.DbContexts.ModelConventions
{
    internal static class CategoryModel
    {
        public static void Builder(DbModelBuilder builder)
        {
            builder.Entity<Category>().HasKey(x => x.Id);

            builder.Entity<Category>().Property(x => x.Name)
                .HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Entity<Category>().Property(x => x.IsActive).IsRequired();

            builder.Entity<Category>().Property(x => x.ModifiedBy).IsOptional();

            builder.Entity<Category>().Property(x => x.ModifiedDate).IsOptional();

            builder.Entity<Category>().Property(x => x.CreatedBy).IsRequired();

            builder.Entity<Category>().Property(x => x.CreatedDate).IsRequired();
        }
    }
}
