using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JJExercise : MonoBehaviour
{
    private Button startStopButton;
    private Button exitButton;

    private ProgressController progressController;

    private Text textTimer;
    private float floatTimer;

    private bool isTimerRunning;

    // Start is called before the first frame update
    void Start()
    {
        startStopButton = GameObject.Find("BtnStartStop").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        progressController = GameObject.Find("ProgressController").GetComponent<ProgressController>();
        textTimer = GameObject.Find("Timer").GetComponent<Text>();

        startStopButton.onClick.AddListener(() => onStartStopButtonPressed());
        exitButton.onClick.AddListener(() => onExitButtonPressed());

        isTimerRunning = false;
    }

    private void onStartStopButtonPressed()
    {
        isTimerRunning = !isTimerRunning;

        if (isTimerRunning)
            startStopButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Stop";
        else
        {
            startStopButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Start";
            //progressController
        }
    }

    private void onExitButtonPressed()
    {
        SceneManager.LoadScene("Scenes/CyclingScene");

        progressController.processJJTime((int) floatTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            floatTimer += Time.deltaTime;
            textTimer.text = floatTimer.ToString("0.00") + "s";
        }
    }


}
