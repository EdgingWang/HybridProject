using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameManager GameManager;
    public Card mobCard1;
    public Card mobCard2;
    public Card mobCard3;
    public Card mobCard4;
    public Card mobCard5;
    private bool hasEnemySpawn = false;

    private List<Card> MobCards = new List<Card>();

    public GameObject EnemySlot1;
    public GameObject EnemySlot2;
    public GameObject EnemySlot3;
    public GameObject EnemySlot4;
    public GameObject EnemySlot5;

    private int EnemyTypeCounts = 5; 
    private int EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        MobCards.Add(mobCard1);
        MobCards.Add(mobCard2);
        MobCards.Add(mobCard3);
        MobCards.Add(mobCard4);
        MobCards.Add(mobCard5);
        GameManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEnemySpawn && PlayButtonScript.hasGameStarted)
        {
            Debug.Log("Game Started!");
            SpawnEnemies();
            hasEnemySpawn = true;
        }
        UpdateCards();
    }

    public void UpdateCards()
    {
        CheckListBoundary(EnemySlot1, 0);
        CheckListBoundary(EnemySlot2, 1);
        CheckListBoundary(EnemySlot3, 2);
        CheckListBoundary(EnemySlot4, 3);
        CheckListBoundary(EnemySlot5, 4);
    }

    public void CheckListBoundary(GameObject enemySlot, int index){
        if (GameManager.EnemyCards.Count > index)
        {
            enemySlot.transform.GetComponent<MobsDisplay>().SetCard(GameManager.EnemyCards[index]);
        }
        else
        {
            if (enemySlot != null)
                Destroy(GameObject.Find(enemySlot.name));
        }
    }

    public Card selectCardType(int type)
    { 
        switch (type)
        {
            case 0: return mobCard1;
            case 1: return mobCard2;
            case 2: return mobCard3;
            case 3: return mobCard4;
            case 4: return mobCard5;
            default: return mobCard1;
        }
    }

    public void SpawnFunc(int type, int number)
    {
        GameManager.EnemyCards.Clear();
        for(int i = 0; i < number; i++)
        {
            GameManager.EnemyCards.Add(Object.Instantiate(selectCardType(type)));
        }
    }

    public void SpawnEnemies()
    {
        EnemyType = Random.Range(1, EnemyTypeCounts) - 1;
        SpawnFunc(EnemyType, 5);
    }
}
