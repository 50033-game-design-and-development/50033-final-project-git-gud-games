using System;
using UnityEngine;

namespace Inv {
    [Serializable]
    public struct Collectable {
        public InventoryItem itemType;
        public Sprite itemSprite;
    }
}