using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject MainMenuPanel, LevelSelectPanel, ScenarioPanel, SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu(){
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        ScenarioPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void GoToLevelSelect(){
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
        ScenarioPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void GoToScenarios(){
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
        ScenarioPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void GoToSettings(){
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
        ScenarioPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadLevel(string level){
        print($"Loading scene {level}");
        SceneManager.LoadScene(level);
    }
}
