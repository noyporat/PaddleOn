using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    public int lapsToComplete;

    bool isRaceCompleted = false;
    bool isFirstTime = true;

    public void Awake(){
        lapsToComplete  = MenuSceneScript.scene1.lapsNumber;
    }

    // public event Action<CarLapCounter> onPassCheckPoint;
    void OnTriggerEnter2D(Collider2D collider2D){

        if(isRaceCompleted){
            return;
        }
        if(isFirstTime){
            isFirstTime = false;
        }else{
            lapsToComplete--;
        }
            
        if(lapsToComplete <= 0){
            isRaceCompleted = true;
        }

        // onPassCheckPoint?.Invoke(this);
    }
}
