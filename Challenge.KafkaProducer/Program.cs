using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Text;

namespace Challenge.KafkaProducer;

public class Program
{
    static void Main(string[] args)
    {
        // The Kafka endpoint address
        string kafkaEndpoint = "127.0.0.1:9092";

        // The Kafka topic we'll be using
        string kafkaTopic = "testtopic";
               
        // Create the producer configuration
        var producerConfig = new Dictionary<string, object> 
        { 
            { "bootstrap.servers", kafkaEndpoint } 
        };

        // Create the producer
        using var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8));

        // -- Generate event producer

        while (true)
        {
            var i = DateTime.Now.Ticks;
            var message = $"Event {i}";
            var result = producer.ProduceAsync(kafkaTopic, null, message).GetAwaiter().GetResult();
            Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}");
            Thread.Sleep(1000);
        };
    }
}