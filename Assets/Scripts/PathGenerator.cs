using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

public class PathGenerator : MonoBehaviour
{
    public static List<Vector3> waypoints = new List<Vector3>();
    public Tilemap pathTilemap;
    public Tile[] pathTileAssets;
    public GameObject spawner;

    private List<Vector2Int> pathShape = new List<Vector2Int>();

    private int mapWidth = 25;
    private int mapHeight = 15;

    // allows for manual creation of levels

    public bool debugCreateMap = false;

    void Awake()
    {
        if (debugCreateMap == false)
        {
            pathShape = Levels.pickLevel();
            generateMap();
        }
    }

    void Update()
    {
        /* 
        To create a level manually start a new game with debugCreateMap set to true,
        then use LMB to draw the desired path (tiles have to be selected in the order
        enemies will walk on them). RMB clears the map and logs the List<Vector2Int>
        level format to the unity Console from where you can copy it into the code.

        Yes I know this is a convoluted way to do it.
        */

        if (debugCreateMap == true)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2Int mousepos = (Vector2Int)GridMaster.GetMousePositionInGrid();
                if (!pathShape.Contains(mousepos))
                {
                    setTileAtVector(mousepos, pickPathTile());
                    pathShape.Add(mousepos);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                logMapElements();

                pathTilemap.ClearAllTiles();
                pathShape = new List<Vector2Int>();

                return;
            }

        }
    }

    private void logMapElements()
    {
        Debug.Log(pathShape.Count);
        string log = "";
        foreach (Vector2Int tile in pathShape)
        {
            log += ($"new Vector2Int({tile.x}, {tile.y}),");
        }
        Debug.Log(log);
    }

    private void generateMap()
    {
        int startX = 0;
        int startY = mapHeight / 2;
        Vector2Int startCoords = new Vector2Int(startX, startY);

        int endX = mapWidth - 1;
        int endY = startY;
        Vector2Int endCoords = new Vector2Int(endX, endY);



        SetCells(pathShape, startCoords, endCoords);

    }

    private void SetCells(List<Vector2Int> path, Vector2Int startCoords, Vector2Int endCoords)
    {
        setTileAtVector(startCoords, pickPathTile());
        spawner.transform.position = waypoints[0];

        foreach (Vector2Int coordinate in path)
        {
            setTileAtVector(coordinate, pickPathTile());
        }

        setTileAtVector(endCoords, pickPathTile());


        spawner.SetActive(true);
    }

    /* sets tiles with a time delay after placing each,
        used for debug purposes */
    private IEnumerator SetCellsDelay(List<Vector2Int> path, Vector2Int endCoords)
    {

        foreach (Vector2Int coordinate in path)
        {
            setTileAtVector(coordinate, pickPathTile());
            yield return new WaitForSeconds(0.1f);
        }

        setTileAtVector(endCoords, pickPathTile());


        spawner.SetActive(true);
        yield return null;
    }

    private Tile pickPathTile()
    {
        int i = UnityEngine.Random.Range(0, pathTileAssets.Length);

        return pathTileAssets[i];
    }

    private void setTileAtVector(Vector2Int coords, Tile tile)
    {
        Vector3Int cellPos = (Vector3Int)coords;
        pathTilemap.SetTile(cellPos, tile);

        Vector3 worldPos = pathTilemap.GetCellCenterLocal(cellPos);
        waypoints.Add(worldPos);
    }

}
