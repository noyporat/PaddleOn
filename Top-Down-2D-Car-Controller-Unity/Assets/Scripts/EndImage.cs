using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndImage : MonoBehaviour
{

    CarLapCounter counterObj;
    [SerializeField] RawImage showImage;
    // Start is called before the first frame update
    void Start()
    {
        showImage.enabled = false;
        GameObject checkPointObj = GameObject.FindWithTag("checkPoint");
 
        if(checkPointObj != null)
        {
            counterObj = checkPointObj.GetComponent<CarLapCounter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(counterObj.lapsToComplete == 0){
            showImage.enabled = true;
        }
    }
}
