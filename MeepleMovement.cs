using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepleMovement : MonoBehaviour
{
	//This script should contain all movement related code for Meeples. Methods here will be called from other Scripts, such as MeepleNeeds or MeepleActions.
	//The methods here will get references to the appropriate Station (sent from other scripts) and handle all navmesh pathfinding to the new object.
	//If we are tracking the distance required to move to a Station, this will also be handled in this script.
	//This script will also track the position of the Meeple & the position of the Station, and call methods from MeepleActions when a Meeple has arrived at a Station.

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
