using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool action/Harvest")]
public class TilePickUpAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapController tileMapController, Items item)
    {
        tileMapController.cropsManager.PickUp(gridPosition);

        return true;
    }
}
