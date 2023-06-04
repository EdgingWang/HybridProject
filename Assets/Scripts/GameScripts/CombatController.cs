using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public MobsDisplay selectedDisplay { get; private set; }

    private Vector2 screenPosition;

    private Vector3 worldPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
            Debug.Log("touched screenpos: " + screenPosition.x + ", " + screenPosition.y);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
            Debug.Log("clicked screenpos: " + screenPosition.x + ", " + screenPosition.y);
        }
        else return;

        //worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 100f));
        Debug.Log("calculated worldpos: " + worldPosition.x + ", " + worldPosition.y);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("pressed " + hit.collider.transform.name);

            if (hit.collider.transform.TryGetComponent(out MobsDisplay display))
            {
                //if (display.isInHand)
                //{
                //    selectedDisplay = display;
                //    Debug.Log("most recently selected hand card: " + display.name);
                //}
            }
        }
    }
}
