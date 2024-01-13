using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool action/ Seed tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, 
        TileMapController tileMapController, 
        Items item)
    {
        if (tileMapController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }
        
        tileMapController.cropsManager.Seed(gridPosition, item.crop);

        return true;
    }

    public override void OnItemUsed(Items usedItem, ItemContainter inventory)
    {
        inventory.Remove(usedItem);
    }
}
