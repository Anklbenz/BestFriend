[System.Serializable]
public class UserData : Data {
	public string name;
	public string gender;
	public string age;
}

[System.Serializable]
public class AvatarData : Data {
	public string name;
	public string gender;
}

[System.Serializable]
public class AvatarMeshData : Data {
	//... for example
	public int head;
	public int body;
	public int legs;
	public int hair;
	public int eyes;
}

[System.Serializable]
public class Data {
	public string raw;
}