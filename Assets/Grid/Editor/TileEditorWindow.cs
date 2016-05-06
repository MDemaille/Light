using UnityEngine;
using System.Collections;
using System.ComponentModel.Design;
using System.Linq;
using UnityEditor;

public class TileEditorWindow : EditorWindow
{

    private Grid _grid;
    private GameObject _objectToPaint;
    private bool _paintMode = false;
    private float _rotation;

    private GameObject _parentObject;
    private GameObject _currentObject;
    private string _parentFileName;

    [MenuItem( "Window/TileEditorWindow" )]
    private static void Init()
    {
        TileEditorWindow window = (TileEditorWindow)EditorWindow.GetWindow( typeof( TileEditorWindow ) );
        window.Show();
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= SceneGUI;
    }

    private void OnGUI()
    {
        GUILayout.Label( "Grid Settings", EditorStyles.boldLabel );
        _grid = EditorGUILayout.ObjectField( "Grid", _grid, typeof( Grid ), true ) as Grid;
        _objectToPaint = EditorGUILayout.ObjectField( "Object To Paint", _objectToPaint, typeof( GameObject ), false ) as GameObject;
        _rotation = EditorGUILayout.FloatField( "Rotation", _rotation );
        _paintMode = EditorGUILayout.Toggle( "Paint", _paintMode );
        EditorGUILayout.Space();
        _parentFileName = EditorGUILayout.TextField( "Parent file name", _parentFileName );
        if ( GUILayout.Button( "New parent File" ) )
        {
            _parentObject = new GameObject();
            Undo.RegisterCreatedObjectUndo( _parentObject, "Creating new parent object" );
            if(_parentFileName.Length != 0)
                _parentObject.name = _parentFileName;
        }

        if ( GUILayout.Button( "Close parent File" ) )
        {
            _parentObject = null;
        }

        EditorGUILayout.Space();

        if ( GUILayout.Button( "Set position to grid space" ) )
        {
            foreach ( GameObject obj in Selection.objects )
            {
                obj.transform.position = Grid.WorldToGridSpace( obj.transform.position, _grid );
            }
        }

    }

    private void SceneGUI( SceneView sceneView )
    {
        if ( !_paintMode )
        {
            DestroyImmediate( _currentObject );
            return;
        }

        if ( Event.current.type == EventType.MouseDown && Event.current.button == 0 )
        {
            _currentObject = null;
        }

        if ( _currentObject == null )
        {
            Vector2 guiPosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay( guiPosition );
            Physics.Raycast( ray );
            Vector2 objectPosition = new Vector2( ray.origin.x, ray.origin.y );
            _currentObject = Instantiate( _objectToPaint, Grid.WorldToGridSpace( objectPosition, _grid ), Quaternion.AngleAxis( _rotation, Vector3.forward)) as GameObject;
            _currentObject.transform.localScale = new Vector3( _grid.pixelSizeX, _grid.pixelSizeY, 1 );
            Undo.RegisterCreatedObjectUndo( _currentObject, "Creating new Object" );
            if ( _parentObject != null )
            {
                _currentObject.transform.parent = _parentObject.transform;
            }
        }

        if ( _currentObject != null )
        {
            Vector2 guiPosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay( guiPosition );
            Physics.Raycast( ray );
            Vector2 objectPosition = new Vector2( ray.origin.x, ray.origin.y );

            _currentObject.transform.position = Grid.WorldToGridSpace( objectPosition, _grid );
        }


    }

}