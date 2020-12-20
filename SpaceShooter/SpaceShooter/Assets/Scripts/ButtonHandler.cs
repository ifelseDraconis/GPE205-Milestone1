using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonHandler : MonoBehaviour
{
    public GameManager myGameManager;
    public AudioClip clickSound;

    void Awake()
    {
        
    }

    void Start()
    {
        LoadOptions();
    }

    public void StartGame()
    {
        SoundManager.thisSoundManager.Play(clickSound, GameManager.thisManager.SFXVolume * GameManager.thisManager.MasterVolume);
        // code to launch the level
        MenuManager.thisMenuHandler.ConcealAll();
        StageHand.thisHand.LoadLevel();
    }

    public void ShowHighScore()
    {
        SoundManager.thisSoundManager.Play(clickSound, GameManager.thisManager.SFXVolume * GameManager.thisManager.MasterVolume);
        // code to display the high scores screen
        MenuManager.thisMenuHandler.AlterMenu(MenuManager.CurrentMenu.HighScores);
    }

    public void ShowOptions()
    {
        SoundManager.thisSoundManager.Play(clickSound, GameManager.thisManager.SFXVolume * GameManager.thisManager.MasterVolume);
        // code to display the options menu
        MenuManager.thisMenuHandler.AlterMenu(MenuManager.CurrentMenu.OptionsMenu);
    }

    public void ReturnToMain()
    {
        SoundManager.thisSoundManager.Play(clickSound, GameManager.thisManager.SFXVolume * GameManager.thisManager.MasterVolume);
        // code to return the menu to the main button screen
        MenuManager.thisMenuHandler.AlterMenu(MenuManager.CurrentMenu.MainMenu);
        updateSoundData();
    }

    public void ReturnToStart()
    {
        StageHand.thisHand.setNewHighScore(GameManager.thisManager.PlayerScore);
        GameManager.thisManager.RestartGame();
    }

    public void updateSoundData()
    {
        SoundManager.thisSoundManager.Play(clickSound, GameManager.thisManager.SFXVolume * GameManager.thisManager.MasterVolume);
        if (myGameManager == null)
        {
            setManager();
        }
        GameObject MasterVolumeSlider = GameObject.FindWithTag("MasterVolumeSlider");
        myGameManager.MasterVolume = MasterVolumeSlider.GetComponent<Slider>().value;
        GameObject MusicVolumeSlider = GameObject.FindWithTag("MusicVolumeSlider");
        myGameManager.MusicVolume = MusicVolumeSlider.GetComponent<Slider>().value;
        GameObject SFXVolumeSlider = GameObject.FindWithTag("SFXVolumeSlider");
        myGameManager.SFXVolume = SFXVolumeSlider.GetComponent<Slider>().value;
        GameObject PlayerSlider = GameObject.FindWithTag("PlayerSlider");
        int thisChoice = (int)PlayerSlider.GetComponent<Slider>().value;
        if (thisChoice == 1)
        {
            myGameManager.selectedPlayers = GameManager.playerCount.OnePlayer;
        }
        else
        {
            myGameManager.selectedPlayers = GameManager.playerCount.TwoPlayers;
        }
        


        GameManager.thisManager.playIntroMusics();
    }

    void LoadOptions()
    {
        StageHand.thisHand.getSaveData();
    }

    // exit the game during a built game
    public void quitGame()
    {
        Application.Quit();
    }

    void setManager()
    {
        myGameManager = GameManager.thisManager;
    }
}
