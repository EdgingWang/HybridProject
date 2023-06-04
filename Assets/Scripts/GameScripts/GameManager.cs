using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public EnemyController EnemyController;
    public PlayerManager PlayerManager;
    public CardEffects cardEffects;

    public List<Card> AllyCards = new List<Card>();
    public List<Card> EnemyCards = new List<Card>();
    public List<GameObject> AllyObjects = new List<GameObject>();

    public bool IsPlayerTurn;
    public int GameState;//0 in progress 1 win 2 lose
    public bool progressControllerCalled = false;

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize()
    {
        GameState = 0;
        IsPlayerTurn = true;
        AllyObjects.Clear();
        PlayerManager.GetObjects();
        PlayerManager.ResetPlayerHp(100);
        PlayerManager.ResetPlayerMana(10);
        AllyCards.Clear();
        EnemyCards.Clear();
        progressControllerCalled = false;
    }

    public bool isCardListFull()
    {
        return AllyObjects.Count >= 5;
    }

    public void PlayCard(GameObject card, GameObject target)
    {
        // This card is which we currently use
        Card c = card.transform.GetComponent<MobsDisplay>().GetCard();

        if (target != null)
        {
            Card targetc = target.transform.GetComponent<MobsDisplay>().GetCard();
        }

        PlayerManager.ConsumeMana(c.cost);
        if (c.cardType == 0)//mob
        {
            AllyCards.Add(Object.Instantiate(c));
            AllyObjects.Add(card);
            card.transform.GetComponent<MobsDisplay>().SetInhand(false);
        }
        else
        {
            Debug.Log("Card Played");
            Card tc = target.transform.GetComponent<MobsDisplay>().GetCard();
            cardEffects.selectEffect(c, tc);
            Destroy(card);
        }
    }

    public void defenderDie(Card c, bool isAllyCardDead)
    {
        if (isAllyCardDead)
        {
            Destroy(AllyObjects[AllyCards.IndexOf(c)]);
            AllyObjects.RemoveAt(AllyCards.IndexOf(c));
            AllyCards.Remove(c);
        }
        else {
            Debug.Log("GameManager.defenderDie. Removing " + c.name);
            EnemyCards.Remove(c);
        }
    }

    public void Atk(List<Card> atkCards, List<Card> defCards, bool isAllyCardDefender)
    {
        int atkIter = 0;
        for (int i = 0; i < defCards.Count; i++)
        {
            Card defender = defCards[i];
            while (atkCards.Count > atkIter)
            {
                Card attacker = atkCards[atkIter];
                if (defender.health - attacker.atkDamage <= 0)
                {
                    defender.health = 0;
                    Debug.Log("calling GameManager.defenderDie with " + defender);
                    defenderDie(defender, isAllyCardDefender);
                    CheckGameState();
                    atkIter++;
                    i--;
                    break;
                }
                else
                {
                    defender.health -= attacker.atkDamage;
                    if (isAllyCardDefender)
                        AllyObjects[defCards.IndexOf(defender)].GetComponent<MobsDisplay>().SetCard(defender);
                }
                atkIter++;
            }
            if (atkCards.Count <= atkIter)
                break;
        }
        while(atkCards.Count > atkIter)
        {
            CheckGameState();
            //Enemy is Attacking
            if (GameState == 0 && isAllyCardDefender)
            {
                PlayerManager.GetAtk(atkCards[atkIter].atkDamage);
            }
            atkIter++;
        }
    }

    public void TurnAtk()
    {
        //Assume allies attack first
        Atk(AllyCards, EnemyCards, false);
        Atk(EnemyCards, AllyCards, true);
    }

    public void CheckGameState()
    {
        //Game win
        if (EnemyCards.Count <= 0)
        {
            GameState = 1;
            GameOverPopup(true);
        }
        //Game lost
        else if (PlayerManager.GetPlayerHpValue() <= 0)
        {
            GameState = 2;
            GameOverPopup(false);
        }
        //Game in progress
        else
        {
            GameState = 0;
        }
    }

    public void GameOverPopup(bool isGameWin)
    {
        if (progressControllerCalled) return;
        progressControllerCalled = true;

        if (isGameWin)
        {
            GameObject.Find("ProgressController").GetComponent<ProgressController>().onBattleSuccess();
        }
        else
        {
            GameObject.Find("ProgressController").GetComponent<ProgressController>().onBattleFailure();
        }

        SceneManager.LoadScene("Scenes/MatchOverScene", LoadSceneMode.Additive);
    }
}
