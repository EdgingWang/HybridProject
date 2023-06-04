using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    private Button button;
    public EnemyController EnemyController;
    public static bool hasGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        hasGameStarted = false;
        button = this.GetComponent<Button>();
        button.onClick.AddListener(() => switchScene());
    }

    private void switchScene()
    {
        SceneManager.LoadScene("Scenes/Combat");
        hasGameStarted = true;
    }
}
