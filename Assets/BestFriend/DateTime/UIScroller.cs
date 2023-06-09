using System;
using UnityEngine;

public class UIScroller : ItemsList<ScrollItem, string> {
	[SerializeField] private UIStickyScroll stickyScroll;
	[SerializeField] private string[] serializedValues;
	public event Action SelectedEvent;
	public ScrollItem selected => lastSelected.GetComponent<ScrollItem>();
	private RectTransform lastSelected => stickyScroll.lastSelected;
	private void Awake() =>
		stickyScroll.SelectedEvent += OnSelected;

	public void Initialize() {
		if (serializedValues.Length == 0) return;
		AddItems(serializedValues);
	}

	public override bool AddItems(string[] items) {
		base.AddItems(items);
		SelectByIndex(0);
		return true;
	}

	public void ShowFirst(int countItems) {
		if (countItems-1 > count) countItems = count;

		for (int i = 0; i < count; i++)
			itemsList[i].isEnabled = i < countItems - 1;
	}
	
	public int GetSelectedIndex() =>
			itemsList.IndexOf(selected);

	public void SelectByIndex(int index) =>
			stickyScroll.MoveTo((RectTransform)itemsList[index].transform);

	private void OnSelected() =>
			SelectedEvent?.Invoke();


	protected override void OnItemCreate(ScrollItem item) {}
	protected override void OnItemDestroy(ScrollItem item) {}
}