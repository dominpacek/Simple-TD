using UnityEngine;
using UnityEngine.Tilemaps;

public class TurretPlacer : MonoBehaviour
{
    [Header("Unity objects")]
    public Tilemap objectsMap;
    public Tilemap interactiveMap;
    public GameObject goldCounter;

    [Header("Prefabs")]
    public GameObject turretPrefab;
    public GameObject turretShadow;

    public static TurretTemplate turretToPlace;
    public static bool placingTurret;
    private int ownedTurrets;

    void Update()
    {

        Vector3 mousedOverTile = GetTileUnderMouse();
        if (placingTurret && interactiveMap.HasTile(Vector3Int.FloorToInt(mousedOverTile))
                            && !objectsMap.HasTile(Vector3Int.FloorToInt(mousedOverTile)))
        {
            //todo move the display (shadow and range) to gridmaster
            turretShadow.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                if (Player.SpendGold(turretToPlace.cost))
                {
                    ownedTurrets++;
                    createTurret(mousedOverTile, turretToPlace);
                }
                placingTurret = false;

            }
            GridMaster.drawRange(mousedOverTile, turretToPlace.range);
            turretShadow.transform.position = mousedOverTile;
        }
        else
        {
            GridMaster.disableRangeIndicator();
            turretShadow.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            placingTurret = false;
        }
    }

    public void buyTurret(TurretTemplate type)
    {
        if (Player.GetGold() >= type.cost)
        {
            placingTurret = true;
        }
        turretToPlace = type;
    }

    void createTurret(Vector3 coords, TurretTemplate turretType)
    {
        GameObject turret = (GameObject)Instantiate(turretPrefab, coords, Quaternion.identity);
        TurretBehaviour turretScript = turret.GetComponent<TurretBehaviour>();
        turretScript.setType(turretType);
        GridMaster.addTurret(coords, turretScript);

        turret.transform.parent = transform;
    }


    Vector3 GetTileUnderMouse()
    {
        //todo move to gridmaster
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posint = Vector3Int.FloorToInt(mouseWorldPos);
        return objectsMap.GetCellCenterWorld(posint);
    }

}