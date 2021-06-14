using Newtonsoft.Json;
using System;

namespace CustomerService.Messaging.RabbitMQ.Outbox
{
    public class Message
    {
        public virtual long Id { get; protected set; }
        public virtual Guid PublicId { get; protected set; }
        public virtual string Type { get; protected set; }
        public virtual string Payload { get; protected set; }
        public DateTime? ProccesDateTime { get; set; }
        public bool IsProcced { get; set; }

        protected Message()
        {
        }
        
        public Message(object message)
        {
            Type = message.GetType().FullName + ", " + message.GetType().Assembly.GetName().Name;
            Payload = JsonConvert.SerializeObject(message);
        }

        public virtual object RecreateMessage() => JsonConvert.DeserializeObject(Payload, System.Type.GetType(Type));
    }
}