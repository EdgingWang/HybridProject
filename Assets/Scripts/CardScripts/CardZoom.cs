using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;

    private GameObject zoomCard;

    public int cardScale = 2;

    public void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void OnHoverEnter()
    {
        if (gameObject.transform.parent.name == "DropZone")
            return;

        float xpos, ypos;
        if (Input.touchCount > 0)
        {
            xpos = Input.GetTouch(0).position.x;
            ypos = Input.GetTouch(0).position.y + 500;
        } else
        {
            xpos = Input.mousePosition.x;
            ypos = Input.mousePosition.y + 350;
        }

        zoomCard = Instantiate(gameObject, new Vector2(xpos, ypos), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, true);
        //zoomCard.layer = LayerMask.NameToLayer("Zoom");

        float height = zoomCard.GetComponent<RectTransform>().rect.height;
        float width = zoomCard.GetComponent<RectTransform>().rect.width;
        zoomCard.GetComponent<RectTransform>().sizeDelta = new Vector2(width*cardScale, height*cardScale);

        Component[] components;
        components = zoomCard.GetComponentsInChildren<Text>();
        foreach(Text t in components)
        {
            t.fontSize *= cardScale;

            if(t.name == "Name")
                t.transform.localPosition = new Vector3(t.transform.localPosition.x, -185, t.transform.localPosition.z);

            else if (t.name == "Health")
            {
                t.transform.localPosition = new Vector3(-153, -296, t.transform.localPosition.z);
            }

            else if (t.name == "Mana")
            {
                if(!(zoomCard.GetComponent<MobsDisplay>().mobCard.cardType == 1))
                    t.transform.localPosition = new Vector3(t.transform.localPosition.x, -296, t.transform.localPosition.z);
                else
                    t.transform.localPosition = new Vector3(214, -174, t.transform.localPosition.z);
            }

            else if (t.name == "AtkDmg")
            {
                t.transform.localPosition = new Vector3(153, -296, t.transform.localPosition.z);
            }

            else if (t.name == "Description")
            {
                t.transform.localPosition = new Vector3(t.transform.localPosition.x, -264, t.transform.localPosition.z);
            }

            if (zoomCard.GetComponent<MobsDisplay>().mobCard.cardType == 1)
            {
                t.color = Color.black;
            }
        }
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
