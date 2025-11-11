using Confluent.Kafka;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orleans.Streams.Kafka.Config
{
	internal static class KafkaStreamOptionsExtensions
	{
		public static ProducerConfig ToProducerProperties(this KafkaStreamOptions options)
		{
			var config = CreateCommonProperties<ProducerConfig>(options);
			config.MessageTimeoutMs = (int)options.ProducerTimeout.TotalMilliseconds;

			return config;
		}

		public static ConsumerConfig ToConsumerProperties(this KafkaStreamOptions options)
		{
			var config = CreateCommonProperties<ConsumerConfig>(options);

			config.GroupId = options.ConsumerGroupId;
			config.EnableAutoCommit = false;

			return config;
		}

		public static AdminClientConfig ToAdminProperties(this KafkaStreamOptions options)
			=> CreateCommonProperties<AdminClientConfig>(options);

		private static readonly HashSet<string> _kafkaStreamOptionsTypeBlackListOptions =
		[
			"Topics",
			"BrokerList",
			"ConsumerGroupId",
			"PollTimeout",
			"AdminRequestTimeout",
			"ConsumeMode",
			"ProducerTimeout",
			"PollBufferTimeout",
			"MessageTrackingEnabled",
			"ImportRequestContext",
		];

		private static TClientConfig CreateCommonProperties<TClientConfig>(KafkaStreamOptions options)
			where TClientConfig : ClientConfig, new()
		{
			var config = new TClientConfig
			{
				BootstrapServers = string.Join(",", options.BrokerList),
				SaslUsername = options.SaslUserName,
			};

			var clientConfigType = typeof(Confluent.Kafka.ClientConfig);
			var kafkaStreamOptionsType = typeof(Orleans.Streams.Kafka.Config.KafkaStreamOptions);
			var props = kafkaStreamOptionsType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var prop in props
				         .Where(x => x.CanRead && !_kafkaStreamOptionsTypeBlackListOptions.Contains(x.Name)))
			{
				var propWrite = clientConfigType.GetProperty(prop.Name);
				if ((propWrite is null) || !propWrite.CanWrite) continue;

				var value = prop.GetValue(options);
				propWrite.SetValue(config, value);
			}

			return config;
		}
	}

	public static class KafkaStreamOptionsPublicExtensions
	{
		public static KafkaStreamOptions WithSaslOptions(
			this KafkaStreamOptions options,
			Credentials credentials,
			SaslMechanism saslMechanism = SaslMechanism.Plain
		)
		{
			options.SaslMechanism = saslMechanism;
			options.SecurityProtocol = SecurityProtocol.SaslSsl;
			options.SaslUserName = credentials.UserName;
			options.SaslPassword = credentials.Password;
			options.SslCaLocation = credentials.SslCaLocation;

			return options;
		}
	}
}