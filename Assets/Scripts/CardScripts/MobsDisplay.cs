using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobsDisplay : MonoBehaviour
{
    public Card mobCard;
    // Card mobInstance;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Image frameFrame;

    public Text healthText;
    public Text costText;
    public Text atkText;

    public Sprite allyCardFrame;
    public Sprite enemyCardFrame;
    public Sprite spellCardFrame;

    public bool isInHand = false;

    public float handCardScale = 0.5f;
    public float fieldCardScale = 1.0f;
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        if (isInHand)
        {
            this.GetComponent<RectTransform>().localScale = new Vector2(handCardScale, handCardScale);
        }
        UpdateCardDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        if (mobCard.cardType == 0 && mobCard.health<=0)
        {
            Destroy(gameObject);
        }
        nameText.text = mobCard.name;
        costText.text = mobCard.cost.ToString();
        artworkImage.sprite = mobCard.icon;

        if (mobCard.cardType == 1)
        {
            frameFrame.GetComponent<Image>().sprite = spellCardFrame;
            descriptionText.text = mobCard.desc;
            healthText.text = " ";
            atkText.text = " ";
        }
        else
        {
            descriptionText.text = " ";
            if (mobCard.isAlly)
            {
                frameFrame.GetComponent<Image>().sprite = allyCardFrame;
            }
            else
            {
                frameFrame.GetComponent<Image>().sprite = enemyCardFrame;
                costText.text = " ";
            }
            
            healthText.text = mobCard.health.ToString();
            atkText.text = mobCard.atkDamage.ToString();
        }
        if (!isInHand)
        {
            this.GetComponent<RectTransform>().localScale = new Vector2(fieldCardScale, fieldCardScale);
            SetInhand(true);
        }
    }

    public void SetCard(Card c)
    {
        mobCard = c;
    }

    public Card GetCard()
    {
        return mobCard;
    }

    public void SetInhand(bool isInhand)
    {
        isInHand = isInhand;
    }
}
