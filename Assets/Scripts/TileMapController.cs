using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public CropsManager cropsManager;

    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(TileData tileData in tileDatas)
        {
            foreach(TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        if(tilemap == null)
        {
            tilemap = GameObject.Find("Base TileMap").GetComponent<Tilemap>();
        }

        if(tilemap == null)
        {
            return Vector3Int.zero;
        }


        Vector3 worldPosition;

        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position; 
        }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition); 

        return gridPosition;
    }


    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("BaseTileMap").GetComponent<Tilemap>();
        }
        if (tilemap == null)
        {
            return null;
        }

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTiles[tileBase];
    }
}
