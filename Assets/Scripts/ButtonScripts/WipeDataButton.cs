using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WipeDataButton : MonoBehaviour
{
    private Button button;

    [SerializeField]
    private ProgressController pc;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(() => pc.purgePersistent());
    }
}
