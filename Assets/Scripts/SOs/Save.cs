using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/Save")]
public class Save : ScriptableObject {
    public List<Inv.Collectable> inventory;
    public int level;
}
