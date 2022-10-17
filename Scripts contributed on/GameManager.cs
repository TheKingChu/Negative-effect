using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int film, SceneToGoTo;
    public GameObject VRheadset;
    public GameObject[] spawnPoints;

    [Header("Gameobjects")]
    public GameObject Player;
    public GameObject MonsterSpawn;
    private GameObject timeSp;

    public bool isCreated;
    private bool playedHoursound;

    //Camera reloading
    public bool reloaded,reloadReady;
    
    /// <summary>
    /// To be able to trigger time specific events
    /// like saving the game each hour and having the monsters behave differently 
    /// every half an hour (ingame time) etc...
    /// </summary>
    public static UnityEvent OnMinuteChanged;
    public static UnityEvent OnHourChanged;

    //timer and saving stuff
    public bool SaveGame = false;
    [Header("Time")]
    static float timer; //local time
    private float minuteToRealTime; //every half a sec realtime is 1minute ingame (needs to be changed obvs, but this is for testing)

    public static float getTimer => timer;

    int previousHour;

    public static GameObject polaroidImage; //Takes the prefab (hopefully) so that it can be used in other scripts
    public static GameObject[] Notes; //Takes the prefab of the items (hopefully) and makes it possible to save
    public GameObject[] hourlyObjects;
    public GameObject hourChangeCanvas;


    // Start is called before the first frame update
    private void Awake()
    {
        timeSp = GameObject.FindGameObjectWithTag("HourUI");
        Player = GameObject.FindGameObjectWithTag("Player");
        MonsterSpawn = GameObject.FindGameObjectWithTag("MonsterSpawn");
        Notes = GameObject.FindGameObjectsWithTag("Notes");
    }
    void Start()
    {
        timeSp.GetComponent<TimeSwap>().hour(Player.transform);

        MonsterSpawn.SetActive(false);
        #region Camera
        // ReloadChecks for the camera
        
        reloadReady = true;
        reloaded = false;
        film = 0;
        #endregion
        
        //game starts at 00:00am
        timer = minuteToRealTime;

       DontDestroyOnLoad(this); 
       // DontDestroyOnLoad(VRheadset);
       if(GameObject.Find("__GM").activeInHierarchy == true)
       {
           
       }

    
    }


    private void Update()
    {
        timer += Time.deltaTime; //makes so that the times goes forward
        /* timer * 10 = around 5 seconds per ingame min
         * timer * 20 = around 2 seconds per ingame min
         * timer * 40 = around 1 second per ingame min*/
        int min = (int)timer * 12 / 60 % 60; //ingame minutes
        int hour = (int)timer * 12 / 3600 % 24; //ingame hours

        //Debug.Log(hour + ":" + min);

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            //this indicates that something will happen every hour 
            if (previousHour != hour) //measures that it has reached a new hour and then if it has then whatever inside this will happen
            {
                OnHourChanged?.Invoke(); /*the "?" is the "null" check instead of putting it into an "if statement"
                                          * starts the event OnHourChanged*/

                

                previousHour = hour; //makes sure it resets the function
            }

            //timer = minuteToRealTime;

            //At 25 minutes it spawnes a monster
            if (min >= 25)
            {
                if (!isCreated)
                {
                    MonsterSpawn.SetActive(true);
                    Debug.Log("spawned");

                    isCreated = true;
                }

            }

            //When the hour reaches 06:00am the player will be sent to the win scene
            if(hour == 6)
            {  
                SceneManager.LoadScene(sceneBuildIndex: 4);             // ? could you delay this till after the 6am display UI has finished playing, ca 3 sec
            }


            #region Hourly Object
            switch (hour) /*might have to fix this later, cuz its pretty scuffed
                               * but what it does is it sets the hourly gameobjects to the hour it need
                               * and then deactivates the one that got active and is no longer needed*/
            {
                case 0:
                    if(hour == 0)
                    {
                        //hourlyObjects[0].SetActive(true);
                        //timeSp.GetComponent<TimeSwap>().hour(Player.transform);
                        //Debug.Log("hello, the hour has been changed");
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                    }
                    break;

                case 1:
                    if(hour == 1)
                    {
                        playedHoursound = false;
                        hourlyObjects[1].SetActive(true);
                        //hourlyObjects[0].SetActive(false);
                        timeSp.GetComponent<TimeSwap>().hour(Player.transform); // this will trigger the clock display to appear infront of the player
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                        
                    }
                    break;

                case 2:
                    if(hour == 2)
                    {
                        playedHoursound = false;
                        hourlyObjects[2].SetActive(true);
                        //hourlyObjects[1].SetActive(false);
                        timeSp.GetComponent<TimeSwap>().hour(Player.transform);
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                    }
                    break;

                case 3:
                    if(hour == 3)
                    {
                        playedHoursound = false;
                        hourlyObjects[3].SetActive(true);
                       // hourlyObjects[2].SetActive(false);
                        timeSp.GetComponent<TimeSwap>().hour(Player.transform);
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                    }
                    break;

                case 4:
                    if(hour == 4)
                    {
                        playedHoursound = false;
                        hourlyObjects[4].SetActive(true);
                        hourlyObjects[3].SetActive(false);
                        timeSp.GetComponent<TimeSwap>().hour(Player.transform);
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }

                    }
                    break;

                case 5:
                    if(hour == 5)
                    {
                        playedHoursound = false;
                        hourlyObjects[5].SetActive(true);
                        //hourlyObjects[4].SetActive(false);
                        timeSp.GetComponent<TimeSwap>().hour(Player.transform);
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                    }
                    break;

                case 6:
                    if(hour == 6)
                    {
                        playedHoursound = false;
                        hourlyObjects[6].SetActive(true);
                        //hourlyObjects[5].SetActive(false);
                        timeSp.GetComponent<Animator>().SetBool("Win", true);
                        if (!playedHoursound)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Effects/NewHourEffect",GetComponent<Transform>().position);
                            playedHoursound = true;
                        }
                    }
                    break;
            }
            #endregion
        }

    }


    // films
    public void SnapPic()
    {

    }

    public void  GetFilm()
    {
        film++ ;
      
    }
    
}
