// See https://aka.ms/new-console-template for more information
using ConsumerPoc;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x => {
    x.AddConsumer<CustomerNewAccessAddedConsumer>();
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host("Endpoint=sb://coffestore-messaging.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=0ZbHX7+UKEuXf656C4Gh7zFTHIVgPRWcr+ASbKBrZNU=");

        cfg.ReceiveEndpoint("coffestore-messaging/customernewaccessaddedintegrationevent", y =>
        {
            y.ConfigureConsumer<CustomerNewAccessAddedConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();