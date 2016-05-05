using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ColorEnum
{
    Red,
    Green,
    Blue,
    All
}

public enum LevelSize
{
    Small,
    Normal,
    Big
}

public class GameManager : MonoBehaviour
{
    public List<GameObject> LevelObjects;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
