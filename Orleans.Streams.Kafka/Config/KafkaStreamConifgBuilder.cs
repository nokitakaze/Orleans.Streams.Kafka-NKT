using Microsoft.Extensions.DependencyInjection;
using Orleans.Hosting;
using Orleans.Streams.Utils.MessageTracking;
using Orleans.Streams.Utils.Serialization;
using System;

namespace Orleans.Streams.Kafka.Config
{
	public class KafkaStreamSiloBuilder
	{
		private readonly ISiloBuilder _hostBuilder;
		private readonly string _providerName;
		private Action<KafkaStreamOptions> _configure;

		public KafkaStreamSiloBuilder(ISiloBuilder hostBuilder, string providerName)
		{
			_hostBuilder = hostBuilder;
			_providerName = providerName;
		}

		public KafkaStreamSiloBuilder WithOptions(Action<KafkaStreamOptions> configure)
		{
			_configure = configure;
			return this;
		}

		public KafkaStreamSiloBuilder AddExternalDeserializer<TDeserializer>()
			where TDeserializer : class, IExternalStreamDeserializer
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<IExternalStreamDeserializer, TDeserializer>(_providerName)
			);

			return this;
		}

		/// <summary>
		/// Configures the Kafka stream client builder to use Avro serialization with a specified schema registry URL.
		/// </summary>
		/// <param name="schemaRegistryUrl">The URL of the schema registry used for Avro serialization.</param>
		/// <returns>Returns the updated <see cref="KafkaStreamClientBuilder"/> for further configuration.</returns>
		/// <url>https://avro.apache.org/</url>
		public KafkaStreamSiloBuilder AddAvro(string schemaRegistryUrl)
		{
			_hostBuilder.AddAvro(_providerName, schemaRegistryUrl);
			return this;
		}

		public KafkaStreamSiloBuilder AddJson()
		{
			_hostBuilder.AddJson(_providerName);
			return this;
		}

		public KafkaStreamSiloBuilder AddMessageTracking<TTraceWriter>()
			where TTraceWriter : class, ITraceWriter
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<ITraceWriter, TTraceWriter>(_providerName)
			);

			return this;
		}

		public KafkaStreamSiloBuilder AddMessageTracking<TTraceWriter>(Func<IServiceProvider, string, TTraceWriter> configure)
			where TTraceWriter : class, ITraceWriter
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<ITraceWriter>(
					_providerName,
					(provider, name) => configure?.Invoke(provider, name.ToString()))
				);


			return this;
		}

		public KafkaStreamSiloBuilder AddLoggingTracker()
		{
			_hostBuilder.UseLoggingTracker(_providerName);
			return this;
		}

		/// <summary>
		/// Add this configuration to IClientBuilder
		/// </summary>
		/// <returns></returns>
		public ISiloBuilder Build()
		{
			_hostBuilder.AddKafkaStreamProvider(
				_providerName,
				options => _configure?.Invoke(options)
			);

			return _hostBuilder;
		}
	}

	public class KafkaStreamSiloHostBuilder
	{
		private readonly ISiloBuilder _hostBuilder;
		private readonly string _providerName;
		private Action<KafkaStreamOptions> _configure;

		public KafkaStreamSiloHostBuilder(ISiloBuilder hostBuilder, string providerName)
		{
			_hostBuilder = hostBuilder;
			_providerName = providerName;
		}

		public KafkaStreamSiloHostBuilder WithOptions(Action<KafkaStreamOptions> configure)
		{
			_configure = configure;
			return this;
		}

		public KafkaStreamSiloHostBuilder AddExternalDeserializer<TDeserializer>()
			where TDeserializer : class, IExternalStreamDeserializer
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<IExternalStreamDeserializer, TDeserializer>(_providerName)
			);

			return this;
		}

		/// <summary>
		/// Configures the Kafka stream client builder to use Avro serialization with a specified schema registry URL.
		/// </summary>
		/// <param name="schemaRegistryUrl">The URL of the schema registry used for Avro serialization.</param>
		/// <returns>Returns the updated <see cref="KafkaStreamClientBuilder"/> for further configuration.</returns>
		/// <url>https://avro.apache.org/</url>
		public KafkaStreamSiloHostBuilder AddAvro(string schemaRegistryUrl)
		{
			_hostBuilder.AddAvro(_providerName, schemaRegistryUrl);
			return this;
		}

		public KafkaStreamSiloHostBuilder AddJson()
		{
			_hostBuilder.AddJson(_providerName);
			return this;
		}

		public KafkaStreamSiloHostBuilder AddMessageTracking<TTraceWriter>()
			where TTraceWriter : class, ITraceWriter
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<ITraceWriter, TTraceWriter>(_providerName)
			);

			return this;
		}

		public KafkaStreamSiloHostBuilder AddMessageTracking<TTraceWriter>(Func<IServiceProvider, string, TTraceWriter> configure)
			where TTraceWriter : class, ITraceWriter
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<ITraceWriter>(
					_providerName,
					(provider, name) => configure?.Invoke(provider, name.ToString()))
			);


			return this;
		}

		public KafkaStreamSiloHostBuilder AddLoggingTracker()
		{
			_hostBuilder.UseLoggingTracker(_providerName);
			return this;
		}

		/// <summary>
		/// Add this configuration to IClientBuilder
		/// </summary>
		/// <returns></returns>
		public ISiloBuilder Build()
		{
			_hostBuilder.AddKafkaStreamProvider(
				_providerName,
				options => _configure?.Invoke(options)
			);

			return _hostBuilder;
		}
	}

	public class KafkaStreamClientBuilder
	{
		private readonly IClientBuilder _hostBuilder;
		private readonly string _providerName;
		private Action<KafkaStreamOptions> _configure;

		public KafkaStreamClientBuilder(IClientBuilder hostBuilder, string providerName)
		{
			_hostBuilder = hostBuilder;
			_providerName = providerName;
		}

		public KafkaStreamClientBuilder WithOptions(Action<KafkaStreamOptions> configure)
		{
			_configure = configure;
			return this;
		}

		public KafkaStreamClientBuilder AddExternalDeserializer<TDeserializer>()
			where TDeserializer : class, IExternalStreamDeserializer
		{
			_hostBuilder.ConfigureServices(services
				=> services.AddKeyedSingleton<IExternalStreamDeserializer, TDeserializer>(_providerName)
			);

			return this;
		}

		/// <summary>
		/// Configures the Kafka stream client builder to use Avro serialization with a specified schema registry URL.
		/// </summary>
		/// <param name="schemaRegistryUrl">The URL of the schema registry used for Avro serialization.</param>
		/// <returns>Returns the updated <see cref="KafkaStreamClientBuilder"/> for further configuration.</returns>
		/// <url>https://avro.apache.org/</url>
		public KafkaStreamClientBuilder AddAvro(string schemaRegistryUrl)
		{
			_hostBuilder.AddAvro(_providerName, schemaRegistryUrl);
			return this;
		}

		public KafkaStreamClientBuilder AddJson()
		{
			_hostBuilder.AddJson(_providerName);
			return this;
		}

		/// <summary>
		/// Add this configuration to IClientBuilder
		/// </summary>
		/// <returns></returns>
		public IClientBuilder Build()
		{
			_hostBuilder.AddKafkaStreamProvider(
				_providerName,
				options => _configure?.Invoke(options)
			);

			return _hostBuilder;
		}
	}
}
