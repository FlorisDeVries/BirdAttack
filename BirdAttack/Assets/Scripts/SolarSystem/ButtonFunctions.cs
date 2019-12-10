using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    private bool paused = false;
    public GravitySystem gs;

    public void PauseToggle(Button button){
        if(paused){
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
            gs.unPauseGame();
            paused = false;
        } else {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Unpause";
            gs.pauseGame();
            paused = true;
        }
    }
}
