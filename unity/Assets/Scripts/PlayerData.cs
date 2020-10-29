using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    public string plummie_tag;
    public int collisions;
    public int steps;

    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }

}
