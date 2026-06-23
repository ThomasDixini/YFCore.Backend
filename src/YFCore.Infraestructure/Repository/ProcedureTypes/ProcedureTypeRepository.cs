using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Domain.ProcedureTypes.Repository;
using YFCore.Infraestructure.Persistance;
using YFCore.Infraestructure.Repository.Shared;

namespace YFCore.Infraestructure.Repository.ProcedureTypes
{
    public class ProcedureTypeRepository : Repository<ProcedureType>, IProcedureTypeRepository
    {
        public ProcedureTypeRepository(AppDbContext context) : base(context) { }
    }
}