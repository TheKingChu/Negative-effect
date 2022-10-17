using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Watch : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Text text;
    public bool shouldCountTime;

    private static Watch instance;
    private float currentTime = GameManager.getTimer;

    private void Awake()
    {
        if(instance && instance != this)
        {
            //if another instance of this object exists destroy this one
            Destroy(gameObject);
            return;
        }

        instance = this; //this is the active instance of the object

        //lets not destroy this object when a new scene is loaded
        DontDestroyOnLoad(gameObject);

        //attach a callback for every new scene that is loaded
        //it is fine to remove a callback that wasn't added so far
        //this makes sure that this callback is definitely only added once
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //this will be called every time a scene is loaded
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //checks the build index
        shouldCountTime = scene.buildIndex != 0;

        //resets the timer in the menu scene
        if (!shouldCountTime)
        {
            Debug.Log("reset timer", this);
            currentTime = 0;
        }
    }

    private void Update()
    {
        int min = (int)GameManager.getTimer * 12 / 60 % 60;
        int hour = (int)GameManager.getTimer * 12 / 3600 % 24;

        string _Min;
        if (min < 10)
        {
            _Min = "0" + min;
        }
        else
        {
            _Min = min.ToString();
        }
            
        string _Hour;
        if(hour < 10)
        {
            _Hour = "0" + hour;
        }
        else
        {
            _Hour = hour.ToString();
        }
        text.text = _Hour + ":" + _Min;
    }
}
