using UnityEngine;


public class Grid : MonoBehaviour
{
    public int GridResolutionX;
    public int GridResolutionY;

    public int CurrentGridResolutionX { get; private set; }
    public int CurrentGridResolutionY { get; private set; }

    [HideInInspector]
    public Vector3[] GridPoints;

    public Vector3 BottomLeftCorner { get; private set; }
    public Vector3 TopRightCorner { get; private set; }

    public float pixelSizeX { get; private set; }
    public float pixelSizeY { get; private set; }

    public void Start()
    {
        UpdateGrid();
    }

    public void UpdateGrid()
    {
        BottomLeftCorner = transform.Find( "BottomLeftCorner" ).position;
        TopRightCorner = transform.Find( "TopRightCorner" ).position;

        CurrentGridResolutionX = GridResolutionX;
        CurrentGridResolutionY = GridResolutionY;

        float sizeGridX = TopRightCorner.x - BottomLeftCorner.x;
        float sizeGridY = TopRightCorner.y - BottomLeftCorner.y;

        pixelSizeX = Mathf.Abs( sizeGridX / CurrentGridResolutionX );
        pixelSizeY = Mathf.Abs( sizeGridY / CurrentGridResolutionY );
        GridPoints = new Vector3[ CurrentGridResolutionX * CurrentGridResolutionY ];

        int i = 0;
        for ( int y = 0; y < CurrentGridResolutionY; y++ )
            for ( int x = 0; x < CurrentGridResolutionX; x++ )
            {
                GridPoints[ i++ ] = new Vector3( BottomLeftCorner.x + ( x + 0.5f ) * pixelSizeX, BottomLeftCorner.y + ( y + 0.5f ) * pixelSizeY, 0 );
            }
    }

    public static Vector3 WorldToGridSpace( Vector3 position, Grid grid )
    {
        float sizeGridX = grid.TopRightCorner.x - grid.BottomLeftCorner.x;
        float sizeGridY = grid.TopRightCorner.y - grid.BottomLeftCorner.y;
        float pixelSizeX = Mathf.Abs( sizeGridX / grid.CurrentGridResolutionX );
        float pixelSizeY = Mathf.Abs( sizeGridY / grid.CurrentGridResolutionY );

        int columnGrid = Mathf.RoundToInt(( position.x - grid.BottomLeftCorner.x ) / pixelSizeX);
        int lineGrid = Mathf.RoundToInt( ( position.y - grid.BottomLeftCorner.y ) / pixelSizeY );

        float x = grid.BottomLeftCorner.x + columnGrid * pixelSizeX;
        float y = grid.BottomLeftCorner.y + lineGrid * pixelSizeY;

        if ( position.x < x )
            x -= pixelSizeX / 2;
        else
            x += pixelSizeX / 2;

        if ( position.y < y )
            y -= pixelSizeY / 2;
        else
            y += pixelSizeY / 2;

        return new Vector3( x, y, grid.BottomLeftCorner.z );

    }

    public Vector3 GetGridPointCoordinates( int x, int y )
    {
        float CoordX = BottomLeftCorner.x + x * pixelSizeX ;
        float CoordY = BottomLeftCorner.y + y * pixelSizeY ;

        return new Vector3(CoordX, CoordY, BottomLeftCorner.z);
    }

}
