using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameManager GameManager;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isOverCard = false;
    private bool isDraggable = true;
    private bool isSpellCard = false;
    private Vector2 startPosition;
    private GameObject dropZone;
    private GameObject startParent;
    private GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6 && !isSpellCard)
        {
            isOverDropZone = true;
            isOverCard = false;
            dropZone = collision.gameObject;
            targetObject = null;
        }
        else if(collision.gameObject.layer == 6 && !isSpellCard)
        {
            if (!isOverDropZone)
            {
                isOverCard = false;
                dropZone = null;
                targetObject = null;
            }
        }
        else if (collision.gameObject.layer == 6 && isSpellCard)
        {
            isOverDropZone = false;
            isOverCard = true;
            dropZone = null;
            targetObject = collision.gameObject;
        }
        else
        {
            isOverDropZone = false;
            isOverCard = false;
            dropZone = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6 && !isSpellCard)
        {
            isOverDropZone = false;
            dropZone = null;
        }
        else if (collision.gameObject.layer == 6 && isSpellCard)
        {
            isOverCard = false;
        }
    }

    public void StartDrag()
    {
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
        isSpellCard = IsSpellCard(gameObject);
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;

        if (isOverDropZone)
        {
            if ((!isSpellCard && GameManager.isCardListFull())||!IsManaEnough(gameObject))
            {
                Debug.Log("DropZone is full! OR Mana is not enough!");
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
            }
            else
            {
                if (dropZone != null)
                    transform.SetParent(dropZone.transform, false);
                isDraggable = false;
                GameManager.PlayCard(gameObject, targetObject);
            }
        }
        else if (isOverCard)
        {
            if (!IsManaEnough(gameObject))
            {
                Debug.Log("Mana is not enough!");
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
            }
            else
                GameManager.PlayCard(gameObject, targetObject);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    public bool IsSpellCard(GameObject g)
    {
        Card c = g.transform.GetComponent<MobsDisplay>().GetCard();
        return c.cardType == 1;
    }

    public bool IsManaEnough(GameObject g)
    {
        Card c = g.transform.GetComponent<MobsDisplay>().GetCard();
        return GameManager.PlayerManager.IsManaEnough(c.cost);
    }
}
