using System;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class FirebaseApi {
	private const string API_KEYS = "apiKeys";
	private const string OPEN_AI_KEY = "ai";
	private const string GOOGLE_SPEECH_KEY = "speech";
	private const string USERS_DATA = "usersData";
	private const string AVATAR_DATA = "avatarData";
	private const string AVATAR_MESH_DATA = "avatarMeshData";
	private const string USERS_SETTINGS = "usersSettings";
	private string aiPath => $"{API_KEYS}/{OPEN_AI_KEY}";
	private string speechPath => $"{API_KEYS}/{GOOGLE_SPEECH_KEY}";
	private string userDataPath => $"{USERS_SETTINGS}/{user.UserId}/{USERS_DATA}";
	private string avatarDataPath => $"{USERS_SETTINGS}/{user.UserId}/{AVATAR_DATA}";
	private string avatarMeshDataPath => $"{USERS_SETTINGS}/{user.UserId}/{AVATAR_MESH_DATA}";

	public FirebaseAuth auth;
	public FirebaseUser user;
	public DatabaseReference database;

	public static FirebaseApi instance;
	public bool isSigned => user != null;
	public string errorMessage { get; private set; }
	public async UniTask<bool> Initialize() {
		instance ??= this;

		try {
			var checkDependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();

			if (checkDependencyResult == DependencyStatus.Available) {
				auth = FirebaseAuth.DefaultInstance;
				user = auth.CurrentUser;
				database = FirebaseDatabase.DefaultInstance.RootReference;
				return true;
			}

			errorMessage = checkDependencyResult.ToString();
			return false;
		}
		catch (Exception exception) {
			errorMessage = exception.GetBaseException().Message;
			return false;
		}
	}

	public async UniTask<bool> RegistrationWithEmailAndPassword(string email, string password) {
		try {
			await auth.CreateUserWithEmailAndPasswordAsync(email, password);
			return true;
		}
		catch (Exception exception) {
			errorMessage = exception.GetBaseException().Message;
		}

		return false;
	}

	public async UniTask<bool> SignInWithEmailAndPassword(string email, string password) {
		try {
			var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);
			user = authResult.User;
			return authResult.User != null;
		}
		catch (Exception exception) {
			errorMessage = exception.GetBaseException().Message;
		}

		return false;
	}

	public void SingOut() =>
			auth.SignOut();

	public async UniTask<bool> SetUserData(UserData data) =>
			await SaveToDatabase(data, userDataPath);
	public async UniTask<UserData> GetUserData() =>
			await GetFromDatabase<UserData>(userDataPath);

	public async UniTask<bool> SetAvatarData(AvatarData data) =>
			await SaveToDatabase(data, avatarDataPath);
	public async UniTask<AvatarData> GetAvatarData() =>
			await GetFromDatabase<AvatarData>(avatarDataPath);

	public async UniTask<bool> SetAvatarMeshData(AvatarMeshData data) =>
			await SaveToDatabase(data, avatarMeshDataPath);

	public async UniTask<AvatarData> GetAvatarMeshData() =>
			await GetFromDatabase<AvatarData>(avatarMeshDataPath);

	public async UniTask<Data> GetAiKey() =>
			await GetFromDatabase<Data>(aiPath);

	public async UniTask<Data> GetSpeechKey() =>
			await GetFromDatabase<Data>(speechPath);

	private async UniTask<T> GetFromDatabase<T>(string databasePath) where T : Data {
		try {
			var dataSnapShot = await database.Child(databasePath).GetValueAsync();

			if (!dataSnapShot.Exists) return null;
			return dataSnapShot.HasChildren ? JsonUtility.FromJson<T>(dataSnapShot.GetRawJsonValue()) : (T)new Data() {raw = dataSnapShot.GetRawJsonValue()};
		}
		catch (Exception exception) {
			errorMessage = exception.GetBaseException().Message;
		}
		return null;
	}

	private async UniTask<bool> SaveToDatabase<T>(T data, string databasePath) {
		try {
			var dataInJson = JsonUtility.ToJson(data);
			await database.Child(databasePath).SetRawJsonValueAsync(dataInJson);
			return true;
		}
		catch (Exception exception) {
			errorMessage = exception.GetBaseException().Message;
		}
		return false;
	}
}