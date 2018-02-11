using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreferenceManager
{
    private const string PLAYER_NAME = "PLAYER_NAME";
    private const string PLAYER_RECORD = "PLAYER_RECORD";
    
    #region Public methods
    
    public static void SetPlayerNameAndPoints(string name, int points = 0)
    {
        PlayerPrefs.SetString(PLAYER_NAME, name);
        PlayerPrefs.SetInt(PLAYER_RECORD, points);
    }

    public static string GetPlayerName()
    {
        return PlayerPrefs.GetString(PLAYER_NAME);
    }

    public static int GetPlayerRecord()
    {
        return PlayerPrefs.GetInt(PLAYER_RECORD, 0);
    }
        
    #endregion
}