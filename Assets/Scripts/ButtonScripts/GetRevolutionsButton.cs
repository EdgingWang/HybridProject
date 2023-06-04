using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRevolutionsButton : MonoBehaviour
{
    private Button button;

    [SerializeField]
    private WebClient wc;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(() => wc.getRevolutions());
    }
}
