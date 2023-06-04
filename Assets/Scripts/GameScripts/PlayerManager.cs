using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int playerHp;
    public int playerMana;
    private Text healthText = null;
    private Text manaText = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay();
    }


    public void GetAtk(int atkValue)
    {
        playerHp -= atkValue;
        UpdateDisplay();
    }

    public void ResetPlayerHp(int hp)
    {
        playerHp = hp;
        UpdateDisplay();
    }

    public void ResetPlayerMana(int mana)
    {
        playerMana = mana;
        UpdateDisplay();
    }

    public void GetObjects()
    {
        if (healthText == null)
            healthText = GameObject.Find("PlayerHp").GetComponent<Text>();

        if (manaText == null)
            manaText = GameObject.Find("PlayerMana").GetComponent<Text>();
    }

    public void GetManaObject()
    {
        if (manaText == null)
            manaText = GameObject.Find("PlayerMana").GetComponent<Text>();
    }

    public void UpdateDisplay()
    {
        healthText.text = playerHp.ToString();
        manaText.text = playerMana.ToString();
    }

    public void ConsumeMana(int consumeValue)
    {
        playerMana -= consumeValue;
        UpdateDisplay();
    }

    public int GetPlayerManaValue()
    {
        return playerMana;
    }

    public int GetPlayerHpValue()
    {
        return playerHp;
    }

    public bool IsManaEnough(int mana)
    {
        return playerMana - mana >= 0;
    }
}
