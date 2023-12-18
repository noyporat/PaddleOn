using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounterText : MonoBehaviour
{
    Text counterText;
    CarLapCounter counterObj;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>();

        GameObject checkPointObj = GameObject.FindWithTag("checkPoint");
 
        if(checkPointObj != null)
        {
            counterObj = checkPointObj.GetComponent<CarLapCounter>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        string counterStr = counterObj.lapsToComplete.ToString();
        
        string prefixStr = "Laps Left: ";

        counterText.text = string.Concat(prefixStr, counterStr);

    }
}
