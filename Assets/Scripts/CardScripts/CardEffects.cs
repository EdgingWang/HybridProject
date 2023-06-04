using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
	bool reps = false;

	public void selectEffect(Card c, Card targetC)
	{

		
		switch (c.cid)
		{
			case Card.CardID.UNASSIGNED:				effectDEFAULT();					break;
			case Card.CardID.bikerMike:					effectBikerMike();					break;
			case Card.CardID.muscleToph:				effectMuscleToph();					break;
			case Card.CardID.dancingJodey:				effectDancingJodey();				break;
			case Card.CardID.bibby:						effectBibby();						break;
			case Card.CardID.gymnasticsElf:				effectGymnasticsElf();				break;

			case Card.CardID.lazyAlien:					effectLazyAlien();					break;
			case Card.CardID.couchPotato:				effectCouchPotato();				break;
			case Card.CardID.fatSlime:					effectFatSlime();					break;
			case Card.CardID.procrastinatingZombie:		effectProcrastinatingZombie();		break;
			case Card.CardID.hbpbandit:					effectHBPBandit();					break;

			case Card.CardID.proteinShake:				effectProteinShake(targetC);		break;
			case Card.CardID.runAMarathon:				effectRunAMarathon();				break;
			case Card.CardID.aerobics:					effectAerobics(targetC);			break;
			case Card.CardID.plank:						effectPlank(targetC);				break;
			case Card.CardID.yoga:						effectYoga(targetC);				break;
			case Card.CardID.cheerleading:				effectCheerleading(targetC);		break;
			case Card.CardID.fencing:					effectFencing();					break;
			case Card.CardID.reps:						effectReps();						break;
			case Card.CardID.strengthOfHercules:		effectStrengthOfHercules(targetC);	break;
			case Card.CardID.sportsStarEncouragement:	effectSportsStarEncouragement();	break;
			case Card.CardID.benching:					effectBenching(targetC);			break;
			case Card.CardID.taichi:					effectTaichi();						break;

		}
		if(c.cid != Card.CardID.reps)
		{			
			if(reps == true)
			{
				reps = false;
				selectEffect(c, targetC);
				Debug.Log("Reps "+c.name);
			}
		}
	}



	public void effectDEFAULT(){}
	public void effectBikerMike(){}
	public void effectMuscleToph(){}
	public void effectDancingJodey(){}
	public void effectBibby(){}
	public void effectGymnasticsElf(){}
	public void effectLazyAlien(){}
	public void effectCouchPotato(){}
	public void effectFatSlime(){}
	public void effectProcrastinatingZombie(){}
	public void effectHBPBandit(){}

	
	public void effectProteinShake(Card targetC)
	{

		
		targetC.health += 15;

	}

	public void effectRunAMarathon()
	{

		Debug.Log("marathon");

	}

	public void effectAerobics(Card targetC)
	{

		targetC.atkDamage += 15;
		Debug.Log("aerobics");

	}

	public void effectPlank(Card targetC)
	{

		Debug.Log("plank");

	}

	public void effectYoga(Card targetC)
	{

		Debug.Log("yoga");

	}

	public void effectCheerleading(Card targetC)
	{
		if (targetC.isAlly)
		{
			int number_of_damage = targetC.atkDamage;
			int number_of_ennemies = GameObject.Find("EnemyZone").transform.childCount;
			int ennemies = Random.Range(0, number_of_ennemies);

			Card mobCard = GameObject.Find("EnemyZone").transform.GetChild(ennemies).GetComponent<MobsDisplay>().mobCard;
			mobCard.health -= number_of_damage;
			GameObject.Find("EnemyZone").transform.GetChild(ennemies).GetComponent<MobsDisplay>().SetCard(mobCard);

			Debug.Log("cheerleading ennemy "+GameObject.Find("EnemyZone").transform.GetChild(ennemies).GetComponent<MobsDisplay>().mobCard.name +" get "+number_of_damage+" damage");
		}
		
		Debug.Log("cheerleading");

	}

	public void effectFencing()
	{
		/*
        Random rnd = new Random(1); // Make sure this is between 1 and n (where n is the total amount of enemy players on the battlefield)

		int target1 = rnd.Next();
		int target2 = rnd.Next();
		int target3 = rnd.Next();

		damage(target1);
		damage(target2);
		damage(target3);
		*/
		int damage = 2;
		for(int i = 0; i < damage; i++)
		{
			int number_of_ennemies = GameObject.Find("EnemyZone").transform.childCount;
			int ennemies = Random.Range(0, number_of_ennemies);
			Card mobCard = GameObject.Find("EnemyZone").transform.GetChild(ennemies).GetComponent<MobsDisplay>().mobCard;
			mobCard.health -= 1;
			if (mobCard.health - 1 <= 0)
			{
				Destroy(GameObject.Find("EnemyZone").transform.GetChild(ennemies));
			}

			Debug.Log("fencing ennemy "+GameObject.Find("EnemyZone").transform.GetChild(ennemies).GetComponent<MobsDisplay>().mobCard.name +" get 1 damage");
		}
		Debug.Log("fencing");
	}

	public void effectReps()
	{
		reps = true;
		Debug.Log("reps");

	}

	public void effectStrengthOfHercules(Card targetC)
	{
		if (targetC.atkDamage <= 15){

			targetC.atkDamage = 0;
		}
        else {
			targetC.atkDamage -= 15;
		}

		Debug.Log("hercules");

	}

	public void effectSportsStarEncouragement()
	{
		int number_of_allies = GameObject.Find("DropZone").transform.childCount;
		for(int i = 0; i < number_of_allies; i++)
		{
			
			GameObject.Find("DropZone").transform.GetChild(i).GetComponent<MobsDisplay>().mobCard.atkDamage += 4;			
			GameObject.Find("DropZone").transform.GetChild(i).GetComponent<MobsDisplay>().mobCard.health += 4;

		}

		/*
		for(Card s : allyBattlefieldList)
        {

			s.atkDamage += 4;
			s.startingHealth += 4;
        }
		*/
		Debug.Log("sportstar");

	}

	public void effectBenching(Card targetC)
	{

		targetC.health -= targetC.atkDamage;
		if (targetC.health - targetC.atkDamage <= 0)
		{
			Destroy(targetC);
		}
			
	}

	public void effectTaichi()
	{
		/*
		allyBattlefieldList[0].atkDamage *= 2;
		enemyBattlefieldList[0].atkDamage = enemyBattlefieldList[0].atkDamage / 2;  // DOUBLE CHECK THIS IS ROUNDED DOWN
		*/
		GameObject.Find("DropZone").transform.GetChild(0).GetComponent<MobsDisplay>().mobCard.atkDamage *= 2;
		GameObject.Find("EnemyZone").transform.GetChild(0).GetComponent<MobsDisplay>().mobCard.atkDamage = (int) (((float) GameObject.Find("EnemyZone").transform.GetChild(0).GetComponent<MobsDisplay>().mobCard.atkDamage)  /2);
		Debug.Log("taichi");

	}

}
