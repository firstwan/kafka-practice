using Confluent.Kafka;

Console.WriteLine("Consumer Starting");

string topic = "order";
string groupId = "kitchen-group-1";
string bootstrapServer = "";
string apiKey = "";
string apiSecret = "";

ConsumerConfig config = new()
{
    GroupId = groupId,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    BootstrapServers = bootstrapServer,
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = apiKey,
    SaslPassword = apiSecret,
};

// creates a new consumer instance
using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe(topic);
    while (true)
    {
        // consumes messages from the subscribed topic and prints them to the console
        var cr = consumer.Consume();

        try
        {
            Console.WriteLine($"Consumed event from topic \"{topic}\": value = {cr.Message.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
