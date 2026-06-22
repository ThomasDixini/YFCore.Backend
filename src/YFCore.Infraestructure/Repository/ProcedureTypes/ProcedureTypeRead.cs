using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using YFCore.Application.ProcedureType.Contracts;
using YFCore.Application.ProcedureType.Queries.GetProcedureTypeById;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.ProcedureTypes
{
    public class ProcedureTypeRead : Repository<ProcedureTypeDTO>, IProcedureTypeRead
    {
        public ProcedureTypeRead(AppDbContext context) : base(context) { }
        public new async Task<ProcedureTypeDTO?> GetByIdAsync(Guid id)
        {
            return await _context.ProcedureTypes.Where(x => x.Id == id).Select(x => new ProcedureTypeDTO
            (
                x.Id,
                x.Name,
                x.Description
            )).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProcedureTypeDTO>> GetProcedureTypesAsync()
        {
            return await _context.ProcedureTypes.Select(x => new ProcedureTypeDTO
            (
                x.Id,
                x.Name,
                x.Description
            )).ToArrayAsync();
        }
    }
}