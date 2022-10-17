using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float time; // how long player has been inside game
    public int level; // at what "checkpoint" the player is on, so when they die they respawn at right time
    public int film; // how many films is on the player when saved

    public PlayerData(float time, int level, int film)
    {
        this.time = time;
        this.level = level;
        this.film = film;
    }

    public override string ToString()
    {
        return $"Time elapsed: {time}. They have reached level {level}. And have {film}.";
    }
}
