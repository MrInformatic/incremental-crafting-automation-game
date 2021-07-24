using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite Image;

    public Item SourceItemA;
    public Item SourceItemB;

    public Item AutomationItem;
    public int AutomationItemAmount = 10;
}
