using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, 
        TileMapController tileMapController, 
        Items item) 
    {
        Debug.LogWarning("OnApplyToTileMap is not implement");
        return true;
    }

    public virtual void OnItemUsed(Items usedItem, ItemContainter inventory)
    {

    }

}
