using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationPipelineExample.Services
{
    public interface ITransactionService
    {
        int GetCount();

        decimal GetTotal();
    }
}
