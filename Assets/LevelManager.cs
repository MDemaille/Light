using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{

    public float Timer { get; private set; }

    private List<GameObject> _redElements = new List<GameObject>();
    private List<GameObject> _greenElements = new List<GameObject>();
    private List<GameObject> _blueElements = new List<GameObject>();

    private int _currentLayerID = 0;

    void Start()
    {
        GameObject[] layers = GameObject.FindGameObjectsWithTag(Tags.ColorLayer);
        foreach (GameObject go in layers)
        {
            go.transform.position = Vector2.zero;
        }

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

    public void AddElement(GameObject element, ColorEnum color)
    {
        if (color.Equals(ColorEnum.Red))
            _redElements.Add(element);
        else if (color.Equals(ColorEnum.Green))
            _greenElements.Add(element);
        else if (color.Equals(ColorEnum.Blue))
            _blueElements.Add(element);
    }

    public void DisplayElementsOfOneColor(ColorEnum color)
    {
        Debug.Log(color.ToString());
        SetActiveAllGameObjectsOfList(_redElements, color.Equals(ColorEnum.Red));
        SetActiveAllGameObjectsOfList(_greenElements, color.Equals(ColorEnum.Green));
        SetActiveAllGameObjectsOfList(_blueElements, color.Equals(ColorEnum.Blue));
    }

    private void SetActiveAllGameObjectsOfList(List<GameObject> list, bool isActive)
    {
        foreach (GameObject go in list)
        {
            go.SetActive(isActive);
        }
    }
}
