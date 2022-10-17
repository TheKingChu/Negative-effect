using UnityEngine;
using System;

public class GameSaver
{
    //Sets the savefile version number.
    /// <summary>
    /// Saves the game version to the game file. 
    /// </summary>
    /// <param name="version">version of the game</param>
    public void SaveGameVersion(string version)
    {
        PlayerPrefs.SetString("NegativeEffect_Version_", version);
    }

    //returns the version. used to check if a savefile has the same game version
    /// <summary>
    /// returns the game version as a string.
    /// </summary>
    /// <returns>version number</returns>
    public string LoadgameVersion()
    {
        return PlayerPrefs.GetString("NegativeEffect_Version_");
    }
}
