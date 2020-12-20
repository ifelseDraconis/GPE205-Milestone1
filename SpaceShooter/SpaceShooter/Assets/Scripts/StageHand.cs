using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageHand : MonoBehaviour
{
    public static StageHand thisHand = null;
    public string[] theseScenes;

    void Awake()
    {
        if (thisHand == null)
        {
            thisHand = this;
        }
        else if (thisHand != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene(theseScenes[0]);
        GameManager.thisManager.thisScene = GameManager.CurrentScene.StartScreen;
        getSaveData();
    }

    public void LoadLevel()
    {
        
        SceneManager.LoadScene(theseScenes[1]);
        GameManager.thisManager.StartGame();
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(theseScenes[2]);
        GameManager.thisManager.thisScene = GameManager.CurrentScene.GameOverScreen;
    }

    public void getSaveData()
    {
        if (PlayerPrefs.GetInt("PlayerCount") == 1)
        {
            GameManager.thisManager.selectedPlayers = GameManager.playerCount.OnePlayer;
        }
        else
        {
            GameManager.thisManager.selectedPlayers = GameManager.playerCount.TwoPlayers;
        }
        GameManager.thisManager.MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        GameManager.thisManager.MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        GameManager.thisManager.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        MapMaker.thisMapMaker.onNumber = PlayerPrefs.GetInt("SeedNumber");
        MapMaker.thisMapMaker.BasedByDay = intToBool(PlayerPrefs.GetInt("DayOfWeek"));
    }

    // saves the current save data, double saving the current high score data by force
    public void makeSaveData(int thesePlayers, float thisMasterVolume, float thisMusicVolume, float thisSFXVolume, bool thisDaySeed, int seedNumber)
    {
        PlayerPrefs.SetInt("PlayerCount", thesePlayers);
        PlayerPrefs.SetFloat("MasterVolume", thisMasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", thisMusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", thisSFXVolume);
        PlayerPrefs.SetInt("DayOfWeek", boolToInt(thisDaySeed));
        PlayerPrefs.SetInt("SeedNumber", seedNumber);
        PlayerPrefs.Save();
    }

    // does a simple check to see if your current high score displaces the old high score
    public void setNewHighScore(int incomingHighScore)
    {
        if (incomingHighScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", incomingHighScore);
            PlayerPrefs.Save();
        }
    }

    bool intToBool(int thisInt)
    {
        if ( thisInt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int boolToInt(bool thisBool)
    {
        if (thisBool)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
