using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelButtons : MonoBehaviour
{
    public Button[] levelButtons;
    public GameObject[] buttonHolders;
    // Start is called before the first frame update
    void Start()
    {
        int levelsCleared = LevelSaving.LevelsCleared();
        for(int i = 0; i < levelsCleared; i++){
            levelButtons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
