#include <CircularBuffer.h>
#include <ezButton.h>


//////joystick variabels
#define VRX_PIN  A1 // Arduino pin connected to VRX pin
#define VRY_PIN  A2 // Arduino pin connected to VRY pin
#define SW_PIN   2  // Arduino pin connected to SW  pin

ezButton button(SW_PIN);

int xValue = 0; // To store value of the X axis
int yValue = 0; // To store value of the Y axis
int bValue = 0; // To store value of the button
bool isPressedFlag = true;

////// fsr variables

int fsrPin = 0;     // the FSR and 10K pulldown are connected to a0
float fsrReading;     // the analog reading from the FSR resistor divider

float sum = -100.0;
bool firstSumFlag = true;


CircularBuffer<float,18> fivePaddles;

//variables to send to unity
//xValue
int isPressed = 0;
int fsrToUnity = 0;


void setup() {
  Serial.begin(9600);
  button.setDebounceTime(50); // set debounce time to 50 milliseconds
}

int counter = 0;
float stopFsr = 0.0;

void loop() {
  button.loop(); // MUST call the loop() function first
  //fsr reading data
  fsrReading = analogRead(fsrPin);
  stopFsr = fsrReading;

  // Serial.println(fsrReading);

  fsrReading /= 100.0f;

  if(fsrReading != 0.0){
    counter = 0;
    if(sum == -100.0){
      // Serial.println("start paddeling!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
      //TODO: ADD CLEAR TO THE BUFFER AND CHECK
      fivePaddles.clear();
      sum = 0.0;
      fsrToUnity = 1;
    }else if(sum > 20.0){
       fsrToUnity = 2;
    }else if(sum < 3){
      fsrToUnity = 0;
    }else{
      fsrToUnity = 1;
    }
    fivePaddles.unshift((float)fsrReading);
    // Serial.println("MOVE" + joystick);
    if(firstSumFlag){
      for(int i=0; i<fivePaddles.capacity; i++){
          sum += fivePaddles[i];
      }
      firstSumFlag = false;
    }else{
        sum -= fivePaddles.last();
        sum += fivePaddles.first();
    }
  }else{
    counter += 1;
    counter = counter % 10;

    if(counter == 0) {
          // Serial.println("STOP" + joystick);
          sum = -100.0;
          fsrToUnity = 0;
    }
  }

  // Serial.println(sum);

  

  //joystick reading & printing to unity
  //TODO: union of print to one line!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

  xValue = analogRead(VRX_PIN);
  yValue = analogRead(VRY_PIN);

  bValue = button.getState();

  if (button.isPressed() && isPressedFlag) {
    isPressed = 1;
    isPressedFlag = false;
    // TODO do something here
  }

  if (button.isReleased()) {
    isPressed = 0;
    isPressedFlag = true;
    // TODO do something here
  }

  Serial.print(map(xValue, 1023, 0, 100, -100));
  Serial.print(",");
  Serial.print(isPressed);
  Serial.print(",");
  Serial.println(fsrToUnity);

  isPressed = 0;

  delay(60);

}
