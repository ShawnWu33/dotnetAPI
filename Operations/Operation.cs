using System;

namespace TodoAPi.Operations
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation() : this(Guid.NewGuid().ToString())
        {
        }

        public Operation(string id)
        {
            OperationId = id.Substring(id.Length - 4).ToUpper();
        }

        public string OperationId { get; private set; }
    }
}