using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class CharacterToolController : MonoBehaviour
{
    CharacterController character;
    Rigidbody2D rgbd2d;
    Animator animator;
    ToolBarController toolbarController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapController tileMapController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] ToolAction onTilePickUp;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<ToolBarController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        SelectTile();
        CanSelectedCheck();
        Marker();

        if (Input.GetKeyDown("c"))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
            PickUpTile();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectedCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {

        Vector2 position = rgbd2d.position + character.saved_motionVector * offsetDistance;

        Items item = toolbarController.GetItem;
        

        if (item == null)
        {
            return false;
        }

        
        if (item.onAction == null)
        {
            return false;
        }

        ToolAnimation(item);

        bool complete = item.onAction.OnApply(position);

        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }
        }

        return complete;
    }

    private void ToolAnimation(Items items)
    {
        if (items.Tool == true)
        {
            animator.SetTrigger(items.Name);
        }
    }

    private void UseToolGrid()
    {
        if (selectable == true)
        {
            Items item = toolbarController.GetItem;

            if (item == null)
            {
                PickUpTile();
                return;
            }

            if (item.onTileMapAction == null)
            {
                return;
            }


            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition,
                tileMapController,
                item
                );


            if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }
        }
    }

    private void PickUpTile()
    {
        if(onTilePickUp == null)
        {
            return;
        }
        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapController, null);
    }
}
