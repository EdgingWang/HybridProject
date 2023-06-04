using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTurn : MonoBehaviour
{
    public GameManager GameManager;
    public PlayerManager PlayerManager;

    void Start()
    {
    }

    public void OnClick()
    {
        GameManager.TurnAtk();
        GameManager.IsPlayerTurn = false;
        PlayerManager.ResetPlayerMana(10);
    }
}
