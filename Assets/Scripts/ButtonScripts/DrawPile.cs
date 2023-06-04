using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using TMPro;

public class DrawPile : MonoBehaviour
{
    public PlayerManager PlayerManager;
    List<Card> cardlist = new List<Card>();
    private Random rng = new Random();
    public Text numberofcardText;
    private GameObject hand;
    public GameObject MobCard;
    public GameObject SpellCard;
    public Card card1;
    public Card card2;
    public Card card3;
    public Card card4;
    public Card card5;
    public Card card6;
    public Card card7;
    public Card card8;
    public Card card9;
    public Card card10;
    public Card card11;
    public Card card12;
    public Card card13;
    public Card card14;
    public Card card15;
    public Card card16;
    public Card card17;    
    
    GameObject card;

    void Start ()
    {
        Init_deck();
    }
    void Update ()
    {
        numberofcardText.text = cardlist.Count.ToString();
        if (cardlist.Count == 0)
        {
            Init_deck();
        }
    }


    void AddCard (Card card)
    {
        cardlist.Add(card);
        Debug.Log("add card in draw pile"+card);
    }

    Card DrawCard ()
    {
            Card carddraw = cardlist[0];
            cardlist.RemoveAt(0);
            Debug.Log("draw card in draw pile" + carddraw);
            return carddraw;
    }
    
    void create_card()
    {
        AddCard(new Card());
    }

    void Shuffle ()
    {        
        int n = cardlist.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card card = cardlist[k];
            cardlist[k] = cardlist[n];
            cardlist[n] = card;
        }
        Debug.Log("shuffle the draw pile");
    }
    
    public void DrawOneCardFromDrawPile()
    {
        if (cardlist.Count > 0 && PlayerManager.playerMana > 0)
        {
            if(GameObject.Find("Hand Card 1").transform.childCount == 0)
            {
                DrawCardFromDrawPile(1);
            }
            else if(GameObject.Find("Hand Card 2").transform.childCount == 0)
            {
                DrawCardFromDrawPile(2);
            }
            else if(GameObject.Find("Hand Card 3").transform.childCount == 0)
            {
                DrawCardFromDrawPile(3);
            }
            else if(GameObject.Find("Hand Card 4").transform.childCount == 0)
            {
                DrawCardFromDrawPile(4);
            }
            else if(GameObject.Find("Hand Card 5").transform.childCount == 0)
            {
                DrawCardFromDrawPile(5);
            }
            else{
                Debug.Log("Hand is full");
            }
        }
        else
        {
            Debug.Log("can't draw");
        }
    }
    void DrawCardFromDrawPile(int hand_pos)
    {
        if (PlayerManager.GetPlayerManaValue() <= 0)
        {
            Debug.Log("No Mana");
            return;
        }
        else
        {
            PlayerManager.ConsumeMana(1);
        }
        Card carddraw = DrawCard();
        hand = GameObject.Find("Hand Card "+hand_pos);
        if(carddraw.cardType == 0)
        {
            GameObject card = Instantiate(MobCard, hand.transform.position, Quaternion.identity);
            card.transform.SetParent(hand.transform);
            card.GetComponent<MobsDisplay>().mobCard = carddraw;
            card.GetComponent<MobsDisplay>().isInHand = true;                
            card.transform.localScale = new Vector3(0.66f,0.66f,0);
        }
        else 
        {
            GameObject card = Instantiate(SpellCard, hand.transform.position, Quaternion.identity);
            card.transform.SetParent(hand.transform);
            card.GetComponent<MobsDisplay>().mobCard = carddraw;
            card.GetComponent<MobsDisplay>().isInHand = true;                
            card.transform.localScale = new Vector3(0.66f,0.66f,0);
        }
            
    }

    void Init_deck()
    {
        int number_of_copy = 2;
        for(int i = 0; i < number_of_copy; i++)
        {
            //for(int i1 =0;i1<100;i1++)
            AddCard(create_copy(card1));
            AddCard(create_copy(card2));
            AddCard(create_copy(card3));
            AddCard(create_copy(card4));
            AddCard(create_copy(card5));
            AddCard(create_copy(card6));
            AddCard(create_copy(card7));
            AddCard(create_copy(card8));
            AddCard(create_copy(card9));
            //AddCard(create_copy(card10));
            AddCard(create_copy(card11));
            AddCard(create_copy(card12));
            //AddCard(create_copy(card13));
            AddCard(create_copy(card14));
            AddCard(create_copy(card15));
            AddCard(create_copy(card16));
            //AddCard(create_copy(card17));
        }
        Shuffle();
    }

    Card create_copy(Card card)
    {

        Card copy = ScriptableObject.CreateInstance("Card") as Card;
        copy.cid = card.cid;
        copy.name = card.name;
        copy.desc = card.desc;
        copy.icon = card.icon;
        copy.cost = card.cost;
        copy.isAlly = card.isAlly;
        copy.health = card.health;
        copy.atkDamage = card.atkDamage;
        copy.id = card.id;
        copy.cardType = card.cardType;

        return copy;
    }    
}
