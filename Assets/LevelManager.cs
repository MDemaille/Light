using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{
    public float Timer { get; private set; }
    public string LevelName = "LevelTest";

    private Level _level;

    private GameObject _allLayer;
    private GameObject _redLayer;
    private GameObject _greenLayer;
    private GameObject _blueLayer;

    private int _currentLayerID = 0;

    public GameObject Player;

    void Awake()
    {
        _allLayer = GameObject.Find("AllLayer");
        _redLayer = GameObject.Find("RedLayer");
        _greenLayer = GameObject.Find("GreenLayer");
        _blueLayer = GameObject.Find("BlueLayer");

        _level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        _level.LoadLevel(LevelName);

        foreach (GameObject element in _level.AllLayersElements)
        {
            element.transform.SetParent(_allLayer.transform);
        }

        foreach (GameObject element in _level.RedElements)
        {
            element.transform.SetParent(_redLayer.transform);
        }

        foreach (GameObject element in _level.GreenElements)
        {
            element.transform.SetParent(_greenLayer.transform);
        }

        foreach (GameObject element in _level.BlueElements)
        {
            element.transform.SetParent(_blueLayer.transform);
        }

        _level.Clear();

        Vector3 Spawn = GameObject.FindGameObjectWithTag("Departure").transform.position;
        Instantiate(Player, Spawn + (Vector3)Vector2.up , Quaternion.identity);

        DisplayElementsOfOneColor((ColorEnum)_currentLayerID);
    }

    void Update()
    {
        Timer += Time.deltaTime;

        if (Input.GetButtonDown("BackLayer"))
        {
            _currentLayerID --;
            if (_currentLayerID < 0)
                _currentLayerID = 2;

            DisplayElementsOfOneColor((ColorEnum)_currentLayerID);
        }
        else if (Input.GetButtonDown("FrontLayer"))
        {
            _currentLayerID ++;
            _currentLayerID = _currentLayerID%3;
            DisplayElementsOfOneColor((ColorEnum)_currentLayerID);
        }
    }

    public void DisplayElementsOfOneColor(ColorEnum color)
    {
        Debug.Log(color.ToString());
        _redLayer.SetActive(color.Equals(ColorEnum.Red));
        _greenLayer.SetActive(color.Equals(ColorEnum.Green));
        _blueLayer.SetActive(color.Equals(ColorEnum.Blue));
    }

    private void SetActiveAllGameObjectsOfList(List<GameObject> list, bool isActive)
    {
        foreach (GameObject go in list)
        {
            go.SetActive(isActive);
        }
    }
}
