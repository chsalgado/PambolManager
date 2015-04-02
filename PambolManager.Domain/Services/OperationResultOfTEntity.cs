using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PambolManager.Domain.Services
{
    public class OperationResult<TEntity> : OperationResult
    {
        public OperationResult(bool isSuccess) : base(isSuccess)
        {

        }

        public TEntity Entity { get; set; }
    }
}
