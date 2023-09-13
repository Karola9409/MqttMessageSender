using System;

namespace MqttMessageSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var message = new MqttMessage("/public-mqtt-broker", "Message");
            using (var publisher = new MqttMessagePublisher())
            {
                var published = publisher.TryPublish(message);
                if (published == false)
                {
                    Console.WriteLine($"Failed to publish message to Topic: {message.Topic}");
                }      
            }
        }
    }
}
