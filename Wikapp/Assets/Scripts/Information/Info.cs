using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : Singleton<Info>
{
    public Player currentPlayer = new Player();
    Player myPlayer;
    private string thePlayer()
    {
        return PlayerPrefs.GetString(Constants.Instance.PLAYER_KEY);
    }

    public Player getPlayer()
    {
        return JsonUtility.FromJson<Player>(thePlayer());
    }
    public void setPlayer(string player)
    {
        PlayerPrefs.SetString(Constants.Instance.PLAYER_KEY, player);
        PlayerPrefs.Save();
    }

    public void setCredentials(
        string name, string age, string month,
        string day, string year, string gender
    )
    {
        currentPlayer._name = name;
        currentPlayer._age = age;
        currentPlayer._month = month;
        currentPlayer._day = int.Parse(day);
        currentPlayer._year = year;
        currentPlayer._gender = gender;

        myPlayer = currentPlayer;
    }

    public Player getCredentials()
    {
        return myPlayer;
    }

    public void deletePlayerAndProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool isGameSaved()
    {
        return PlayerPrefs.HasKey(Constants.Instance.PLAYER_KEY);
    }
}
