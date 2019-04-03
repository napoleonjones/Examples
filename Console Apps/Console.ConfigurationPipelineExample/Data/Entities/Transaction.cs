using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationPipelineExample.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public string AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
