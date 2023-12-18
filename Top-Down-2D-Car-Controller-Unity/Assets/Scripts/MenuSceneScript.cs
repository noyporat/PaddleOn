using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneScript : MonoBehaviour
{
    public static MenuSceneScript scene1;
    public InputField inputField;
    public int lapsNumber;

    private void Awake(){
        scene1 = this;
    }

    public void PlayGame(){
        lapsNumber = int.Parse(inputField.text);
        SceneManager.LoadScene("SampleScene");
    }
}
