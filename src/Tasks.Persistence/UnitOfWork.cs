﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Abstractions.Data;

namespace Tasks.Persistence
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _dbContext;

        public UnitOfWork(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
