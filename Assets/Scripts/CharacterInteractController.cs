using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 0.2f;
    [SerializeField] float sizeOfInteractableArea = 0.4f;
    Character character;
    [SerializeReference] SelectableSignController signController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }
    private void Update()
    {
        Check();

        if (Input.GetKeyDown("z"))
        {
            Interact();
        }
    }

   

    private void Interact()
    {

        Vector2 position = rgbd2d.position + characterController.saved_motionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {

            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }


    private void Check()
    {
        Vector2 position = rgbd2d.position + characterController.saved_motionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
;               signController.selectable(hit.gameObject);
                return;
            }
        }
        signController.Hide();
    }
}
