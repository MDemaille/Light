﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditorDeleteButton : MonoBehaviour
{
    private LevelEditor _editor;

    void Start()
    {
        _editor = GameObject.FindGameObjectWithTag("LevelEditor").GetComponent<LevelEditor>();
        GetComponent<Button>().onClick.AddListener(delegate { _editor.DeleteCurrentObject(); });
    }
}
