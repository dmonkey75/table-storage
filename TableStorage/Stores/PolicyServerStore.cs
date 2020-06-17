using Grundfos.GiC.Shared.Azure.Storage;
using Grundfos.GiC.Shared.Defense;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableStorage.Models;

namespace TableStorage.Stores
{
    public class PolicyServerStore : ITableStorageRepository<PolicyServerMap>
    {
        private readonly TableStorageRepository<PolicyServerMap> _repository;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _tableReference;

        private readonly string _tableName = nameof(PolicyServerMap);

        public PolicyServerStore(string connectionString)
        {
            Ensure.Argument.NotNull(connectionString, nameof(connectionString));

            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();

            _tableReference = _tableClient.GetTableReference(_tableName);
            var result = _tableReference.CreateIfNotExistsAsync().Result;

            _repository = new TableStorageRepository<PolicyServerMap>(new AzureCloudTable(_tableReference));
        }

        public async Task<Result<TableStorageRepositoryStatus>> DeleteAllAsync(IList<PolicyServerMap> models)
        {
            return await _repository.DeleteAllAsync(models);
        }

        public async Task<Result<TableStorageRepositoryStatus>> DeleteAsync(PolicyServerMap model)
        {
            var result = await _repository.DeleteAsync(model);
            return result;
        }

        public async Task<(Result<IEnumerable<PolicyServerMap>, TableStorageRepositoryStatus>, Maybe<TableContinuationToken>)> GetAllAsync(Maybe<TableContinuationToken> token)
        {
            return await _repository.GetAllAsync(token);
        }

        public async Task<(Result<IEnumerable<PolicyServerMap>, TableStorageRepositoryStatus>, Maybe<TableContinuationToken>)> GetAllAsync(TableQuery<PolicyServerMap> query, Maybe<TableContinuationToken> token)
        {
            return await _repository.GetAllAsync(query, token);
        }

        public async Task<Result<PolicyServerMap, TableStorageRepositoryStatus>> GetAsync(ITableStorageQuery query)
        {
            return await _repository.GetAsync(query);
        }

        public async Task<Result<TableStorageRepositoryStatus>> InsertAsync(PolicyServerMap model)
        {
            return await _repository.InsertAsync(model);
        }

        public async Task<Result<TableStorageRepositoryStatus>> InsertOrReplaceAllAsync(IList<PolicyServerMap> models)
        {
            return await _repository.InsertOrReplaceAllAsync(models);
        }

        public async Task<Result<TableStorageRepositoryStatus>> InsertOrReplaceAsync(PolicyServerMap model)
        {
            return await _repository.InsertOrReplaceAsync(model);
        }

        public async Task<Result<TableStorageRepositoryStatus>> ReplaceAsync(PolicyServerMap model)
        {
            return await _repository.ReplaceAsync(model);
        }
    }
}
