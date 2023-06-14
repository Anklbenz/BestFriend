using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Intro : MonoBehaviour {
	private const string NAME_TAG = "#name";
	private const int DELAY_BETWEEN_MESSAGES = 1000;

	/*[TextArea(2,2)]*/
	[SerializeField] private Typewriter typewriter;
	[SerializeField] private InputText nameField;
	[SerializeField] private InputDate inputDate;
	[SerializeField] private InputOption inputOption;
	[SerializeField] private string[] messages;
	[SerializeField] private string[] genders;
	[SerializeField] private string playerNamePlaceholderText, aiNamePlaceholderText;
	private void Awake() {
		nameField.ForceClose();
	}

	private async void Start() {
		nameField.Initialize();
		inputDate.Initialize();

		await Say(messages[0]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await Say(messages[1]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		var playerName = await AskAndAwaitAnswer(messages[2], playerNamePlaceholderText);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await Say(messages[3].Replace(NAME_TAG, playerName));
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await AskAndAwaitAnswer(messages[4], aiNamePlaceholderText);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await Say(messages[5]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await AskAndAwaitDate(messages[6]);
		await UniTask.Delay(DELAY_BETWEEN_MESSAGES);

		await AskAndAwaitOpinion(messages[7], genders);
	}

	private async UniTask Say(string message, int delayBeforeHide = 2000) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);
		await UniTask.Delay(delayBeforeHide);
		typewriter.Close();
	}

	private async UniTask<string> AskAndAwaitAnswer(string message, string placeholderHint = "") {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		nameField.placeholder = placeholderHint;
		nameField.Open();
		var answer = await nameField.AwaitInput();
		nameField.Close();
		typewriter.Close();
		return answer;
	}

	private async UniTask<DateTime> AskAndAwaitDate(string message) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		inputDate.Open();
		var date = await inputDate.AwaitInput();
		inputDate.Close();
		typewriter.Close();
		return date;
	}

	private async UniTask<string> AskAndAwaitOpinion(string message, string[] options) {
		inputOption.AddItems(options);
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		inputOption.Open();
		var option = await inputOption.AwaitInput();
		typewriter.Close();
		inputOption.Close();
		return option;
	}
}