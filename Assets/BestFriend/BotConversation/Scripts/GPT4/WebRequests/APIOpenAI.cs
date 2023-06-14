using System;
using Cysharp.Threading.Tasks;
using SerializeDeserialize;

public class APIOpenAI : Requests {
	public static APIOpenAI instance;
	public GptConnectionSettings connectionSettings {
		set => _connectionSettings = value;
	}

	private GptConnectionSettings _connectionSettings;
	public void Initialize() {
		if (instance is not null) return;

		instance = this;
	}

	public async UniTask<ChatApiResponse> Send(ChatApiRequest request) {
		if (!_connectionSettings) throw new Exception("Connection settings not found");

		authorKey = $"Bearer {_connectionSettings.privateApiKey}";
		return await PostJsonBodyAsync<ChatApiResponse, ChatApiRequest>(_connectionSettings.completionUrl, request);
	}
}
