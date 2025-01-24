using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item",menuName = "New item/item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public ItemType itemType;
    public Sprite ItemImage;
    public GameObject ItemPrefebs;

    public enum ItemType
    {
        Used,
        Equipment,
        Ingredient,
        Etc
    }

}
