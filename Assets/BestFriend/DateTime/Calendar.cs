using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Calendar : MonoBehaviour {
	private const int START_YEAR = 2000;
	private const int START_DAY = 1;
	private const int START_MONTH = 1;

	[SerializeField] private UIScroller daysScroll, monthScroll, yearsScroll;
	private readonly Date _date = new();
	private List<string> _yearsNames, _monthNames, _dayNames;

	private void Awake() {
		_yearsNames = _date.GetYears().ToList();
		_monthNames = _date.GetMonths().ToList();

		yearsScroll.AddItems(_yearsNames.ToArray());
		monthScroll.AddItems(_monthNames.ToArray());
		daysScroll.AddItems(_date.GetDayArray(31).ToArray());

		yearsScroll.SelectedEvent += UpdateDays;
		monthScroll.SelectedEvent += UpdateDays;
	}

	public DateTime GetDate() {
		var dateString = daysScroll.selected.text + monthScroll.selected.text + yearsScroll.selected.text;
		Debug.Log(dateString);
		return DateTime.Now;
	}

	private void UpdateDays() {
		var year = yearsScroll.selected;

		var currentValue = daysScroll.GetSelectedIndex() + 1;
		var daysCount = _date.DayInMonth(Int32.Parse(year.text), monthScroll.GetSelectedIndex() + 1);

		if (currentValue > daysCount)
			daysScroll.SelectByIndex(daysCount - 1);

		daysScroll.ShowFirst(daysCount + 1);
	}
}