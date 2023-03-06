﻿using Microsoft.Extensions.Options;
using Moor.Core.Utilities.DataFilter;
using Sieve.Models;
using Sieve.Services;

namespace Moor.Core.Sieve
{
    public class BaseApplicationSieveProcessor<TFilterModel, TFilterTerm, TSortTerm> : SieveProcessor<DataFilterModel, FilterTerm, SortTerm>
    {
        public BaseApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }
    }
}
