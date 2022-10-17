using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSaveData : MonoBehaviour
{
    private OurPlayerData OurData = new OurPlayerData();

    private float currentTime = GameManager.getTimer; //Hopefully it works like this
    private GameObject currentFilm = GameManager.polaroidImage;
    private GameObject[] notesSaved = GameManager.Notes;

    //for load button UI
    public Button[] loadButtons;

    private static Watch watch;

    private void Awake()
    {
        ///Ads the event from GM where we call this function to happen every hour
        ///we then call the AutoSave function from here
        GameManager.OnHourChanged.AddListener(AutoSave); //this gets nullreferenced

        if (watch && watch != this)
        {
            //if another instance of this object exists destroy this one
            Destroy(gameObject);
            return;
        }

        //watch = ; //this is the active instance of the object

        //lets not destroy this object when a new scene is loaded
        DontDestroyOnLoad(gameObject);

        //attach a callback for every new scene that is loaded
        //it is fine to remove a callback that wasn't added so far
        //this makes sure that this callback is definitely only added once
        SceneManager.sceneLoaded -= watch.OnSceneLoaded;
        SceneManager.sceneLoaded += watch.OnSceneLoaded;
    }

    private void Start()
    {
        currentFilm = GameObject.FindGameObjectWithTag("Film");
        Debug.Log("Film tag has been located");
        notesSaved = GameObject.FindGameObjectsWithTag("Save");
        Debug.Log("The tag save has been loacted");
    }

    // Update is called once per frame
    void Update()
    {
        OurData.CurrentTime = currentTime;
        OurData.CurrentFilm = currentFilm;
        OurData.NotesSaved = notesSaved;

        #region SAVE TESTING
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
            SaveGameManager.SaveGame();
            Debug.Log("SaveGame has saved");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SaveGameManager.LoadGame();
            OurData = SaveGameManager.CurrentSaveData.OurPlayerData;
            currentTime = OurData.CurrentTime;
            Debug.Log("LoadGame has loaded the savefile");
        }
        #endregion
    }

    /// <summary>
    /// Saves the player data and overwrites the SaveGame file
    /// this AutoSave is run every ingame hour or when game is quit
    /// </summary>
    public void AutoSave()
    {
        SaveGameManager.CurrentSaveData.OurPlayerData = OurData;
        SaveGameManager.SaveGame();
        Debug.Log("SaveGame has saved");
    }

    public void LoadSaveFile() //this is used on the load button in the main menu scene
    {
        currentTime += Time.deltaTime; //makes so that the times goes forward
        /* timer * 10 = around 5 seconds per ingame min
         * timer * 20 = around 2 seconds per ingame min
         * timer * 40 = around 1 second per ingame min*/
        int min = (int)currentTime * 8 / 60 % 60; //ingame minutes
        int hour = (int)currentTime * 8 / 3600 % 24; //ingame hours

        //SaveGameManager.LoadGame(); //calls the load game function from the SaveGameManager.cs

        ///Checks if the time and file corresponds
        ///if it does it enables the button and hopefully
        ///the game will load it at the right time
        //scuffed way of checking if there is a savefile from time
        #region LOAD SAVE AFTER SPECIFIC HOURS
        if (hour == 0)
        {
            /*when clicking the load button the panel with the hourly ones should appear
             * so checking if the button is enabled is just an extra step to ensure
             * that the right thing happens to the right buttons etc etc*/
            if (loadButtons[0].enabled == true)
            { 
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2); //test if this actually sends the player to the game scene
                Debug.Log("loading hour 1");
                Debug.Log(hour + ":" + min);

                //resets the timer
                watch.OnSceneLoaded(SceneManager.GetSceneByBuildIndex(2), LoadSceneMode.Single);
            }
        }
        else if(hour == 1)
        {
            if(loadButtons[1].enabled == true)
            {
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2);
                Debug.Log("loading hour 2");
                Debug.Log(hour + ":" + min);
            }
        }
        else if(hour == 2)
        {
            if(loadButtons[2].enabled == true)
            {
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2);
                Debug.Log("loading hour 3");
                Debug.Log(hour + ":" + min);
            }
        }
        else if(hour == 3)
        {
            if(loadButtons[3].enabled == true)
            {
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2);
                Debug.Log("loading hour 4");
                Debug.Log(hour + ":" + min);
            }
        }
        else if(hour == 4)
        {
            if(loadButtons[4].enabled == true)
            {
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2);
                Debug.Log("loading hour 5");
                Debug.Log(hour + ":" + min);
            }
        }
        else if (hour == 5)
        {
            if (loadButtons[5].enabled == true)
            {
                SaveGameManager.LoadGame();
                SceneManager.LoadScene(2);
                Debug.Log(hour + ":" + min);
            }
        }
        else
        {
            Debug.Log("no save file found and cannot be loaded");
        }
        #endregion

    }

    /// <summary>
    /// When the player exits the game it will autosave the game
    /// </summary>
    void OnApplicationQuit() //Needs to be tested
    {
        AutoSave();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}

/// <summary>
/// This is where you put the stuff needed to be saved on the player
/// (just make sure that if stuff needs to be linked from somewhere else
/// to make sure it actually does work)
/// </summary>
[System.Serializable]
public struct OurPlayerData
{
    public float CurrentTime;
    public GameObject CurrentFilm;
    public GameObject[] NotesSaved;
}
