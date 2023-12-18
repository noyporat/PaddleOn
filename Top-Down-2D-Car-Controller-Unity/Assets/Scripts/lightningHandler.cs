using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningHandler : MonoBehaviour
{
    public Sprite redLight, yellowLight, greenLight;

    public void setLightning(string color){
        if(string.Compare(color, "red") == 0){
            GetComponent<SpriteRenderer>().sprite = redLight;
        }else if(string.Compare(color, "green") == 0){
            GetComponent<SpriteRenderer>().sprite = greenLight;
        }else if(string.Compare(color, "yellow") == 0){
            GetComponent<SpriteRenderer>().sprite = yellowLight;
        }
    }
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
