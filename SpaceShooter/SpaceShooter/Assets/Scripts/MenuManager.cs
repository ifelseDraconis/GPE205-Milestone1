using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager thisMenuHandler = null;
    public GameManager myGameManager;

    public enum CurrentMenu { MainMenu, OptionsMenu, HighScores}
    public CurrentMenu thisMenu;
    public bool changeMenu = false;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject highScore;

    void Awake()
    {
        if (thisMenuHandler == null)
        {
            thisMenuHandler = this;
        }
        else if (thisMenuHandler != null)
        {
            Destroy(gameObject);
        }

        setMainMenu();
        myGameManager = GameManager.thisManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeMenu)
        {
            switch (thisMenu)
            {
                case CurrentMenu.MainMenu:
                    setMainMenu();
                    break;

                case CurrentMenu.OptionsMenu:
                    setOptionsMenu();
                    break;

                case CurrentMenu.HighScores:
                    setHighScoreDisplay();
                    break;

            }

            changeMenu = false;
        }
        
    }

    void setMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        highScore.SetActive(false);
    }

    void setOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        highScore.SetActive(false);

        if (myGameManager == null)
        {
            myGameManager = GameManager.thisManager;            
        }
        // this loads the playerprefs
        StageHand.thisHand.getSaveData();

        // sets the options with playerpref values
        GameObject MasterVolumeSlider = GameObject.FindWithTag("MasterVolumeSlider");
        MasterVolumeSlider.GetComponent<Slider>().value = myGameManager.MasterVolume;
        GameObject MusicVolumeSlider = GameObject.FindWithTag("MusicVolumeSlider");
        MusicVolumeSlider.GetComponent<Slider>().value = myGameManager.MusicVolume;
        GameObject SFXVolumeSlider = GameObject.FindWithTag("SFXVolumeSlider");
        SFXVolumeSlider.GetComponent<Slider>().value = myGameManager.SFXVolume;
        GameObject PlayerSlider = GameObject.FindWithTag("PlayerSlider");
        if (myGameManager.selectedPlayers == GameManager.playerCount.OnePlayer)
        {
            PlayerSlider.GetComponent<Slider>().value = 1;
        }
        else
        {
            PlayerSlider.GetComponent<Slider>().value = 2;
        }
        GameObject DayOfWeekMod = GameObject.FindWithTag("DayOfWeekMod");
        DayOfWeekMod.GetComponent<Toggle>().isOn = MapMaker.thisMapMaker.BasedByDay;
        GameObject SeedNumber = GameObject.FindWithTag("SeedNumber");
        SeedNumber.GetComponent<Slider>().value = MapMaker.thisMapMaker.onNumber;
        
    }

    // saves the player pref values
    public void saveOptions()
    {
        if (myGameManager == null)
        {
            myGameManager = GameManager.thisManager;
        }
        StageHand.thisHand.getSaveData();
        GameObject MasterVolumeSlider = GameObject.FindWithTag("MasterVolumeSlider");
        GameObject MusicVolumeSlider = GameObject.FindWithTag("MusicVolumeSlider");
        GameObject SFXVolumeSlider = GameObject.FindWithTag("SFXVolumeSlider");
        GameObject PlayerSlider = GameObject.FindWithTag("PlayerSlider");
        GameObject DayOfWeekMod = GameObject.FindWithTag("DayOfWeekMod");
        GameObject SeedNumber = GameObject.FindWithTag("SeedNumber");

        // create a float for the current value of the game volume
        float thisMasterVolume = MasterVolumeSlider.GetComponent<Slider>().value;
        myGameManager.MasterVolume = thisMasterVolume;

        // create a float for the current value of the music volume in the game
        float thisMusicVolume = MusicVolumeSlider.GetComponent<Slider>().value;
        myGameManager.MusicVolume = thisMusicVolume;


        float thisSFXVolume = SFXVolumeSlider.GetComponent<Slider>().value;
        myGameManager.SFXVolume = thisSFXVolume;

        int playerCount;
        if (PlayerSlider.GetComponent<Slider>().value == 1)
        {
            myGameManager.selectedPlayers = GameManager.playerCount.OnePlayer;
            playerCount = 1;
        }
        else
        {
            myGameManager.selectedPlayers = GameManager.playerCount.TwoPlayers;
            playerCount = 2;
        }
        bool thisDayofWeekMod = DayOfWeekMod.GetComponent<Toggle>().isOn;
        MapMaker.thisMapMaker.BasedByDay = thisDayofWeekMod;

        int thisSeedNumber = (int)SeedNumber.GetComponent<Slider>().value;
        MapMaker.thisMapMaker.onNumber = thisSeedNumber;

        // save to player pref file
        StageHand.thisHand.makeSaveData(playerCount, thisMasterVolume, thisMusicVolume, thisSFXVolume, thisDayofWeekMod, thisSeedNumber);
    }

    void setHighScoreDisplay()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        highScore.SetActive(true);
    }

    public void ConcealAll()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);        
        highScore.SetActive(false);
    }

    public void AlterMenu(CurrentMenu newMenu)
    {
        thisMenu = newMenu;
        changeMenu = true;
    }

    bool intToBool(int thisInt)
    {
        if (thisInt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
