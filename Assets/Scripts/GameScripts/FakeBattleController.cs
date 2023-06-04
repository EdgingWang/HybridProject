using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBattleController : MonoBehaviour
{

    [SerializeField]
    private ProgressController progressController;

    // Start is called before the first frame update
    void Start()
    {
        print("henlo");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            print("is win");
            progressController.onBattleSuccess();
        }
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            print("is lose");
            progressController.onBattleFailure();
        }*/
    }
}
