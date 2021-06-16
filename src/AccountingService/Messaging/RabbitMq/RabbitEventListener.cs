﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace AccountingService.Messaging.RabbitMq
{
    public class RabbitEventListener
    {
        private readonly IBusClient busClient;
        private readonly IServiceProvider serviceProvider;

        public RabbitEventListener(
            IBusClient busClient,
            IServiceProvider serviceProvider)
        {
            this.busClient = busClient;
            this.serviceProvider = serviceProvider;
        }

        public void ListenTo(List<Type> eventsToSubscribe)
        {
            foreach (var evtType in eventsToSubscribe)
            {
                //add check if is INotification
                this.GetType()
                    .GetMethod("Subscribe", System.Reflection.BindingFlags.NonPublic| System.Reflection.BindingFlags.Instance)
                    .MakeGenericMethod(evtType)
                    .Invoke(this, new object[] { });
            }
        }

        private void Subscribe<T>() where T : INotification
        {
            //TODO: move exchange name and queue prefix to cfg
            this.busClient.SubscribeAsync<T>(
                async (msg,msgcontext) =>
                {
                    //add logging
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var internalBus = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await internalBus.Publish(msg);
                    }

                },
                cfg =>
                    cfg.WithExchange(excfg =>
                    {
                        excfg.WithName("simple-outbox-netcore");
                        excfg.WithType(RawRabbit.Configuration.Exchange.ExchangeType.Topic);
                        excfg.WithArgument("key", typeof(T).Name.ToLower());
                    })
                    .WithQueue(qcfg =>
                    {
                        qcfg.WithName("accounting-service-" + typeof(T).Name);
                    })
                //cfg => cfg.UseSubscribeConfiguration( 
                //    c => c
                //    .OnDeclaredExchange(e => e
                //        .WithName("lab-dotnet-micro")
                //        .WithType(RawRabbit.Configuration.Exchange.ExchangeType.Topic)
                //        .WithArgument("key", typeof(T).Name.ToLower()))
                //    .FromDeclaredQueue(q => q.WithName("lab-dashboard-service-" + typeof(T).Name)))
                ) ;
        }
    }
}
