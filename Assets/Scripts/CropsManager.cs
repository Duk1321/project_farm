using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Croptile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if(crop == null)
            {
                return false;
            }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTileMap;
    Tilemap TargetTileMap
    {
        get
        {
            if(targetTileMap == null)
            {
                targetTileMap = GameObject.Find("CropsTileMap").GetComponent<Tilemap>();
            }
            return targetTileMap;
        }
    }

    [SerializeField] GameObject cropSpritePrefab;

    Dictionary<Vector2Int, Croptile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Croptile>();
        OnTimeTick += Tick;
        Init();
    }

    public void Tick()
    {
        if (TargetTileMap == null)
        {
            return;
        }

        foreach (Croptile cropTile in crops.Values)
        {
            if(cropTile.crop == null)
            {
                continue;
            }

            cropTile.damage += 0.02f;

            if(cropTile.damage > 1f)
            {
                cropTile.Harvested();
                TargetTileMap.SetTile(cropTile.position, plowed);
                continue;
            }

            if(cropTile.Complete)
            {
                Debug.Log("IM GROWNED");
                continue;
            }

            cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                
                cropTile.growStage += 1;
            }  

        }
    }

    public bool Check(Vector3Int position)
    {
        if (TargetTileMap == null)
        {
            return false;
        }

        return crops.ContainsKey((Vector2Int)position);
    }
    public void Plow(Vector3Int position)
    {
        if (TargetTileMap == null)
        {
            return;
        }

        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

       /* Invoke("Delay", 0.9f);*/

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (TargetTileMap == null)
        {
            return;
        }

        TargetTileMap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        if (TargetTileMap == null)
        {
            return;
        }

        Croptile crop = new Croptile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropSpritePrefab);
        go.transform.position = TargetTileMap.CellToWorld(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        TargetTileMap.SetTile(position, plowed);
    }

    private void Delay()
    {
        
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (TargetTileMap == null) 
        { 
            return; 
        }

        Vector2Int position = (Vector2Int)gridPosition;
        if(crops.ContainsKey(position) == false) 
        { 
            return;
        }
        Croptile cropTile = crops[position];

        if(cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                TargetTileMap.CellToWorld(gridPosition),
                cropTile.crop.yield,
                cropTile.crop.count
                );

            TargetTileMap.SetTile(gridPosition, plowed);
            cropTile.Harvested();
        }
    }
}
