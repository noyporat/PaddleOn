using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class CarInputHandler : MonoBehaviour
{
    //Components
    TopDownCarController topDownCarController;
    SerialPort seri = new SerialPort("COM3", 9600);
    float horizontalSpeed = 0f;
    float verticalSpeed = 0f;
    float tempVertSpeed = 0.3f;
    float fsrMode = 0;
    float isJoystickPressed = 0;

    lightningHandler lightning;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        GameObject redLightObj = GameObject.FindWithTag("redLight");
 
        if(redLightObj != null)
        {
            lightning = redLightObj.GetComponent<lightningHandler>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        seri.Open();
        StartCoroutine(ReadDatatFromSerialPort());
    }

    IEnumerator ReadDatatFromSerialPort(){
        while(true){
            string[] value = seri.ReadLine().Split(',');
            horizontalSpeed = (float.Parse(value[0]))/ 100f;
            isJoystickPressed = (float.Parse(value[1]));
            fsrMode = (float.Parse(value[2]));

            yield return new WaitForSeconds (.05f);
        }
        
    }

    // Update is called once per frame and is frame dependent
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if(isJoystickPressed == 1){
            tempVertSpeed = -1 * tempVertSpeed;
        }

        if(fsrMode == 0 || fsrMode == 2){
            verticalSpeed = 0;
        }else if(fsrMode == 1){
            verticalSpeed = tempVertSpeed;
        }

        inputVector.x = horizontalSpeed;
        inputVector.y = verticalSpeed;

        //Send the input to the car controller.
        topDownCarController.SetInputVector(inputVector);

        //lights rendering
        if(fsrMode == 0){
            lightning.setLightning("red");
        }else if(fsrMode == 1){
            lightning.setLightning("green");
        }else if(fsrMode == 2){
            lightning.setLightning("yellow");;
        }
        
    }
}
