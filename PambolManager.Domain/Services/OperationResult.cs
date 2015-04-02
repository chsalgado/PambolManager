using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PambolManager.Domain.Services
{
    public class OperationResult
    {
        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; private set; }
    }
}
