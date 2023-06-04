using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ProgressController : MonoBehaviour
{
    // initialized here for now.
    // should serialize and read from disk
    // if persisting.
    private int totalRevolutions = 0;
    private int lastRevolutions = 0;
    private int enemyXP = 0;

    public int jumpingJacksGoal { get; private set; } = -1;
    public int cyclingGoal { get; private set; } = -1;

    //public int cyclingGoal { get; set; } = 30;

    public int playerLevel { get; private set; } = -1;
    public int enemyLevel { get; private set; } = -1;
    public int playerXP { get; private set; } = -1;
    public int xPBuffedFightsRemaining { get; private set; } = 0;


    [SerializeField]
    private int playerXPPerLevel;

    [SerializeField]
    private int enemyXPPerLevel;

    [SerializeField]
    private int playerXPPerWonBattle;

    [SerializeField]
    private float playerXPExerciseMultiplyer; // only used if xPBuffedFightsRemaining > 0

    [SerializeField]
    private int enemyXPPerBattle;

    [SerializeField]
    private int exerciseBuffLength; // measured in number of combat scenarios / "fights"

    [SerializeField]
    private Card allyCard1;
    [SerializeField]
    private Card allyCard2;
    [SerializeField]
    private Card allyCard3;
    [SerializeField]
    private Card allyCard4;
    [SerializeField]
    private Card allyCard5;

    [SerializeField]
    private Card enemyCard1;
    [SerializeField]
    private Card enemyCard2;
    [SerializeField]
    private Card enemyCard3;
    [SerializeField]
    private Card enemyCard4;
    [SerializeField]
    private Card enemyCard5;

    List<Card> allyCards, enemyCards;

    private Random rng;

    [SerializeField]
    private Text infoDisplay;

    public void loadPersistent(){

        if (PlayerPrefs.HasKey("totalRevolutions"))
        {
            totalRevolutions = PlayerPrefs.GetInt("totalRevolutions");
            lastRevolutions = PlayerPrefs.GetInt("lastRevolutions");
            enemyLevel = PlayerPrefs.GetInt("enemyLevel");
            enemyXP = PlayerPrefs.GetInt("enemyXP");
            playerLevel = PlayerPrefs.GetInt("playerLevel");
            playerXP = PlayerPrefs.GetInt("playerXP");
            jumpingJacksGoal = PlayerPrefs.GetInt("jumpingJacksGoal");
            cyclingGoal = PlayerPrefs.GetInt("cyclingGoal");
            xPBuffedFightsRemaining = PlayerPrefs.GetInt("xPBuffedFightsRemaining");
        } else {
            PlayerPrefs.SetInt("totalRevolutions", 0);
            PlayerPrefs.SetInt("lastRevolutions", 0);
            PlayerPrefs.SetInt("enemyLevel", 1);
            PlayerPrefs.SetInt("enemyXP", 0);
            PlayerPrefs.SetInt("playerLevel", 1);
            PlayerPrefs.SetInt("playerXP", 0);
            PlayerPrefs.SetInt("jumpingJacksGoal", 30);
            PlayerPrefs.SetInt("cyclingGoal", 20);
            PlayerPrefs.SetInt("xPBuffedFightsRemaining", 0);

            loadPersistent();
        }
    }

    public void savePersistent(){

        PlayerPrefs.SetInt("totalRevolutions", totalRevolutions);
        PlayerPrefs.SetInt("lastRevolutions", lastRevolutions);
        PlayerPrefs.SetInt("enemyLevel", enemyLevel);
        PlayerPrefs.SetInt("enemyXP", enemyXP);
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        PlayerPrefs.SetInt("playerXP", playerXP);
        PlayerPrefs.SetInt("xPBuffedFightsRemaining", xPBuffedFightsRemaining);
        PlayerPrefs.SetInt("jumpingJacksGoal", jumpingJacksGoal);
        PlayerPrefs.SetInt("cyclingGoal", cyclingGoal);

        PlayerPrefs.Save();

    }

    public void purgePersistent(){
        Debug.Log("purging data");
        PlayerPrefs.DeleteAll();
        loadPersistent();

        if (infoDisplay != null)
            infoDisplay.text = "Data wiped!";
    }

    public void updateRevolutions(int revolutions)
    {
        lastRevolutions = revolutions - totalRevolutions;
        totalRevolutions = revolutions;

        // could have a check here to make sure the user did enough exercise
        if (lastRevolutions >= cyclingGoal)
            buffPlayer();
        else
            Debug.Log("buff NOT gained");
    }

    public void processJJTime(int seconds)
    {
        Debug.Log("processing JJ time of " + seconds + " seconds");
        if (seconds >= jumpingJacksGoal)
            buffPlayer();
        else
            Debug.Log("buff NOT gained");
    }

    private void buffPlayer()
    {
        Debug.Log("buff gained");
        xPBuffedFightsRemaining = exerciseBuffLength;
        savePersistent();
    }

    public int getTotalRevolutions()
    {
        return totalRevolutions;
    }

    public void onBattleSuccess()
    {
        Debug.Log("BATTLE SUCCESS");

        int tempPlayerXP = playerXP;
        //playerXP += (int) (xPBuffedFightsRemaining-- > 0 ? playerXPPerWonBattle * playerXPExerciseMultiplyer : playerXPPerWonBattle);
        if (xPBuffedFightsRemaining > 0) {
            playerXP += (int) (playerXPPerWonBattle * playerXPExerciseMultiplyer);
        } else {
            playerXP += playerXPPerWonBattle;
        }
        
        Debug.Log("Gained XP: " + (playerXP - tempPlayerXP));

        onBattleFailure(); // still need to increase enemy XP

        int tempPlayerLevel = playerLevel;
        playerLevel = (playerXP / playerXPPerLevel) + 1;


        if(playerLevel > tempPlayerLevel) {
            Debug.Log("levelling up card");
            levelUpRandomCard(allyCards);
        // call a UI script
        // display LEVEL UP! or something like that
        }


        savePersistent();

    }

    public void onBattleFailure()
    {
        Debug.Log("INCREASING ENEMY XP");

        int tempEnemyLevel = enemyLevel;
        enemyXP += enemyXPPerBattle;
        enemyLevel = (enemyXP / enemyXPPerLevel) + 1;
        xPBuffedFightsRemaining = --xPBuffedFightsRemaining < 0 ? 0 : xPBuffedFightsRemaining;

        if(enemyLevel > tempEnemyLevel) {
            levelUpRandomCard(enemyCards);
        // call a UI script here too?
        // display ENEMY LEVEL UP! or something like that
        }

        savePersistent();
    }

    private void levelUpRandomCard(List<Card> cards)
    {
        Card card = cards[rng.Next(0, cards.Count)]; // exclusive upper bound


        if (rng.Next(0, 2) == 0)
        {
            card.atkDamage++;
            Debug.Log("buffed " + card.name + "'s atk damage to " + card.atkDamage);
        }
        else
        {
            card.health++;
            Debug.Log("buffed " + card.name + "'s health to " + card.health);
        }
    }

    public void submitJJGoal(int goal)
    {
        Debug.Log("setting JJ goal to " + goal);
        jumpingJacksGoal = goal;
        savePersistent();
    }

    public void submitCyclingGoal(int goal)
    {
        Debug.Log("setting cycling goal to " + goal);
        cyclingGoal = goal;
        savePersistent();
    }

    // Start is called before the first frame update
    void Start()
    {
        loadPersistent();

        rng = new Random();
        allyCards = new List<Card>();
        enemyCards = new List<Card>();
        allyCards.AddRange(new List<Card>
        {
            allyCard1, allyCard2, allyCard3, allyCard4, allyCard5
        });
        enemyCards.AddRange(new List<Card>
        {
            enemyCard1, enemyCard2, enemyCard3, enemyCard4, enemyCard5
        });

        //DEBUG:
        //purgePersistent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
