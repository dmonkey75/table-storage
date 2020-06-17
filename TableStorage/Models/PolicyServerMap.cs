using Grundfos.GiC.Shared.Defense;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableStorage.ValueObjects;

namespace TableStorage.Models
{
    public class PolicyServerMap: TableEntity
    {  
        //private DateTimeOffset _nextIncrement;
        public PolicyServerMap()
        {
        }

        public Guid Id { get; set; }
        public string AppId { get; set; }
        public int PolicyId { get; set; }

        public PolicyServerMap(Guid id, int policyId, string appId)
        {
            Ensure.Argument.NotNull(id, nameof(id));
            Ensure.Argument.NotNull(appId, nameof(appId));
            Ensure.Argument.NotNull(policyId, nameof(policyId)); 

            PartitionKey = appId;

            var timestamp = DateTimeOffset.Now;

            RowKey = InvertedTicksRowkey.Create(timestamp).Value; 
            //_nextIncrement = timestamp.AddMilliseconds(1);

            Id = id;
            AppId = appId;
            PolicyId = policyId;
        }

        //public PolicyServerMap IncrementRowKey()
        //{
        //    var nextRowKey = InvertedTicksRowkey.Create(_nextIncrement);

        //    return new PolicyServerMap()
        //    {
        //        Id = Id,
        //        AppId = AppId,
        //        PolicyId = PolicyId, 
        //        RowKey = nextRowKey.Value, 
        //        _nextIncrement = _nextIncrement.AddMilliseconds(1)
        //    };
        //}
    }
}
