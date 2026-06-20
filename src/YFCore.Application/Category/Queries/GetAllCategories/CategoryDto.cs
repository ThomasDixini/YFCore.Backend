using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YFCore.Application.Category.Queries.GetAllCategories
{
    public sealed record CategoryDto(Guid Id, string Name, string Description);
}
