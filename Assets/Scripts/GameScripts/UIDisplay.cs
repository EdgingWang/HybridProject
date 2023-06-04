using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{

    [SerializeField]
    private GameObject UiPlayerLevel;

    [SerializeField]
    private GameObject UiXp;

    [SerializeField]
    private GameObject UiEnemyLevel;

    [SerializeField]
    private GameObject UiBuffRemaining;

    [SerializeField]
    private ProgressController progressController;

    private Text textPlayerLevel, textXp, textEnemyLevel, textBuffRemaining;

    // Start is called before the first frame update
    void Start()
    {
        //UiPlayerLevel.GetComponent<Text>().text = 
        textPlayerLevel = UiPlayerLevel.GetComponent<Text>();
        textXp = UiXp.GetComponent<Text>();
        textEnemyLevel = UiEnemyLevel.GetComponent<Text>();
        textBuffRemaining = UiBuffRemaining.GetComponent<Text>();

        //Debug.Log("was: " + textEnemyLevel.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (textPlayerLevel == null) return;

        //textPlayerLevel.text = progressController.playerLevel.ToString();
        textPlayerLevel.text = "Player Level " + progressController.playerLevel.ToString();
        textXp.text = (progressController.playerXP % 100).ToString() + " XP / 100";
        textEnemyLevel.text = "Enemy Level " + progressController.enemyLevel.ToString();

        if (textBuffRemaining == null) return;

        textBuffRemaining.text = progressController.xPBuffedFightsRemaining.ToString() + " Buffed Battle(s) Remaining";

        if (progressController.xPBuffedFightsRemaining >= 3)
            textBuffRemaining.color = Color.green;
        else if (progressController.xPBuffedFightsRemaining == 2)
            textBuffRemaining.color = Color.yellow;
        else if (progressController.xPBuffedFightsRemaining == 1)
            textBuffRemaining.color = Color.red;
        else
            textBuffRemaining.color = Color.black;

        //Debug.Log("update: " + textEnemyLevel.text);
    }
}
