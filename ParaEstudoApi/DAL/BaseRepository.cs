using ParaEstudoApi.DAL.Context;
using System;

namespace ParaEstudoApi.DAL
{
    public abstract class BaseRepository
    {
        protected readonly ParaEstudoApiContext _context;

        public BaseRepository(ParaEstudoApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
