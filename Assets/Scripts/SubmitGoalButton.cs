using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitGoalButton : MonoBehaviour
{
    private Button button;

    [SerializeField]
    private TMPro.TMP_InputField input;

    [SerializeField]
    private ProgressController progressController;

    [SerializeField]
    private bool isJJ;

    private bool inputValueSet;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(() => submitGoal());
    }

    private void Update()
    {
        if (inputValueSet) return;

        if (isJJ && input.text.ToString() != progressController.jumpingJacksGoal.ToString())
        {
            inputValueSet = true;
            input.text = progressController.jumpingJacksGoal.ToString();
        }
        else if (!isJJ && input.text.ToString() != progressController.cyclingGoal.ToString())
        {
            inputValueSet = true;
            input.text = progressController.cyclingGoal.ToString();
        }
    }

    private void submitGoal()
    {
        int goal;
        if (int.TryParse(input.text, out goal)) {
            if (isJJ)
                progressController.submitJJGoal(goal);
            else
                progressController.submitCyclingGoal(goal);
        }
    }
}
