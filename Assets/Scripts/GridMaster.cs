using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridMaster : MonoBehaviour, ISerializationCallbackReceiver
{
    private static Grid grid;
    private static Tilemap interactiveMap;
    private static Tilemap objectsMap;
    private static Tile turretTile;
    private static LineRenderer rangeIndicator;
    private static GameObject turretStatsPanel;
    private static TurretStatDisplayManager turretStatsManager;

    public bool debug = true;

    [Header("Setters")]
    public Tilemap interactiveMapSetter;
    public Tilemap objectsMapSetter;
    public Tile turretTileSetter;
    public LineRenderer rangeIndicatorSetter;
    public GameObject turretStatsPanelSetter;

    [Header("Colors")]
    public Color selectedColor;
    public Color hoverColor;

    private static Dictionary<Vector3, TurretBehaviour> turrets = new Dictionary<Vector3, TurretBehaviour>();

    private Vector3Int previousMousePos;
    private Vector3Int currentlySelected;

    private static TurretBehaviour selectedTurret = null;
    private bool tileSelected;

    public void OnBeforeSerialize() {; }
    public void OnAfterDeserialize()
    {
        interactiveMap = interactiveMapSetter;
        objectsMap = objectsMapSetter;
        turretTile = turretTileSetter;
        rangeIndicator = rangeIndicatorSetter;
        turretStatsPanel = turretStatsPanelSetter;
    }


    void Start()
    {
        turretStatsManager = turretStatsPanelSetter.GetComponent<TurretStatDisplayManager>();
        grid = gameObject.GetComponent<Grid>();
    }


    void Update()
    {

        // highlight on mouseover
        Vector3Int mousePos = GetMousePositionInGrid();

        if (!mousePos.Equals(previousMousePos))
        {
            resetTileColor(previousMousePos);

            changeTileColor(mousePos, hoverColor);

            previousMousePos = mousePos;

        }


        // select a tile
        if (Input.GetMouseButtonDown(0) && interactiveMap.HasTile(mousePos))
        {
            tileSelected = true;

            resetTileColor(currentlySelected);
            selectTurret(mousePos);

            currentlySelected = mousePos;
        }

        //testing purposes 
        if (debug == true)
        {
            if (selectedTurret != null)
                selectedTurret.range = selectedTurret.range * (1 + Input.mouseScrollDelta.y * 0.1f);
        }


        // highlight the selected tile
        if (tileSelected == true)
        {
            changeTileColor(currentlySelected, selectedColor);
        }

        // display stats or selected/placed turret
        if (TurretPlacer.placingTurret)
        {
            turretStatsManager.displayTemplateStats(TurretPlacer.turretToPlace, "(placing)");
            turretStatsManager.hideUpgradeButton();
        }
        else if (selectedTurret != null)
        {
            turretStatsManager.displayTurretStats(selectedTurret);
            drawRange(interactiveMap.GetCellCenterWorld(currentlySelected), selectedTurret.getRange());
        }


    }

    void changeTileColor(Vector3Int tileCoords, Color color)
    {
        interactiveMap.SetTileFlags(tileCoords, TileFlags.None);
        interactiveMap.SetColor(tileCoords, color);
        interactiveMap.SetTileFlags(tileCoords, TileFlags.LockColor);
    }

    void resetTileColor(Vector3Int tileCoords)
    {
        interactiveMap.SetTileFlags(tileCoords, TileFlags.None);
        interactiveMap.SetColor(tileCoords, new Color(255f, 255f, 255f, 0f));
        interactiveMap.SetTileFlags(tileCoords, TileFlags.LockColor);
    }


    public static Vector3Int GetMousePositionInGrid()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    public static Vector3Int worldToCell(Vector3 coords)
    {
        return grid.WorldToCell(coords);
    }

    static void selectTurret(Vector3Int coords)
    {
        Vector3 clickedTile = interactiveMap.GetCellCenterWorld(coords);
        if (turrets.ContainsKey(clickedTile))
        {
            turrets.TryGetValue(clickedTile, out selectedTurret);
        }
        else
        {
            selectedTurret = null;
            disableRangeIndicator();
        }
    }

    public static void addTurret(Vector3 coords, TurretBehaviour turret)
    {
        Vector3Int tileCoords = Vector3Int.FloorToInt(coords);
        Vector3 tileCenter = interactiveMap.GetCellCenterWorld(tileCoords);
        turrets.Add(tileCenter, turret);
        objectsMap.SetTile(tileCoords, turretTile);
    }

    public static void drawRange(Vector3 center, float radius)
    {
        int steps = 100;

        rangeIndicator.gameObject.SetActive(true);
        rangeIndicator.loop = true;
        rangeIndicator.startWidth = 0.05f;
        rangeIndicator.positionCount = steps;
        Vector3[] positions = new Vector3[steps];
        for (int i = 0; i < steps; i++)
        {
            float progress = (float)i / steps;

            float radian = progress * 2 * Mathf.PI;

            float xRatio = Mathf.Cos(radian);
            float yRatio = Mathf.Sin(radian);

            float x = xRatio * radius + center.x;
            float y = yRatio * radius + center.y;

            positions[i] = new Vector3(x, y, 0);
        }

        rangeIndicator.SetPositions(positions);
    }

    public static void disableRangeIndicator()
    {
        rangeIndicator.gameObject.SetActive(false);
    }

    public void upgradeTurret()
    {
        if (selectedTurret == null)
        {
            Debug.LogError("No turret to upgrade!");
            return;
        }
        else
        {
            selectedTurret.Upgrade();
        }
    }

    public void setDebugMode(bool set){
        debug = set;
    }

}
