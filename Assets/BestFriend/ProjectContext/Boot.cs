using UnityEngine;

public class Boot : MonoBehaviour {
	[SerializeField] private Project project;

	private async void Awake() {
		await project.Initialize();

//		await FirebaseApi.instance.RegistrationWithEmailAndPassword("aabb.cc", "111");
		if (FirebaseApi.instance.isSigned) {
			await FirebaseApi.instance.SetUserData(new UserData() {age = "19", gender = "female", name = "Grisha1"});
			await FirebaseApi.instance.SetAvatarData(new AvatarData() {gender = "male", name = "Dyadya Petya"});
			await FirebaseApi.instance.SetAvatarMeshData(new AvatarMeshData() {hair = 2, head = 1, body = 3, eyes = 21, legs = 24});
			var avatar = await FirebaseApi.instance.GetAvatarData();
			var user = await FirebaseApi.instance.GetUserData();
			var mesh = await FirebaseApi.instance.GetAvatarMeshData();
			var openAi = await FirebaseApi.instance.GetAiKey();
			var google = await FirebaseApi.instance.GetSpeechKey();
		}
		else {
			await FirebaseApi.instance.SignInWithEmailAndPassword("aa@bb.cc", "111111");
		}
//		ScenesLoader.instance.GoToIntroScene();
		DontDestroyOnLoad(project);
	}
}