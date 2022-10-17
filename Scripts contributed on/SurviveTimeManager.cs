/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This saves the game at every ingame hour
/// </summary>
public class SurviveTimeManager : MonoBehaviour
{
    public Animator amAni;
    static string timeString = "0";
    public static Text timeText;

    private void OnEnable()
    {
        GameManager.OnHourChanged += TimeCheck;
    }

    private void OnDisable()
    {
        GameManager.OnHourChanged -= TimeCheck;
    }

    private void Update()
    {

        //Debug.Log("is it working?");
    }

    static void Hour(string T)
    {
        timeText.text = T;
        amAni.SetBool("Win", true);

    }


    //Dont understand how this works tbh, gonna have to look more into it
    private void TimeCheck()
    {
        if (GameManager.Hour == 01)
        {
            if (GameManager.Minute == 59)
            {
                //SceneManager.LoadScene(4);
                Debug.Log("is the timecheck working?");
            }
        }
        if (GameManager.Hour == 06)
        {
            amAni.SetBool("Win", true);

        }
    }
}*/
