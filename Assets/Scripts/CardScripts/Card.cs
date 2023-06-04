using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class Card : ScriptableObject
{
	public enum CardID
	{

		// Ally cards
		UNASSIGNED = 0,
		bikerMike = 1,
		muscleToph = 2,
		dancingJodey = 3,
		bibby = 4,
		gymnasticsElf = 5,

		// Enemy cards
		lazyAlien = 6,
		couchPotato = 7,
		fatSlime = 8,
		procrastinatingZombie = 9,
		hbpbandit = 10,

		// Spell cards
		proteinShake = 11,
		runAMarathon = 12,
		aerobics = 13,
		plank = 14,
		yoga = 15,
		cheerleading = 16,
		fencing = 17,
		reps = 18,
		strengthOfHercules = 19,
		sportsStarEncouragement = 20,
		benching = 21,
		taichi = 22,

	};                  // Card ID

	public CardID cid;
	new public string name = "New Card";    // Name of the item
	public string desc = "N/A";
	public Sprite icon = null;              // Item icon
	public int cost = 0;
	public bool isAlly = false;
	public int health = -1;
	public int atkDamage;
	public Guid id = Guid.NewGuid();
	public int cardType;//0 Mob 1 Spell

	// Card effect. Run when card is played.
	public virtual void Effect()
	{
		// Use the item
		// Something may happen
		Debug.Log("Playing card " + name);
	}
}


