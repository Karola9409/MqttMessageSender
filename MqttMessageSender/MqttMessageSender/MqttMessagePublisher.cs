using System;
using System.Configuration;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace MqttMessageSender
{
    internal class MqttMessagePublisher : IDisposable
    {
        private readonly MqttClient _client;

        public MqttMessagePublisher()
        {
            var brokerHostName = ConfigurationManager.AppSettings["MqttBrokerHostName"];

            _client = new MqttClient(brokerHostName);
            var clientID = Guid.NewGuid().ToString();

            _client.Connect(clientID);
        }

        public bool TryPublish(MqttMessage message)
        {
            try
            {
                var messageBytes = Encoding.UTF8.GetBytes(message.Message);
                _client.Publish(message.Topic, messageBytes);
            }
            catch
            { 
                return false;
            }    
            return true;
        }

        public void Dispose()
        {
            if (_client.IsConnected)
            {
                _client.Disconnect();
            }
        }
    }
}
