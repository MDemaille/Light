using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditorSaveAndPlayButton : MonoBehaviour {

    private LevelEditor _editor;

    void Start()
    {
        _editor = GameObject.FindGameObjectWithTag("LevelEditor").GetComponent<LevelEditor>();
        GetComponent<Button>().onClick.AddListener(delegate { _editor.SaveAndPlay(); });
    }
}
