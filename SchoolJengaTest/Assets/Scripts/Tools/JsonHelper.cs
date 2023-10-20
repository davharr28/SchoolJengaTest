using System;
using UnityEngine;


/// <summary>
/// Tool that takes json arrays and puts the info into a class T returns that array
/// </summary>
public static class JsonHelper
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
    public static T[] FromJsonArray<T>(string json)
    {
        string newJson = "{ \"Items\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

 
}

