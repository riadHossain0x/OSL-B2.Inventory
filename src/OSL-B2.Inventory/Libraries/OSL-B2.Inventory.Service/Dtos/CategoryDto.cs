using System;
using System.Collections.Generic;

namespace OSL_B2.Inventory.Service.Dtos
{
    public class CategoryDto : BaseEntityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
