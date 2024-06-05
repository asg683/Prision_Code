using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public Transform goToPoint;
    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(3,0.5f);
} 