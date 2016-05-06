using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum EditorState
{
    Free,
    Selected
}

public class LevelEditor : MonoBehaviour
{
    public string LevelName = "LevelTest";

    public static EditorState State = EditorState.Free;
    public static ColorEnum EditorLayer = ColorEnum.All;

    private Level _level;

    public GameObject ObjectEdit;
    private SpriteRenderer _currentRenderer;
    private LevelObject _levelObject;

    private Transform _allLayer;
    private Transform _redLayer;
    private Transform _greenLayer;
    private Transform _blueLayer;

    public Grid GridEditor;

    void Start()
    {
        _allLayer = GameObject.Find("AllLayer").transform;
        _redLayer = GameObject.Find("RedLayer").transform;
        _greenLayer = GameObject.Find("GreenLayer").transform;
        _blueLayer = GameObject.Find("BlueLayer").transform;

        _level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        _level.LoadLevel(LevelName);

        foreach (GameObject element in _level.AllLayersElements)
        {
            element.transform.SetParent(_allLayer);
            SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }

        foreach (GameObject element in _level.RedElements)
        {
            element.transform.SetParent(_redLayer);
            SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
                spriteRenderer.color = Color.red;
        }

        foreach (GameObject element in _level.GreenElements)
        {
            element.transform.SetParent(_greenLayer);
            SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.color = Color.green;
        }

        foreach (GameObject element in _level.BlueElements)
        {
            element.transform.SetParent(_blueLayer);
            SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                spriteRenderer.color = Color.blue;
        }

        _level.Clear();
    }

    public void SetCurrentObject(GameObject element)
    {
        if(ObjectEdit != null)
            Destroy(ObjectEdit);

        Vector3 mousePositionOnScreen = Grid.WorldToGridSpace(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridEditor);
        Vector3 objectPosition = new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, 0);

        ObjectEdit = Instantiate(element, objectPosition, Quaternion.identity) as GameObject;
        _currentRenderer = ObjectEdit.GetComponent<SpriteRenderer>();
        _levelObject = ObjectEdit.GetComponent<LevelObject>();
        SetObjectColor(EditorLayer);
    }

    void FreeCurrentObject()
    {
        Vector3 mousePositionOnScreen = Grid.WorldToGridSpace(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridEditor);
        Vector3 objectPosition = new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, 0);

        ObjectEdit = Instantiate(ObjectEdit, objectPosition, Quaternion.identity) as GameObject;
        _currentRenderer = ObjectEdit.GetComponent<SpriteRenderer>();
        _levelObject = ObjectEdit.GetComponent<LevelObject>();
        SetObjectColor(EditorLayer);
    }

    public void DeleteCurrentObject()
    {
        if (ObjectEdit != null)
        {
            Destroy(ObjectEdit);
            _currentRenderer = null;
            _levelObject = null;
        }
    }

    public void SaveAndPlay()
    {
        Save(LevelName);
        SceneManager.LoadScene("Game");
    }

    public void SetObjectColor(ColorEnum color)
    {
        EditorLayer = color;

        if (ObjectEdit == null)
            return;

        _levelObject.Layer = EditorLayer;

        if (EditorLayer.Equals(ColorEnum.Red))
        {
            if (_currentRenderer != null)
                _currentRenderer.color = Color.red;
            ObjectEdit.transform.SetParent(_redLayer);
        }
        else if (EditorLayer.Equals(ColorEnum.Green))
        {
            if (_currentRenderer != null)
                _currentRenderer.color = Color.green;
            ObjectEdit.transform.SetParent(_greenLayer);
        }
        else if (EditorLayer.Equals(ColorEnum.Blue))
        {
            if (_currentRenderer != null)
                _currentRenderer.color = Color.blue;
            ObjectEdit.transform.SetParent(_blueLayer);
        }
        else if (EditorLayer.Equals(ColorEnum.All))
        {
            if (_currentRenderer != null)
                _currentRenderer.color = Color.white;
            ObjectEdit.transform.SetParent(_allLayer);
        }
    }

    public void Save(string filename)
    {
        DeleteCurrentObject();

        foreach (Transform element in _allLayer)
        {
            _level.AllLayersElements.Add(element.gameObject);
        }
        foreach (Transform element in _redLayer)
        {
            _level.RedElements.Add(element.gameObject);
        }
        foreach (Transform element in _greenLayer)
        {
            _level.GreenElements.Add(element.gameObject);
        }
        foreach (Transform element in _blueLayer)
        {
            _level.BlueElements.Add(element.gameObject);
        }

        _level.SaveLevel(filename);
        _level.Clear();
    }

    void Update()
    {
        if (ObjectEdit != null)
        {
            Vector3 mousePositionOnScreen = Grid.WorldToGridSpace(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridEditor);
            Vector3 objectPosition = new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, 0);
            ObjectEdit.transform.position = objectPosition;

            if (Input.GetButtonDown("Drop"))
            {
                FreeCurrentObject();
            }
        }
        else
        {
            if (Input.GetButtonDown("Select"))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000);
                if (hit)
                {
                    Debug.Log("Hit");
                    ObjectEdit = hit.transform.gameObject;
                    _currentRenderer = ObjectEdit.GetComponent<SpriteRenderer>();
                    _levelObject = ObjectEdit.GetComponent<LevelObject>();
                    SetObjectColor(_levelObject.Layer);
                }
                else
                {
                    Debug.Log("NO");
                }
            }
        }
    }
}
