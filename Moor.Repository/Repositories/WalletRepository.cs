using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moor.Core.Entities.MoorEntities;
using Moor.Core.Repositories.MoorRepository;
using Moor.Core.Session;
using Moor.Core.Sieve;
using Moor.Core.Utilities.DataFilter;
using Moor.Repository.GenericRepository;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Repositories
{
    public class WalletRepository : GenericRepository<WalletEntity>, IWalletRepository
    {
        private readonly BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> _sieveProcessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WalletRepository(AppDbContext context, BaseApplicationSieveProcessor<DataFilterModel, FilterTerm, SortTerm> sieveProcessor, SessionManager sessionManager, IHttpContextAccessor httpContextAccessor) : base(context, sieveProcessor, sessionManager, httpContextAccessor)
        {
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<WalletEntity> GetAll(DataFilterModel dataFilterModel)
        {
            IQueryable<WalletEntity> data = _sieveProcessor.Apply<WalletEntity>(
                dataFilterModel,
                _context.Set<WalletEntity>().Where(x => x.IsDeleted == false),
                applyPagination: false);

            _httpContextAccessor.HttpContext.Response.Headers.Add("X-Total-Count", data.Count().ToString());
            _httpContextAccessor.HttpContext.Response.Headers.Add("access-control-expose-headers", "X-Total-Count");

            return _sieveProcessor.Apply<WalletEntity>(dataFilterModel, data);
        }

        public override IQueryable<WalletEntity> Where(Expression<Func<WalletEntity, bool>> expression)
        {
            return base.Where(expression).Where(x => x.IsDeleted == false);
        }
    }
}
