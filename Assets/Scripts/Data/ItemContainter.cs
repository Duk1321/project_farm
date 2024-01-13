using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Items item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Set(Items item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null; 
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Containter")]
public class ItemContainter : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Items item, int count = 1)
    {
        if(item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if(itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if(itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }

    public void Remove(Items itemToRemove, int count = 1)
    {
        if (itemToRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(xx => xx.item == itemToRemove);
            if(itemSlot == null) { return; }
            
            itemSlot.count -= count;
            if(itemSlot.count < 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            while(count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);
                if(itemSlot == null) { break; }
                itemSlot.Clear();
            }
        }
    }
}