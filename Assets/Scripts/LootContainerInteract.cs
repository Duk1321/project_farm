using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{

    Transform player;
    [SerializeField] float closeDistance;
    Animator animator;
    bool opened;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        /*if(player != null)
        {
            Debug.Log("fecthed");
        }
        else
        {
            Debug.Log("none");
        }*/

        player = GameManager.instance.player.transform;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > closeDistance)
        {
            opened = false;
            animator.SetBool("isOpened", false);
        }
    }
    public override void Interact(Character character)
    {
        if (opened == false)
        {
            opened = true;
            animator.SetBool("isOpened", true);
        }
       
    }
}
