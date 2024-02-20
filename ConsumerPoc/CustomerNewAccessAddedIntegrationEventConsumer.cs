using CoffeStore.Common.MessageModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerPoc
{
    internal class CustomerNewAccessAddedConsumer : IConsumer<CustomerNewAccessAdded>
    {
        private ILogger<CustomerNewAccessAddedConsumer> _logger;

        public CustomerNewAccessAddedConsumer(ILogger<CustomerNewAccessAddedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CustomerNewAccessAdded> context)
        {
            _logger.LogInformation($"Consumed with success: {context.Message}");
            return Task.CompletedTask;
        }
    }
}
