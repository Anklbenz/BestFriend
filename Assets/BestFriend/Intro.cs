using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Intro : MonoBehaviour {
	private const string NAME_TAG = "#name";
	private const int DELAY_BETWEEN_MESSAGES = 1000;

	/*[TextArea(2,2)]*/ [SerializeField]
	private string[] messages;
	[SerializeField] private Typewriter typewriter;
	[SerializeField] private PlayerInput nameField;
	[SerializeField] private Calendar calendar;
	private void Awake() {
		nameField.gameObject.SetActive(false);
	}

	private async void Start() {
		await Say(messages[0]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);
		
		await Say(messages[1]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		var playerName = await AskAndAwaitAnswer(messages[2]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await Say(messages[3].Replace(NAME_TAG, playerName));
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);
		
		await AskAndAwaitAnswer(messages[4]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);
		
		await Say(messages[5]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await AskAndAwaitDate(messages[6]);
	}

	private async UniTask Say(string message, int delayBeforeHide = 2000) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);
		await UniTask.Delay(delayBeforeHide);
		typewriter.Close();
	}

	private async UniTask<string> AskAndAwaitAnswer(string message) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		nameField.Open();
		var answer = await nameField.AwaitEnter();
		nameField.Close();
		typewriter.Close();
		return answer;
	}

	private async UniTask<DateTime> AskAndAwaitDate(string message) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		

		return DateTime.Now;
	}
}