namespace CustomerService.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CustomerService.Models;
    using CustomerService.Repositories;
    using GreenDonut;

    public class CustomerDataLoader : DataLoaderBase<int, Customer>, ICustomerDataLoader
    {
        private readonly ICustomerRepository repository;

        public CustomerDataLoader(IBatchScheduler batchScheduler, ICustomerRepository repository)
            : base(batchScheduler, new DataLoaderOptions()) =>
            this.repository = repository;

        protected override async ValueTask FetchAsync(
            IReadOnlyList<int> keys,
            Memory<Result<Customer>> results,
            CancellationToken cancellationToken)
        {
            var customers = await this.repository.GetCustomersAsync(keys, cancellationToken).ConfigureAwait(false);
            customers.Select(Result<Customer>.Resolve).ToArray().CopyTo(results);
        }
    }
}
