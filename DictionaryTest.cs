using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryTest : MonoBehaviour
{
	Dictionary<string, float> testDic = new Dictionary<string, float>();

	SortedList<string, bool> testTwo = new SortedList<string, bool>();

	float meepleEnergyMin, meepleEnergyMax;
	float meepleHealthMin, meepleHealthMax;
	float meepleThirstMin, meepleThirstMax;
	float meepleHungerMin, meepleHungerMax;
	float meepleBladderMin, meepleBladderMax;
	float meepleBowelsMin, meepleBowelsMax;
	float meepleHygieneMin, meepleHygieneMax;
	float meepleMoneyMin, meepleMoneyMax;
	float meepleComfortMin, meepleComfortMax;
	float meepleEnjoymentMin, meepleEnjoymentMax;
	float meepleSocialMin, meepleSocialMax;
	float meepleKarmaMin, meepleKarmaMax;

	// Use this for initialization
	void Start ()
	{
		testDic.Add("meepleEnergy", 1000.0f);
		testDic.Add("meepleHealth", 1000.0f);
		testDic.Add("meepleThirst", 1000.0f);
		testDic.Add("meepleHunger", 1000.0f);
		testDic.Add("meepleBladder", 1000.0f);
		testDic.Add("meepleBowels", 1000.0f);
		testDic.Add("meepleHygiene", 1000.0f);
		testDic.Add("meepleMoney", 1000.0f);
		testDic.Add("meepleComfort", 1000.0f);
		testDic.Add("meepleEnjoyment", 1000.0f);
		testDic.Add("meepleSocial", 1000.0f);
		testDic.Add("meepleKarma", 1000.0f);

		testDic.Add("meepleEnergyMin", -500.0f); testDic.Add("meepleEnergyMax", 1000.0f);
		testDic.Add("meepleHealthMin", -500.0f); testDic.Add("meepleHealthMax", 1000.0f);
		testDic.Add("meepleThirstMin", -500.0f); testDic.Add("meepleThirstMax", 1000.0f);
		testDic.Add("meepleHungerMin", -500.0f); testDic.Add("meepleHungerMax", 1000.0f);
		testDic.Add("meepleBladderMin", -500.0f); testDic.Add("meepleBladderMax", 1000.0f);
		testDic.Add("meepleBowelsMin", -500.0f); testDic.Add("meepleBowelsMax", 1000.0f);
		testDic.Add("meepleHygieneMin", -500.0f); testDic.Add("meepleHygieneMax", 1000.0f);
		testDic.Add("meepleMoneyMin", -500.0f); testDic.Add("meepleMoneyMax", 1000.0f);
		testDic.Add("meepleComfortMin", -500.0f); testDic.Add("meepleComfortMax", 1000.0f);
		testDic.Add("meepleEnjoymentMin", -500.0f); testDic.Add("meepleEnjoymentMax", 1000.0f);
		testDic.Add("meepleSocialMin", -500.0f); testDic.Add("meepleSocialMax", 1000.0f);
		testDic.Add("meepleKarmaMin", -500.0f); testDic.Add("meepleKarmaMax", 1000.0f);

		testDic.Add("meepleEnergyDecayRate", 1.0f);
		testDic.Add("meepleHealthDecayRate", 1.0f);
		testDic.Add("meepleThirstDecayRate", 1.0f);
		testDic.Add("meepleHungerDecayRate", 1.0f);
		testDic.Add("meepleBladderDecayRate", 1.0f);
		testDic.Add("meepleBowelsDecayRate", 1.0f);
		testDic.Add("meepleHygieneDecayRate", 1.0f);
		testDic.Add("meepleMoneyDecayRate", 1.0f);
		testDic.Add("meepleComfortDecayRate", 1.0f);
		testDic.Add("meepleEnjoymentDecayRate", 1.0f);
		testDic.Add("meepleSocialDecayRate", 1.0f);
		testDic.Add("meepleKarmaDecayRate", 1.0f);

		testTwo.Add("Energy", true);
		testTwo.Add("Health", false);
		testTwo.Add("Thirst", true);
		testTwo.Add("Hunger", true);
		testTwo.Add("Bladder", true);
		testTwo.Add("Bowels", true);
		testTwo.Add("Hygiene", true);
		testTwo.Add("Money", false);
		testTwo.Add("Comfort", false);
		testTwo.Add("Enjoyment", true);
		testTwo.Add("Social", true);
		testTwo.Add("Karma", false);

		StartCoroutine("DecayNeeds");
	}

	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKeyDown("b"))
		{
			AdjustNeed ("Energy", 250.0f);
			print(testDic["meepleEnergy"]);
		}

		if (Input.GetKeyDown("m"))
		{
			AdjustNeed ("Karma", 250.0f);
			print(testDic["meepleKarma"]);
		}

		if (Input.GetKeyDown("n"))
		{
			AdjustNeed ("Hygiene", 250.0f);
			print(testDic["meepleHygiene"]);
		}

		if (Input.GetKeyDown("v"))
		{
			AdjustNeed ("Bladdder", 250.0f);
			print(testDic["meepleBladder"]);
		}
	}

	public void AdjustNeed (string needName, float adjustValue)
	{
		if (testDic.ContainsKey("meeple" + needName))
		{
			if (testDic["meeple" + needName] + adjustValue <= testDic["meeple" + needName + "Min"])
			{
				testDic["meeple" + needName] = testDic["meeple" + needName + "Min"];
			}
			else if (testDic["meeple" + needName] + adjustValue >= testDic["meeple" + needName + "Max"])
			{
				testDic["meeple" + needName] = testDic["meeple" + needName + "Max"];
			}
			else
			{
				testDic["meeple" + needName] += adjustValue;
			}
		}
	}

	IEnumerator DecayNeeds ()
	{
		foreach (var toggle in testTwo)
		{
			if (toggle.Value)
			{
				testDic["meeple" + toggle.Key] -= testDic["meeple" + toggle.Key + "DecayRate"] * Time.deltaTime;
				print (testDic["meeple" + toggle.Key]);
				print (toggle.Key);
			}
		}
		yield return null;
		StartCoroutine("DecayNeeds");
	}
}
