using Confluent.Kafka;
using System;
using System.Collections.Generic;

namespace Orleans.Streams.Kafka.Config
{
	/// <summary>
	/// Represents configuration options for Kafka streams in Orleans.
	/// </summary>
	/// <url>https://docs.confluent.io/platform/current/installation/configuration/index.html</url>
	public class KafkaStreamOptions
	{
		public IList<TopicConfig> Topics { get; set; } = new List<TopicConfig>();
		public IList<string> BrokerList { get; set; }
		public string ConsumerGroupId { get; set; } = "orleans-kafka";
		public TimeSpan PollTimeout { get; set; } = TimeSpan.FromMilliseconds(100);
		public TimeSpan AdminRequestTimeout { get; set; } = TimeSpan.FromSeconds(5);
		public ConsumeMode ConsumeMode { get; set; } = ConsumeMode.LastCommittedMessage;
		public TimeSpan ProducerTimeout { get; set; } = TimeSpan.FromSeconds(5);
		public TimeSpan PollBufferTimeout { get; set; } = TimeSpan.FromMilliseconds(500);
		public bool MessageTrackingEnabled { get; set; }
		public bool ImportRequestContext { get; set; } = false;

		#region TClientConfig

        // ReSharper disable UnusedMember.Global

		/// <summary>
        ///     SASL mechanism to use for authentication. Supported: GSSAPI, PLAIN, SCRAM-SHA-256, SCRAM-SHA-512. **NOTE**: Despite the name, you may not configure more than one mechanism.
        /// </summary>
        public SaslMechanism? SaslMechanism { get; set; }

        /// <summary>
        ///     This field indicates the number of acknowledgements the leader broker must receive from ISR brokers
        ///     before responding to the request: Zero=Broker does not send any response/ack to client, One=The
        ///     leader will write the record to its local log but will respond without awaiting full acknowledgement
        ///     from all followers. All=Broker will block until message is committed by all in sync replicas (ISRs).
        ///     If there are less than min.insync.replicas (broker configuration) in the ISR set the produce request
        ///     will fail.
        /// </summary>
        public Acks? Acks { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client identifier.
        ///
        ///     default: rdkafka
        ///     importance: low
        /// ]]>
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Maximum Kafka protocol request message size. Due to differing framing overhead between protocol versions the producer is unable to reliably enforce a strict max message limit at produce time and may exceed the maximum size by one message in protocol ProduceRequests, the broker will enforce the the topic's `max.message.bytes` limit (see Apache Kafka documentation).
        ///
        ///     default: 1000000
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? MessageMaxBytes { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Maximum size for message to be copied to buffer. Messages larger than this will be passed by reference (zero-copy) at the expense of larger iovecs.
        ///
        ///     default: 65535
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? MessageCopyMaxBytes { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Maximum Kafka protocol response message size. This serves as a safety precaution to avoid memory exhaustion in case of protocol hickups. This value must be at least `fetch.max.bytes`  + 512 to allow for protocol overhead; the value is adjusted automatically unless the configuration property is explicitly set.
        ///
        ///     default: 100000000
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? ReceiveMessageMaxBytes { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Maximum number of in-flight requests per broker connection. This is a generic property applied to all broker communication, however it is primarily relevant to produce requests. In particular, note that other mechanisms limit the number of outstanding consumer fetch request per broker to one.
        ///
        ///     default: 1000000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? MaxInFlight { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Controls how the client recovers when none of the brokers known to it is available. If set to `none`, the client doesn't re-bootstrap. If set to `rebootstrap`, the client repeats the bootstrap process using `bootstrap.servers` and brokers added through `rd_kafka_brokers_add()`. Rebootstrapping is useful when a client communicates with brokers so infrequently that the set of brokers may change entirely before the client refreshes metadata. Metadata recovery is triggered when all last-known brokers appear unavailable simultaneously or the client cannot refresh metadata within `metadata.recovery.rebootstrap.trigger.ms` or it's requested in a metadata response.
        ///
        ///     default: rebootstrap
        ///     importance: low
        /// ]]>
        /// </summary>
        public MetadataRecoveryStrategy? MetadataRecoveryStrategy { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     If a client configured to rebootstrap using `metadata.recovery.strategy=rebootstrap` is unable to obtain metadata from any of the brokers for this interval, client repeats the bootstrap process using `bootstrap.servers` configuration and brokers added through `rd_kafka_brokers_add()`.
        ///
        ///     default: 300000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? MetadataRecoveryRebootstrapTriggerMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Period of time in milliseconds at which topic and broker metadata is refreshed in order to proactively discover any new brokers, topics, partitions or partition leader changes. Use -1 to disable the intervalled refresh (not recommended). If there are no locally referenced topics (no topic objects created, no messages produced, no subscription or no assignment) then only the broker list will be refreshed every interval but no more often than every 10s.
        ///
        ///     default: 300000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? TopicMetadataRefreshIntervalMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Metadata cache max age. Defaults to topic.metadata.refresh.interval.ms * 3
        ///
        ///     default: 900000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? MetadataMaxAgeMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     When a topic loses its leader a new metadata request will be enqueued immediately and then with this initial interval, exponentially increasing upto `retry.backoff.max.ms`, until the topic metadata has been refreshed. If not set explicitly, it will be defaulted to `retry.backoff.ms`. This is used to recover quickly from transitioning leader brokers.
        ///
        ///     default: 100
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? TopicMetadataRefreshFastIntervalMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Sparse metadata requests (consumes less network bandwidth)
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? TopicMetadataRefreshSparse { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Apache Kafka topic creation is asynchronous and it takes some time for a new topic to propagate throughout the cluster to all brokers. If a client requests topic metadata after manual topic creation but before the topic has been fully propagated to the broker the client is requesting metadata from, the topic will seem to be non-existent and the client will mark the topic as such, failing queued produced messages with `ERR__UNKNOWN_TOPIC`. This setting delays marking a topic as non-existent until the configured propagation max time has passed. The maximum propagation time is calculated from the time the topic is first referenced in the client, e.g., on produce().
        ///
        ///     default: 30000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? TopicMetadataPropagationMaxMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Topic blacklist, a comma-separated list of regular expressions for matching topic names that should be ignored in broker metadata information as if the topics did not exist.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string TopicBlacklist { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     A comma-separated list of debug contexts to enable. Detailed Producer debugging: broker,topic,msg. Consumer: consumer,cgrp,topic,fetch
        ///
        ///     default: ''
        ///     importance: medium
        /// ]]>
        /// </summary>
        public string Debug { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Default timeout for network requests. Producer: ProduceRequests will use the lesser value of `socket.timeout.ms` and remaining `message.timeout.ms` for the first message in the batch. Consumer: FetchRequests will use `fetch.wait.max.ms` + `socket.timeout.ms`. Admin: Admin requests will use `socket.timeout.ms` or explicitly set `rd_kafka_AdminOptions_set_operation_timeout()` value.
        ///
        ///     default: 60000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SocketTimeoutMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Broker socket send buffer size. System default is used if 0.
        ///
        ///     default: 0
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SocketSendBufferBytes { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Broker socket receive buffer size. System default is used if 0.
        ///
        ///     default: 0
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SocketReceiveBufferBytes { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Enable TCP keep-alives (SO_KEEPALIVE) on broker sockets
        ///
        ///     default: false
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? SocketKeepaliveEnable { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Disable the Nagle algorithm (TCP_NODELAY) on broker sockets.
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? SocketNagleDisable { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Disconnect from broker when this number of send failures (e.g., timed out requests) is reached. Disable with 0. WARNING: It is highly recommended to leave this setting at its default value of 1 to avoid the client and broker to become desynchronized in case of request timeouts. NOTE: The connection is automatically re-established.
        ///
        ///     default: 1
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SocketMaxFails { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     How long to cache the broker address resolving results (milliseconds).
        ///
        ///     default: 1000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? BrokerAddressTtl { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Allowed broker IP address families: any, v4, v6
        ///
        ///     default: any
        ///     importance: low
        /// ]]>
        /// </summary>
        public BrokerAddressFamily? BrokerAddressFamily { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Maximum time allowed for broker connection setup (TCP connection setup as well SSL and SASL handshake). If the connection to the broker is not fully functional after this the connection will be closed and retried.
        ///
        ///     default: 30000
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? SocketConnectionSetupTimeoutMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Close broker connections after the specified time of inactivity. Disable with 0. If this property is left at its default value some heuristics are performed to determine a suitable default value, this is currently limited to identifying brokers on Azure (see librdkafka issue #3109 for more info). Actual value can be lower, up to 2s lower, only if `connections.max.idle.ms` >= 4s, as jitter is added to avoid disconnecting all brokers at the same time.
        ///
        ///     default: 0
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? ConnectionsMaxIdleMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The initial time to wait before reconnecting to a broker after the connection has been closed. The time is increased exponentially until `reconnect.backoff.max.ms` is reached. -25% to +50% jitter is applied to each reconnect backoff. A value of 0 disables the backoff and reconnects immediately.
        ///
        ///     default: 100
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? ReconnectBackoffMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The maximum time to wait before reconnecting to a broker after the connection has been closed.
        ///
        ///     default: 10000
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? ReconnectBackoffMaxMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     librdkafka statistics emit interval. The application also needs to register a stats callback using `rd_kafka_conf_set_stats_cb()`. The granularity is 1000ms. A value of 0 disables statistics.
        ///
        ///     default: 0
        ///     importance: high
        /// ]]>
        /// </summary>
        public int? StatisticsIntervalMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Disable spontaneous log_cb from internal librdkafka threads, instead enqueue log messages on queue set with `rd_kafka_set_log_queue()` and serve log callbacks or events through the standard poll APIs. **NOTE**: Log messages will linger in a temporary queue until the log queue has been set.
        ///
        ///     default: false
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? LogQueue { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Print internal thread name in log messages (useful for debugging librdkafka internals)
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? LogThreadName { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     If enabled librdkafka will initialize the PRNG with srand(current_time.milliseconds) on the first invocation of rd_kafka_new() (required only if rand_r() is not available on your platform). If disabled the application must call srand() prior to calling rd_kafka_new().
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? EnableRandomSeed { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Log broker disconnects. It might be useful to turn this off when interacting with 0.9 brokers with an aggressive `connections.max.idle.ms` value.
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? LogConnectionClose { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Signal that librdkafka will use to quickly terminate on rd_kafka_destroy(). If this signal is not set then there will be a delay before rd_kafka_wait_destroyed() returns true as internal threads are timing out their system calls. If this signal is set however the delay will be minimal. The application should mask this signal as an internal signal handler is installed.
        ///
        ///     default: 0
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? InternalTerminationSignal { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     **DEPRECATED** **Post-deprecation actions: remove this configuration property, brokers < 0.10.0 won't be supported anymore in librdkafka 3.x.** Request broker's supported API versions to adjust functionality to available protocol features. If set to false, or the ApiVersionRequest fails, the fallback version `broker.version.fallback` will be used. **NOTE**: Depends on broker version >=0.10.0. If the request is not supported by (an older) broker the `broker.version.fallback` fallback is used.
        ///
        ///     default: true
        ///     importance: high
        /// ]]>
        /// </summary>
        public bool? ApiVersionRequest { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Timeout for broker API version requests.
        ///
        ///     default: 10000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? ApiVersionRequestTimeoutMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     **DEPRECATED** **Post-deprecation actions: remove this configuration property, brokers < 0.10.0 won't be supported anymore in librdkafka 3.x.** Dictates how long the `broker.version.fallback` fallback is used in the case the ApiVersionRequest fails. **NOTE**: The ApiVersionRequest is only issued when a new connection to the broker is made (such as after an upgrade).
        ///
        ///     default: 0
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? ApiVersionFallbackMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     **DEPRECATED** **Post-deprecation actions: remove this configuration property, brokers < 0.10.0 won't be supported anymore in librdkafka 3.x.** Older broker versions (before 0.10.0) provide no way for a client to query for supported protocol features (ApiVersionRequest, see `api.version.request`) making it impossible for the client to know what features it may use. As a workaround a user may set this property to the expected broker version and the client will automatically adjust its feature set accordingly if the ApiVersionRequest fails (or is disabled). The fallback broker version will be used for `api.version.fallback.ms`. Valid values are: 0.9.0, 0.8.2, 0.8.1, 0.8.0. Any other value >= 0.10, such as 0.10.2.1, enables ApiVersionRequests.
        ///
        ///     default: 0.10.0
        ///     importance: medium
        /// ]]>
        /// </summary>
        public string BrokerVersionFallback { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Allow automatic topic creation on the broker when subscribing to or assigning non-existent topics. The broker must also be configured with `auto.create.topics.enable=true` for this configuration to take effect. Note: the default value (true) for the producer is different from the default value (false) for the consumer. Further, the consumer default value is different from the Java consumer (true), and this property is not supported by the Java producer. Requires broker version >= 0.11.0.0, for older broker versions only the broker configuration applies.
        ///
        ///     default: false
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? AllowAutoCreateTopics { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Protocol used to communicate with brokers.
        ///
        ///     default: plaintext
        ///     importance: high
        /// ]]>
        /// </summary>
        public SecurityProtocol? SecurityProtocol { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     A cipher suite is a named combination of authentication, encryption, MAC and key exchange algorithm used to negotiate the security settings for a network connection using TLS or SSL network protocol. See manual page for `ciphers(1)` and `SSL_CTX_set_cipher_list(3).
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCipherSuites { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The supported-curves extension in the TLS ClientHello message specifies the curves (standard/named, or 'explicit' GF(2^k) or GF(p)) the client is willing to have the server use. See manual page for `SSL_CTX_set1_curves_list(3)`. OpenSSL >= 1.0.2 required.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCurvesList { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The client uses the TLS ClientHello signature_algorithms extension to indicate to the server which signature/hash algorithm pairs may be used in digital signatures. See manual page for `SSL_CTX_set1_sigalgs_list(3)`. OpenSSL >= 1.0.2 required.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslSigalgsList { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to client's private key (PEM) used for authentication.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslKeyLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Private key passphrase (for use with `ssl.key.location` and `set_ssl_cert()`)
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslKeyPassword { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client's private key string (PEM format) used for authentication.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslKeyPem { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to client's public key (PEM) used for authentication.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCertificateLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client's public key string (PEM format) used for authentication.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCertificatePem { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     File or directory path to CA certificate(s) for verifying the broker's key. Defaults: On Windows the system's CA certificates are automatically looked up in the Windows Root certificate store. On Mac OSX this configuration defaults to `probe`. It is recommended to install openssl using Homebrew, to provide CA certificates. On Linux install the distribution's ca-certificates package. If OpenSSL is statically linked or `ssl.ca.location` is set to `probe` a list of standard paths will be probed and the first one found will be used as the default CA certificate location path. If OpenSSL is dynamically linked the OpenSSL library's default path will be used (see `OPENSSLDIR` in `openssl version -a`).
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCaLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     File or directory path to CA certificate(s) for verifying HTTPS endpoints, like `sasl.oauthbearer.token.endpoint.url` used for OAUTHBEARER/OIDC authentication. Mutually exclusive with `https.ca.pem`. Defaults: On Windows the system's CA certificates are automatically looked up in the Windows Root certificate store. On Mac OSX this configuration defaults to `probe`. It is recommended to install openssl using Homebrew, to provide CA certificates. On Linux install the distribution's ca-certificates package. If OpenSSL is statically linked or `https.ca.location` is set to `probe` a list of standard paths will be probed and the first one found will be used as the default CA certificate location path. If OpenSSL is dynamically linked the OpenSSL library's default path will be used (see `OPENSSLDIR` in `openssl version -a`).
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string HttpsCaLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     CA certificate string (PEM format) for verifying HTTPS endpoints. Mutually exclusive with `https.ca.location`. Optional: see `https.ca.location`.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string HttpsCaPem { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     CA certificate string (PEM format) for verifying the broker's key.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCaPem { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Comma-separated list of Windows Certificate stores to load CA certificates from. Certificates will be loaded in the same order as stores are specified. If no certificates can be loaded from any of the specified stores an error is logged and the OpenSSL library's default CA location is used instead. Store names are typically one or more of: MY, Root, Trust, CA.
        ///
        ///     default: Root
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCaCertificateStores { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to CRL for verifying broker's certificate validity.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslCrlLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to client's keystore (PKCS#12) used for authentication.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslKeystoreLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client's keystore (PKCS#12) password.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslKeystorePassword { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Comma-separated list of OpenSSL 3.0.x implementation providers. E.g., "default,legacy".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslProviders { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     **DEPRECATED** Path to OpenSSL engine library. OpenSSL >= 1.1.x required. DEPRECATED: OpenSSL engine support is deprecated and should be replaced by OpenSSL 3 providers.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslEngineLocation { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     OpenSSL engine id is the name used for loading engine.
        ///
        ///     default: dynamic
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SslEngineId { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Enable OpenSSL's builtin broker (server) certificate verification. This verification can be extended by the application by implementing a certificate_verify_cb.
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? EnableSslCertificateVerification { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Endpoint identification algorithm to validate broker hostname using broker certificate. https - Server (broker) hostname verification as specified in RFC2818. none - No endpoint verification. OpenSSL >= 1.0.2 required.
        ///
        ///     default: https
        ///     importance: low
        /// ]]>
        /// </summary>
        public SslEndpointIdentificationAlgorithm? SslEndpointIdentificationAlgorithm { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Kerberos principal name that Kafka runs as, not including /hostname@REALM
        ///
        ///     default: kafka
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslKerberosServiceName { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     This client's Kerberos principal name. (Not supported on Windows, will use the logon user's principal).
        ///
        ///     default: kafkaclient
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslKerberosPrincipal { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Shell command to refresh or acquire the client's Kerberos ticket. This command is executed on client creation and every sasl.kerberos.min.time.before.relogin (0=disable). %{config.prop.name} is replaced by corresponding config object value.
        ///
        ///     default: kinit -R -t "%{sasl.kerberos.keytab}" -k %{sasl.kerberos.principal} || kinit -t "%{sasl.kerberos.keytab}" -k %{sasl.kerberos.principal}
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslKerberosKinitCmd { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to Kerberos keytab file. This configuration property is only used as a variable in `sasl.kerberos.kinit.cmd` as ` ... -t "%{sasl.kerberos.keytab}"`.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslKerberosKeytab { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Minimum time in milliseconds between key refresh attempts. Disable automatic key refresh by setting this property to 0.
        ///
        ///     default: 60000
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SaslKerberosMinTimeBeforeRelogin { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     SASL username for use with the PLAIN and SASL-SCRAM-.. mechanisms
        ///
        ///     default: ''
        ///     importance: high
        /// ]]>
        /// </summary>
        public string SaslUserName { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     SASL password for use with the PLAIN and SASL-SCRAM-.. mechanism
        ///
        ///     default: ''
        ///     importance: high
        /// ]]>
        /// </summary>
        public string SaslPassword { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     SASL/OAUTHBEARER configuration. The format is implementation-dependent and must be parsed accordingly. The default unsecured token implementation (see https://tools.ietf.org/html/rfc7515#appendix-A.5) recognizes space-separated name=value pairs with valid names including principalClaimName, principal, scopeClaimName, scope, and lifeSeconds. The default value for principalClaimName is "sub", the default value for scopeClaimName is "scope", and the default value for lifeSeconds is 3600. The scope value is CSV format with the default value being no/empty scope. For example: `principalClaimName=azp principal=admin scopeClaimName=roles scope=role1,role2 lifeSeconds=600`. In addition, SASL extensions can be communicated to the broker via `extension_NAME=value`. For example: `principal=admin extension_traceId=123`
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerConfig { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Enable the builtin unsecure JWT OAUTHBEARER token handler if no oauthbearer_refresh_cb has been set. This builtin handler should only be used for development or testing, and not in production.
        ///
        ///     default: false
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? EnableSaslOauthbearerUnsecureJwt { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Set to "default" or "oidc" to control which login method to be used. If set to "oidc", the following properties must also be be specified: `sasl.oauthbearer.client.id`, `sasl.oauthbearer.client.secret`, and `sasl.oauthbearer.token.endpoint.url`.
        ///
        ///     default: default
        ///     importance: low
        /// ]]>
        /// </summary>
        public SaslOauthbearerMethod? SaslOauthbearerMethod { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Public identifier for the application. Must be unique across all clients that the authorization server handles. Only used when `sasl.oauthbearer.method` is set to "oidc".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerClientId { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client secret only known to the application and the authorization server. This should be a sufficiently random string that is not guessable. Only used when `sasl.oauthbearer.method` is set to "oidc".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerClientSecret { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client use this to specify the scope of the access request to the broker. Only used when `sasl.oauthbearer.method` is set to "oidc".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerScope { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Allow additional information to be provided to the broker. Comma-separated list of key=value pairs. E.g., "supportFeatureX=true,organizationId=sales-emea".Only used when `sasl.oauthbearer.method` is set to "oidc".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerExtensions { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     OAuth/OIDC issuer token endpoint HTTP(S) URI used to retrieve token. Only used when `sasl.oauthbearer.method` is set to "oidc".
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerTokenEndpointUrl { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     OAuth grant type to use when communicating with the identity provider.
        ///
        ///     default: client_credentials
        ///     importance: low
        /// ]]>
        /// </summary>
        public SaslOauthbearerGrantType? SaslOauthbearerGrantType { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Algorithm the client should use to sign the assertion sent to the identity provider and in the OAuth alg header in the JWT assertion.
        ///
        ///     default: RS256
        ///     importance: low
        /// ]]>
        /// </summary>
        public SaslOauthbearerAssertionAlgorithm? SaslOauthbearerAssertionAlgorithm { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to client's private key (PEM) used for authentication when using the JWT assertion.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionPrivateKeyFile { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Private key passphrase for `sasl.oauthbearer.assertion.private.key.file` or `sasl.oauthbearer.assertion.private.key.pem`.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionPrivateKeyPassphrase { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Client's private key (PEM) used for authentication when using the JWT assertion.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionPrivateKeyPem { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to the assertion file. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionFile { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     JWT audience claim. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionClaimAud { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Assertion expiration time in seconds. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: 300
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SaslOauthbearerAssertionClaimExpSeconds { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     JWT issuer claim. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionClaimIss { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     JWT ID claim. When set to `true`, a random UUID is generated. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: false
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? SaslOauthbearerAssertionClaimJtiInclude { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Assertion not before time in seconds. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: 60
        ///     importance: low
        /// ]]>
        /// </summary>
        public int? SaslOauthbearerAssertionClaimNbfSeconds { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     JWT subject claim. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionClaimSub { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Path to the JWT template file. Only used when `sasl.oauthbearer.method` is set to "oidc" and JWT assertion is needed.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string SaslOauthbearerAssertionJwtTemplateFile { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Type of metadata-based authentication to use for OAUTHBEARER/OIDC `azure_imds` authenticates using the Azure IMDS endpoint. Sets a default value for `sasl.oauthbearer.token.endpoint.url` if missing. Configuration values specific of chosen authentication type can be passed through `sasl.oauthbearer.config`.
        ///
        ///     default: none
        ///     importance: low
        /// ]]>
        /// </summary>
        public SaslOauthbearerMetadataAuthenticationType? SaslOauthbearerMetadataAuthenticationType { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     List of plugin libraries to load (; separated). The library search path is platform dependent (see dlopen(3) for Unix and LoadLibrary() for Windows). If no filename extension is specified the platform-specific extension (such as .dll or .so) will be appended automatically.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string PluginLibraryPaths { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     A rack identifier for this client. This can be any string value which indicates where this client is physically located. It corresponds with the broker config `broker.rack`.
        ///
        ///     default: ''
        ///     importance: low
        /// ]]>
        /// </summary>
        public string ClientRack { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The backoff time in milliseconds before retrying a protocol request, this is the first backoff time, and will be backed off exponentially until number of retries is exhausted, and it's capped by retry.backoff.max.ms.
        ///
        ///     default: 100
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? RetryBackoffMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     The max backoff time in milliseconds before retrying a protocol request, this is the atmost backoff allowed for exponentially backed off requests.
        ///
        ///     default: 1000
        ///     importance: medium
        /// ]]>
        /// </summary>
        public int? RetryBackoffMaxMs { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Controls how the client uses DNS lookups. By default, when the lookup returns multiple IP addresses for a hostname, they will all be attempted for connection before the connection is considered failed. This applies to both bootstrap and advertised servers. If the value is set to `resolve_canonical_bootstrap_servers_only`, each entry will be resolved and expanded into a list of canonical names. **WARNING**: `resolve_canonical_bootstrap_servers_only` must only be used with `GSSAPI` (Kerberos) as `sasl.mechanism`, as it's the only purpose of this configuration value. **NOTE**: Default here is different from the Java client's default behavior, which connects only to the first IP address returned for a hostname.
        ///
        ///     default: use_all_dns_ips
        ///     importance: low
        /// ]]>
        /// </summary>
        public ClientDnsLookup? ClientDnsLookup { get; set; }

        /// <summary>
        /// <![CDATA[
        ///     Whether to enable pushing of client metrics to the cluster, if the cluster has a client metrics subscription which matches this client
        ///
        ///     default: true
        ///     importance: low
        /// ]]>
        /// </summary>
        public bool? EnableMetricsPush { get; set; }

        // ReSharper restore UnusedMember.Global

        #endregion

		/// <summary>
		/// Add a new internal topic.
		/// </summary>
		/// <param name="name">Topic Name</param>
		/// <param name="topicCreationConfig"></param>
		public KafkaStreamOptions AddTopic(string name, TopicCreationConfig topicCreationConfig = null)
		{
			var config = new TopicConfig { IsExternal = false, Name = name };
			if (topicCreationConfig != null)
			{
				config.AutoCreate = topicCreationConfig.AutoCreate;
				config.Partitions = topicCreationConfig.Partitions;
				config.ReplicationFactor = topicCreationConfig.ReplicationFactor;
				if (topicCreationConfig.RetentionPeriodInMs.HasValue)
				{
					config.RetentionPeriodInMs = topicCreationConfig.RetentionPeriodInMs;
				}
			}

			Topics.Add(config);

			return this;
		}

		/// <summary>
		/// Add a new external topic.
		/// </summary>
		/// <param name="name">Topic Name</param>
		/// <param name="topicCreationConfig"></param>
		public KafkaStreamOptions AddExternalTopic<T>(string name, TopicCreationConfig topicCreationConfig = null)
		{
			AddExternalTopic(typeof(T), name, topicCreationConfig);
			return this;
		}

		/// <summary>
		/// Add a new external topic.
		/// </summary>
		/// <param name="type">The data type that this contract will use.</param>
		/// <param name="name">Topic Name</param>
		/// <param name="topicCreationConfig"></param>
		public KafkaStreamOptions AddExternalTopic(
			Type type,
			string name,
			TopicCreationConfig topicCreationConfig = null
		)
		{
			var config = new TopicConfig { IsExternal = true, Name = name, ExternalContractType = type };
			if (topicCreationConfig != null)
			{
				config.AutoCreate = topicCreationConfig.AutoCreate;
				config.Partitions = topicCreationConfig.Partitions;
				config.ReplicationFactor = topicCreationConfig.ReplicationFactor;
			}

			Topics.Add(config);

			return this;
		}
	}

	public class Credentials
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string SslCaLocation { get; set; }
	}

	public class TopicConfig
	{
		public string Name { get; set; }

		/// <summary>
		/// Specifies whether the topic will be produced by producers external to the silo
		/// </summary>
		public bool IsExternal { get; set; }

		/// <summary>
		/// The expected DataType that is expected on this topic
		/// </summary>
		public Type ExternalContractType { get; set; }

		/// <summary>
		/// Determines whether the topic will be auto created
		/// </summary>
		/// <remarks><c>false</c> by default.</remarks>
		public bool AutoCreate { get; set; }

		/// <summary>
		/// If <see cref="AutoCreate"/> is true the topic will
		/// be created with set number of topics
		/// </summary>
		/// <remarks>-1 by default</remarks>
		public int Partitions { get; set; } = -1;

		/// <summary>
		/// If <see cref="AutoCreate"/> is true the topic will
		/// be created with the Replication Factor defined
		/// </summary>
		/// <remarks>1 by default</remarks>
		public short ReplicationFactor { get; set; } = 1;

		/// <summary>
		/// If <see cref="RetentionPeriodInMs"/> is set the topic will
		/// be created with only retain data for this much duration.
		/// If not set, it'll take default configuration of broker which is 7 days.
		/// </summary>
		/// <remarks>7 days by default at broker level if not set</remarks>
		public ulong? RetentionPeriodInMs { get; set; }
	}

	public class TopicCreationConfig
	{
		/// <summary>
		/// Determines whether the topic will be auto created
		/// </summary>
		/// <remarks><c>false</c> by default.</remarks>
		public bool AutoCreate { get; set; }

		/// <summary>
		/// If <see cref="AutoCreate"/> is true the topic will
		/// be created with set number of topics
		/// </summary>
		/// <remarks>-1 by default</remarks>
		public int Partitions { get; set; } = -1;

		/// <summary>
		/// If <see cref="AutoCreate"/> is true the topic will
		/// be created with the Replication Factor defined
		/// </summary>
		/// <remarks>1 by default</remarks>
		public short ReplicationFactor { get; set; } = 1;

		/// <summary>
		/// If <see cref="RetentionPeriodInMs"/> is set the topic will
		/// be created with only retain data for this much duration in milliseconds.
		/// If not set, it'll take default configuration of broker which is 7 days.
		/// </summary>
		/// <remarks>7 days by default</remarks>
		public ulong? RetentionPeriodInMs { get; set; }
	}

	public enum ConsumeMode
	{
		StreamStart = 0,
		LastCommittedMessage = 1,
		StreamEnd = 2
	}
}