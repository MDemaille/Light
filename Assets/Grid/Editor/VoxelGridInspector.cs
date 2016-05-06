using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class VoxelGridInspector : Editor
{
    private Grid grid;

    private void OnSceneGUI()
    {
        grid = target as Grid;
        ShowGrid();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        grid = target as Grid;
        if ( GUILayout.Button( "Update Grid" ) )
        {
            Undo.RecordObject( grid, "Update Grid" );
            grid.UpdateGrid();
            EditorUtility.SetDirty( grid );
        }
    }

    private void ShowGrid()
    {
        for ( int x = 0; x <= grid.CurrentGridResolutionX; x++ )
            for ( int y = 0; y <= grid.CurrentGridResolutionY; y++ )
            {

                Vector3 currentPoint = grid.GetGridPointCoordinates( x, y );
                Vector3 pointNext = grid.GetGridPointCoordinates( x + 1, y );
                Vector3 pointAbove = grid.GetGridPointCoordinates( x, y + 1 );

                Handles.color = Color.green;
                if ( x != grid.CurrentGridResolutionX )
                    Handles.DrawLine( currentPoint, pointNext );
                if ( y != grid.CurrentGridResolutionY )
                    Handles.DrawLine( currentPoint, pointAbove );

            }
    }
}
