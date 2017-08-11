using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepleNeeds : MonoBehaviour
{
	[SerializeField] float meepleEnergy;
	[SerializeField] float meepleHealth;
	[SerializeField] float meepleThirst;
	[SerializeField] float meepleHunger;
	[SerializeField] float meepleBladder;
	[SerializeField] float meepleBowels;
	[SerializeField] float meepleHygiene;
	[SerializeField] float meepleMoney;
	[SerializeField] float meepleComfort;
	[SerializeField] float meepleEnjoyment;
	[SerializeField] float meepleSocial;
	[SerializeField] float meepleKarma;

	//Min & Max values for each Need. These could be hard coded to a value, or initially set depending on parameters within the MeepleStats script on spawn.
	//Or even manipulated after the game has started, if we so desire.
	float meepleEnergyMin; float meepleEnergyMax;
	float meepleHealthMin; float meepleHealthMax;
	float meepleThirstMin; float meepleThirstMax;
	float meepleHungerMin; float meepleHungerMax;
	float meepleBladderMin; float meepleBladderMax;
	float meepleBowelsMin; float meepleBowelsMax;
	float meepleHygieneMin; float meepleHygieneMax;
	float meepleMoneyMin; float meepleMoneyMax;
	float meepleComfortMin; float meepleComfortMax;
	float meepleEnjoymentMin; float meepleEnjoymentMax;
	float meepleSocialMin; float meepleSocialMax;
	float meepleKarmaMin; float meepleKarmaMax;


	//Independent Decay Rates for each Need. These can be set at spawn or tweaked on the fly as well.
	float meepleEnergyDecayAwake; float meepleEnergyDecayAsleep;
	float meepleHealthDecayAwake; float meepleHealthDecayAsleep;
	float meepleThirstDecayAwake; float meepleThirstDecayAsleep;
	float meepleHungerDecayAwake; float meepleHungerDecayAsleep;
	float meepleBladderDecayAwake; float meepleBladderDecayAsleep;
	float meepleBowelsDecayAwake; float meepleBowelsDecayAsleep;
	float meepleHygieneDecayAwake; float meepleHygieneDecayAsleep;
	float meepleMoneyDecayAwake; float meepleMoneyDecayAsleep;
	float meepleComfortDecayAwake; float meepleComfortDecayAsleep;
	float meepleEnjoymentDecayAwake; float meepleEnjoymentDecayAsleep;
	float meepleSocialDecayAwake; float meepleSocialDecayAsleep;
	float meepleKarmaDecayAwake; float meepleKarmaDecayAsleep;

	bool meepleEnergyDecays;
	bool meepleHealthDecays;
	bool meepleThirstDecays;
	bool meepleHungerDecays;
	bool meepleBladderDecays;
	bool meepleBowelsDecays;
	bool meepleHygieneDecays;
	bool meepleMoneyDecays;
	bool meepleComfortDecays;
	bool meepleEnjoymentDecays;
	bool meepleSocialDecays;
	bool meepleKarmaDecays;

	//Some simple bools to tidy things up, especially before deleting the game object if it dies.
	[SerializeField] bool meepleIsAsleep;
	[SerializeField] bool meepleIsDead;

	MeepleMovement meepleMoveRef;
	MeepleStats meepleStatsRef;

	//This class should contain all of the Needs the Meeple has, as well as control the global decay rate of all the Needs.
	//This class will also contain the various Thresholds of each Need, and call methods from MeepleActions to begin moving to the appropriate Station.
	//This class will also contain flexible methods to allow for the addition / subtraction of various Needs. These methods will be called from other scripts.

	void Awake ()
	{
		meepleEnergy = 1000.0f;
		meepleHealth = 1000.0f;
		meepleThirst = 1000.0f;
		meepleHunger = 1000.0f;
		meepleBladder = 1000.0f;
		meepleBowels = 1000.0f;
		meepleHygiene = 1000.0f;
		meepleMoney = 1000.0f;
		meepleComfort = 1000.0f;
		meepleEnjoyment = 1000.0f;
		meepleSocial = 1000.0f;
		meepleKarma = 1000.0f;

		meepleIsAsleep = false;
		meepleIsDead = false;
	}

	// Use this for initialization
	void Start ()
	{
		if (gameObject.GetComponent<MeepleMovement>() != null)
		{
			meepleMoveRef = gameObject.GetComponent<MeepleMovement>();
		}
		else if (gameObject.GetComponent<MeepleMovement>() == null)
		{
			print ("Error! Couldn't find a MeepleMovement script on " + gameObject.name + ".");
			print ("This error can safely be ignored for now, it's here for when we're calling MeepleMovement methods.");
		}

		meepleEnergyMin = -500.0f; meepleEnergyMax = 1000.0f;
		meepleHealthMin = -500.0f; meepleHealthMax = 1000.0f;
		meepleThirstMin = -500.0f; meepleThirstMax = 1000.0f;
		meepleHungerMin = -500.0f; meepleHungerMax = 1000.0f;
		meepleBladderMin = -500.0f; meepleBladderMax = 1000.0f;
		meepleBowelsMin = -500.0f; meepleBowelsMax = 1000.0f;
		meepleHygieneMin = -500.0f; meepleHygieneMax = 1000.0f;
		meepleMoneyMin = -500.0f; meepleMoneyMax = 1000000.0f;
		meepleComfortMin = -500.0f; meepleComfortMax = 1000.0f;
		meepleEnjoymentMin = -500.0f; meepleEnjoymentMax = 1000.0f;
		meepleSocialMin = -500.0f; meepleSocialMax = 1000.0f;
		meepleKarmaMin = -500.0f; meepleKarmaMax = 1000.0f;

		meepleEnergyDecayAwake = 1.0f; meepleEnergyDecayAsleep = 0.5f;
		meepleHealthDecayAwake = 0.0f; meepleHealthDecayAsleep = 0.0f;
		meepleThirstDecayAwake = 2.0f; meepleThirstDecayAsleep = 1.0f;
		meepleHungerDecayAwake = 2.0f; meepleHungerDecayAsleep = 1.0f;
		meepleBladderDecayAwake = 1.0f; meepleBladderDecayAsleep = 0.5f;
		meepleBowelsDecayAwake = 1.0f; meepleBowelsDecayAsleep = 0.5f;
		meepleHygieneDecayAwake = 1.0f; meepleHygieneDecayAsleep = 0.5f;
		meepleMoneyDecayAwake = 0.0f; meepleMoneyDecayAsleep = 0.0f;
		meepleComfortDecayAwake = 0.0f; meepleComfortDecayAsleep = 0.0f;
		meepleEnjoymentDecayAwake = 1.0f; meepleEnjoymentDecayAsleep = 0.5f;
		meepleSocialDecayAwake = 1.0f; meepleSocialDecayAsleep = 0.5f;
		meepleKarmaDecayAwake = 0.0f; meepleKarmaDecayAsleep = 0.0f;

		meepleEnergyDecays = true;
		meepleHealthDecays = false;
		meepleThirstDecays = true;
		meepleHungerDecays = true;
		meepleBladderDecays = true;
		meepleBowelsDecays = true;
		meepleHygieneDecays = true;
		meepleMoneyDecays = true;
		meepleComfortDecays = false;
		meepleEnjoymentDecays = true;
		meepleSocialDecays = true;
		meepleKarmaDecays = false;
	}
	
	// Update is called once per frame
	// Update here is primarily responsible for running the decay of the various Needs,
	// as well as checking against the various Thresholds which must be met to begin taking certain Actions.
	void Update ()
	{
		if (!meepleIsDead)
		{
			if (meepleEnergyDecays)
			{
				if (meepleEnergy > meepleEnergyMin && meepleEnergy <= meepleEnergyMax && !meepleIsAsleep)
				{
					meepleEnergy -= meepleEnergyDecayAwake * Time.deltaTime;
				}
				else if (meepleEnergy > meepleEnergyMin && meepleEnergy <= meepleEnergyMax && meepleIsAsleep)
				{
					meepleEnergy -= meepleEnergyDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleHealthDecays)
			{
				if (meepleHealth >= meepleHealthMin && meepleHealth <= meepleHealthMax && !meepleIsAsleep)
				{
					meepleHealth -= meepleHealthDecayAwake * Time.deltaTime;
				}
				else if (meepleHealth >= meepleHealthMin && meepleHealth <= meepleHealthMax && meepleIsAsleep)
				{
					meepleHealth -= meepleHealthDecayAsleep * Time.deltaTime;
				}
			}
				
			if (meepleThirstDecays)
			{
				if (meepleThirst >= meepleThirstMin && meepleThirst <= meepleThirstMax && !meepleIsAsleep)
				{
					meepleThirst -= meepleThirstDecayAwake * Time.deltaTime;
				}
				else if (meepleThirst >= meepleThirstMin && meepleThirst <= meepleThirstMax && meepleIsAsleep)
				{
					meepleThirst -= meepleThirstDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleHungerDecays)
			{
				if (meepleHunger >= meepleHungerMin && meepleHunger <= meepleHungerMax && !meepleIsAsleep)
				{
					meepleHunger -= meepleHungerDecayAwake * Time.deltaTime;
				}
				else if (meepleHunger >= meepleHungerMin && meepleHunger <= meepleHungerMax && meepleIsAsleep)
				{
					meepleHunger -= meepleHungerDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleBladderDecays)
			{
				if (meepleBladder >= meepleBladderMin && meepleBladder <= meepleBladderMax && !meepleIsAsleep)
				{
					meepleBladder -= meepleBladderDecayAwake * Time.deltaTime;
				}
				else if (meepleBladder >= meepleBladderMin && meepleBladder <= meepleBladderMax && meepleIsAsleep)
				{
					meepleBladder -= meepleBladderDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleBowelsDecays)
			{
				if (meepleBowels >= meepleBowelsMin && meepleBowels <= meepleBowelsMax && !meepleIsAsleep)
				{
					meepleBowels -= meepleBowelsDecayAwake * Time.deltaTime;
				}
				else if (meepleBowels >= meepleBowelsMin && meepleBowels <= meepleBowelsMax && meepleIsAsleep)
				{
					meepleBowels -= meepleBowelsDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleHygieneDecays)
			{
				if (meepleHygiene >= meepleHygieneMin && meepleHygiene <= meepleHygieneMax && !meepleIsAsleep)
				{
					meepleHygiene -= meepleHygieneDecayAwake * Time.deltaTime;
				}
				else if (meepleHygiene >= meepleHygieneMin && meepleHygiene <= meepleHygieneMax && meepleIsAsleep)
				{
					meepleHygiene -= meepleHygieneDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleMoneyDecays)
			{
				if (meepleMoney >= meepleMoneyMin && meepleMoney <= meepleMoneyMax && !meepleIsAsleep)
				{
					meepleMoney -= meepleMoneyDecayAwake * Time.deltaTime;
				}
				else if (meepleMoney >= meepleMoneyMin && meepleMoney <= meepleMoneyMax && meepleIsAsleep)
				{
					meepleMoney -= meepleMoneyDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleComfortDecays)
			{
				if (meepleComfort >= meepleComfortMin && meepleComfort <= meepleComfortMax && !meepleIsAsleep)
				{
					meepleComfort -= meepleComfortDecayAwake * Time.deltaTime;
				}
				else if (meepleComfort >= meepleComfortMin && meepleComfort <= meepleComfortMax && meepleIsAsleep)
				{
					meepleComfort -= meepleComfortDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleEnjoymentDecays)
			{
				if (meepleEnjoyment >= meepleEnjoymentMin && meepleEnjoyment <= meepleEnjoymentMax && !meepleIsAsleep)
				{
					meepleEnjoyment -= meepleEnjoymentDecayAwake * Time.deltaTime;
				}
				else if (meepleEnjoyment >= meepleEnjoymentMin && meepleEnjoyment <= meepleEnjoymentMax && meepleIsAsleep)
				{
					meepleEnjoyment -= meepleEnjoymentDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleSocialDecays)
			{
				if (meepleSocial >= meepleSocialMin && meepleSocial <= meepleSocialMax && !meepleIsAsleep)
				{
					meepleSocial -= meepleSocialDecayAwake * Time.deltaTime;
				}
				else if (meepleSocial >= meepleSocialMin && meepleSocial <= meepleSocialMax && meepleIsAsleep)
				{
					meepleSocial -= meepleSocialDecayAsleep * Time.deltaTime;
				}
			}

			if (meepleKarmaDecays)
			{
				if (meepleKarma >= meepleKarmaMin && meepleKarma <= meepleKarmaMax && !meepleIsAsleep)
				{
					meepleKarma -= meepleKarmaDecayAwake * Time.deltaTime;
				}
				else if (meepleKarma >= meepleKarmaMin && meepleKarma <= meepleKarmaMax && meepleIsAsleep)
				{
					meepleKarma -= meepleKarmaDecayAsleep * Time.deltaTime;
				}
			}
		}

		if (Input.GetKeyDown("d"))
		{
			AdjustNeed ("Energy", -250.0f);
		}

		if (Input.GetKeyDown("i"))
		{
			AdjustNeed ("Energy", 250.0f);
		}
	}
		
	/// <summary>
	/// Adds a value to the specified Need of a Meeple.
	/// Pass a negative value to subtract instead.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Name of the Need to adjust.</param>
	/// <param name="adjustValue">The value by which to adjust the Need.</param>
	public void AdjustNeed (string needName, float adjustValue)
	{
		if (needName == "Energy")
		{
			if ((meepleEnergy + adjustValue) >= 1000.0f)
			{
				meepleEnergy = 1000.0f;
			}
			else if ((meepleEnergy + adjustValue) <= -500.0f)
			{
				meepleEnergy = -500.0f;
			}
			else
			{
				meepleEnergy += adjustValue;
			}
		}
		else if (needName == "Health")
		{
			if ((meepleHealth + adjustValue) >= 1000.0f)
			{
				meepleHealth = 1000.0f;
			}
			else if ((meepleHealth + adjustValue) <= -500.0f)
			{
				meepleHealth = -500.0f;
			}
			else
			{
				meepleHealth += adjustValue;
			}
		}
		else if (needName == "Thirst")
		{
			if ((meepleThirst + adjustValue) >= 1000.0f)
			{
				meepleThirst = 1000.0f;
			}
			else if ((meepleThirst + adjustValue) <= -500.0f)
			{
				meepleThirst = -500.0f;
			}
			else
			{
				meepleThirst += adjustValue;
			}
		}
		else if (needName == "Hunger")
		{
			if ((meepleHunger + adjustValue) >= 1000.0f)
			{
				meepleHunger = 1000.0f;
			}
			else if ((meepleHunger + adjustValue) <= -500.0f)
			{
				meepleHunger = -500.0f;
			}
			else
			{
				meepleHunger += adjustValue;
			}
		}
		else if (needName == "Bladder")
		{
			if ((meepleBladder + adjustValue) >= 1000.0f)
			{
				meepleBladder = 1000.0f;
			}
			else if ((meepleBladder + adjustValue) <= -500.0f)
			{
				meepleBladder = -500.0f;
			}
			else
			{
				meepleBladder += adjustValue;
			}
		}
		else if (needName == "Bowels")
		{
			if ((meepleBowels + adjustValue) >= 1000.0f)
			{
				meepleBowels = 1000.0f;
			}
			else if ((meepleBowels + adjustValue) <= -500.0f)
			{
				meepleBowels = -500.0f;
			}
			else
			{
				meepleBowels += adjustValue;
			}
		}
		else if (needName == "Hygiene")
		{
			if ((meepleHygiene + adjustValue) >= 1000.0f)
			{
				meepleHygiene = 1000.0f;
			}
			else if ((meepleHygiene + adjustValue) <= -500.0f)
			{
				meepleHygiene = -500.0f;
			}
			else
			{
				meepleHygiene += adjustValue;
			}
		}
		else if (needName == "Money")
		{
			if ((meepleMoney + adjustValue) >= 100000.0f)
			{
				meepleMoney = 100000.0f;
			}
			else if ((meepleMoney + adjustValue) <= -500.0f)
			{
				meepleMoney = -500.0f;
			}
			else
			{
				meepleMoney += adjustValue;
			}
		}
		else if (needName == "Comfort")
		{
			if ((meepleComfort + adjustValue) >= 1000.0f)
			{
				meepleComfort = 1000.0f;
			}
			else if ((meepleComfort + adjustValue) <= -500.0f)
			{
				meepleComfort = -500.0f;
			}
			else
			{
				meepleComfort += adjustValue;
			}
		}
		else if (needName == "Enjoyment")
		{
			if ((meepleEnjoyment + adjustValue) >= 1000.0f)
			{
				meepleEnjoyment = 1000.0f;
			}
			else if ((meepleEnjoyment + adjustValue) <= -500.0f)
			{
				meepleEnjoyment = -500.0f;
			}
			else
			{
				meepleEnjoyment += adjustValue;
			}
		}
		else if (needName == "Social")
		{
			if ((meepleSocial + adjustValue) >= 1000.0f)
			{
				meepleSocial = 1000.0f;
			}
			else if ((meepleSocial + adjustValue) <= -500.0f)
			{
				meepleSocial = -500.0f;
			}
			else
			{
				meepleSocial += adjustValue;
			}
		}
		else if (needName == "Karma")
		{
			if ((meepleKarma + adjustValue) >= 1000.0f)
			{
				meepleKarma = 1000.0f;
			}
			else if ((meepleKarma + adjustValue) <= -500.0f)
			{
				meepleKarma = -500.0f;
			}
			else
			{
				meepleKarma += adjustValue;
			}
		}
		else
		{
			print ("Error! " + gameObject.name + ".MeepleNeeds tried to call AdjustNeed () with the string " + "\"" + needName + "\"" + ". This is not a valid string for this method!");
		}
	}


	/// <summary>
	/// Adjusts the minimum value a Need can be at before it stops decaying.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Need name.</param>
	/// <param name="newNeedMin">New need minimum.</param>
	public void AdjustNeedMin (string needName, float newNeedMin)
	{
		if (needName == "Energy")
		{
			if (meepleEnergyMin != newNeedMin)
			{
				meepleEnergyMin = newNeedMin;
			}
		}
		else if (needName == "Health")
		{
			if (meepleHealthMin != newNeedMin)
			{
				meepleHealthMin = newNeedMin;
			}
		}
		else if (needName == "Thirst")
		{
			if (meepleThirstMin != newNeedMin)
			{
				meepleThirstMin = newNeedMin;
			}
		}
		else if (needName == "Hunger")
		{
			if (meepleHungerMin != newNeedMin)
			{
				meepleHungerMin = newNeedMin;
			}
		}
		else if (needName == "Bladder")
		{
			if (meepleBladderMin != newNeedMin)
			{
				meepleBladderMin = newNeedMin;
			}
		}
		else if (needName == "Bowels")
		{
			if (meepleBowelsMin != newNeedMin)
			{
				meepleBowelsMin = newNeedMin;
			}
		}
		else if (needName == "Hygiene")
		{
			if (meepleHygieneMin != newNeedMin)
			{
				meepleHygieneMin = newNeedMin;
			}
		}
		else if (needName == "Money")
		{
			if (meepleMoneyMin != newNeedMin)
			{
				meepleMoneyMin = newNeedMin;
			}
		}
		else if (needName == "Comfort")
		{
			if (meepleComfortMin != newNeedMin)
			{
				meepleComfortMin = newNeedMin;
			}
		}
		else if (needName == "Enjoyment")
		{
			if (meepleEnjoymentMin != newNeedMin)
			{
				meepleEnjoymentMin = newNeedMin;
			}
		}		else if (needName == "Social")
		{
			if (meepleSocialMin != newNeedMin)
			{
				meepleSocialMin = newNeedMin;
			}
		}
		else if (needName == "Karma")
		{
			if (meepleKarmaMin != newNeedMin)
			{
				meepleKarmaMin = newNeedMin;
			}
		}
	}

	/// <summary>
	/// Adjusts the maximum value a Need can be at after being restored.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Need name.</param>
	/// <param name="newNeedMax">New need max.</param>
	public void AdjustNeedMax (string needName, float newNeedMax)
	{
		if (needName == "Energy")
		{
			if (meepleEnergyMax != newNeedMax)
			{
				meepleEnergyMax = newNeedMax;
			}
		}
		else if (needName == "Health")
		{
			if (meepleHealthMax != newNeedMax)
			{
				meepleHealthMax = newNeedMax;
			}
		}
		else if (needName == "Thirst")
		{
			if (meepleThirstMax != newNeedMax)
			{
				meepleThirstMax = newNeedMax;
			}
		}
		else if (needName == "Hunger")
		{
			if (meepleHungerMax != newNeedMax)
			{
				meepleHungerMax = newNeedMax;
			}
		}
		else if (needName == "Bladder")
		{
			if (meepleBladderMax != newNeedMax)
			{
				meepleBladderMax = newNeedMax;
			}
		}
		else if (needName == "Bowels")
		{
			if (meepleBowelsMax != newNeedMax)
			{
				meepleBowelsMax = newNeedMax;
			}
		}
		else if (needName == "Hygiene")
		{
			if (meepleHygieneMax != newNeedMax)
			{
				meepleHygieneMax = newNeedMax;
			}
		}
		else if (needName == "Money")
		{
			if (meepleMoneyMax != newNeedMax)
			{
				meepleMoneyMax = newNeedMax;
			}
		}
		else if (needName == "Comfort")
		{
			if (meepleComfortMax != newNeedMax)
			{
				meepleComfortMax = newNeedMax;
			}
		}
		else if (needName == "Enjoyment")
		{
			if (meepleEnjoymentMax != newNeedMax)
			{
				meepleEnjoymentMax = newNeedMax;
			}
		}		else if (needName == "Social")
		{
			if (meepleSocialMax != newNeedMax)
			{
				meepleSocialMax = newNeedMax;
			}
		}
		else if (needName == "Karma")
		{
			if (meepleKarmaMax != newNeedMax)
			{
				meepleKarmaMax = newNeedMax;
			}
		}
	}

	/// <summary>
	/// Adjusts the Decay Rate of a Need while the Meeple is Awake.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Need name.</param>
	/// <param name="newDecayRate">New decay rate.</param>
	public void AdjustDecayRateAwake (string needName, float newDecayRate)
	{
		if (needName == "Energy")
		{
			if (meepleEnergyDecayAwake != newDecayRate)
			{
				meepleEnergyDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Health")
		{
			if (meepleHealthDecayAwake != newDecayRate)
			{
				meepleHealthDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Thirst")
		{
			if (meepleThirstDecayAwake != newDecayRate)
			{
				meepleThirstDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Hunger")
		{
			if (meepleHungerDecayAwake != newDecayRate)
			{
				meepleHungerDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Bladder")
		{
			if (meepleBladderDecayAwake != newDecayRate)
			{
				meepleBladderDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Bowels")
		{
			if (meepleBowelsDecayAwake != newDecayRate)
			{
				meepleBowelsDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Hygiene")
		{
			if (meepleHygieneDecayAwake != newDecayRate)
			{
				meepleHygieneDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Money")
		{
			if (meepleMoneyDecayAwake != newDecayRate)
			{
				meepleMoneyDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Comfort")
		{
			if (meepleComfortDecayAwake != newDecayRate)
			{
				meepleComfortDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Enjoyment")
		{
			if (meepleEnjoymentDecayAwake != newDecayRate)
			{
				meepleEnjoymentDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Social")
		{
			if (meepleSocialDecayAwake != newDecayRate)
			{
				meepleSocialDecayAwake = newDecayRate;
			}
		}
		else if (needName == "Karma")
		{
			if (meepleKarmaDecayAwake != newDecayRate)
			{
				meepleKarmaDecayAwake = newDecayRate;
			}
		}
	}


	/// <summary>
	/// Adjusts the Decay Rate of a Need while the Meeple is Asleep.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Need name.</param>
	/// <param name="newDecayRate">New decay rate.</param>
	public void AdjustDecayAsleep (string needName, float newDecayRate)
	{
		if (needName == "Energy")
		{
			if (meepleEnergyDecayAsleep != newDecayRate)
			{
				meepleEnergyDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Health")
		{
			if (meepleHealthDecayAsleep != newDecayRate)
			{
				meepleHealthDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Thirst")
		{
			if (meepleThirstDecayAsleep != newDecayRate)
			{
				meepleThirstDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Hunger")
		{
			if (meepleHungerDecayAsleep != newDecayRate)
			{
				meepleHungerDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Bladder")
		{
			if (meepleBladderDecayAsleep != newDecayRate)
			{
				meepleBladderDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Bowels")
		{
			if (meepleBowelsDecayAsleep != newDecayRate)
			{
				meepleBowelsDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Hygiene")
		{
			if (meepleHygieneDecayAsleep != newDecayRate)
			{
				meepleHygieneDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Money")
		{
			if (meepleMoneyDecayAsleep != newDecayRate)
			{
				meepleMoneyDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Comfort")
		{
			if (meepleComfortDecayAsleep != newDecayRate)
			{
				meepleComfortDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Enjoyment")
		{
			if (meepleEnjoymentDecayAsleep != newDecayRate)
			{
				meepleEnjoymentDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Social")
		{
			if (meepleSocialDecayAsleep != newDecayRate)
			{
				meepleSocialDecayAsleep = newDecayRate;
			}
		}
		else if (needName == "Karma")
		{
			if (meepleKarmaDecayAsleep != newDecayRate)
			{
				meepleKarmaDecayAsleep = newDecayRate;
			}
		}
	}

	/// <summary>
	/// Toggles whether or not the specified Need will Decay.
	/// Applicable needNames are: Energy, Health, Thirst, Hunger, Bladder, Bowels, Hygiene, Money, Comfort, Enjoyment, Social, or Karma.
	/// </summary>
	/// <param name="needName">Need name.</param>
	/// <param name="doesDecay">If set to <c>true</c> does decay.</param>
	public void ToggleNeedDecay (string needName, bool doesDecay)
	{
		if (needName == "Energy")
		{
			if (meepleEnergyDecays != doesDecay)
			{
				meepleEnergyDecays = doesDecay;
			}
		}
		else if (needName == "Health")
		{
			if (meepleHealthDecays != doesDecay)
			{
				meepleHealthDecays = doesDecay;
			}
		}
		else if (needName == "Thirst")
		{
			if (meepleThirstDecays != doesDecay)
			{
				meepleThirstDecays = doesDecay;
			}
		}
		else if (needName == "Hunger")
		{
			if (meepleHungerDecays != doesDecay)
			{
				meepleHungerDecays = doesDecay;
			}
		}
		else if (needName == "Bladder")
		{
			if (meepleBladderDecays != doesDecay)
			{
				meepleBladderDecays = doesDecay;
			}
		}
		else if (needName == "Bowels")
		{
			if (meepleBowelsDecays != doesDecay)
			{
				meepleBowelsDecays = doesDecay;
			}
		}
		else if (needName == "Hygiene")
		{
			if (meepleHygieneDecays != doesDecay)
			{
				meepleHygieneDecays = doesDecay;
			}
		}
		else if (needName == "Money")
		{
			if (meepleMoneyDecays != doesDecay)
			{
				meepleMoneyDecays = doesDecay;
			}
		}
		else if (needName == "Comfort")
		{
			if (meepleComfortDecays != doesDecay)
			{
				meepleComfortDecays = doesDecay;
			}
		}
		else if (needName == "Enjoyment")
		{
			if (meepleEnjoymentDecays != doesDecay)
			{
				meepleEnjoymentDecays = doesDecay;
			}
		}
		else if (needName == "Social")
		{
			if (meepleSocialDecays != doesDecay)
			{
				meepleSocialDecays = doesDecay;
			}
		}
		else if (needName == "Karma")
		{
			if (meepleKarmaDecays != doesDecay)
			{
				meepleKarmaDecays = doesDecay;
			}
		}
	}
}
