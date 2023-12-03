using System.Collections.Generic;

namespace UI {
    public static class InventoryDescriptions {
        private static readonly Dictionary<
            InventoryItem, string
        > DESCRIPTIONS = new() {
            { InventoryItem.L0_Paper, "Piece of Paper" },
            { InventoryItem.L0_Key, "Old Key" },
            
            { InventoryItem.L1_Capsicum, "Capsicum" },
            { InventoryItem.L1_Garlic, "Garlic Clove" },
            { InventoryItem.L1_Lemon, "Fresh Lemon" },
            { InventoryItem.L1_Onion, "Whole Onion" },
            { InventoryItem.L1_Banana, "Ripe Banana" },
            { InventoryItem.L1_Mushroom, "Wild Mushroom" },
            { InventoryItem.L1_Carrot, "Crunchy Carrot" },
            { InventoryItem.L1_Tomato, "Juicy Tomato" },
            { InventoryItem.L1_Lily, "Lily" },
            { InventoryItem.L1_Vial, "Empty Vial" },
            { InventoryItem.L1_Vial_filled, "Filled Vial" },
            { InventoryItem.L1_Pencil, "Pencil" },
            
            { InventoryItem.L2_Fuse, "Electrical Fuse" },
            { InventoryItem.L2_Floppy, "Floppy Disk" },
            
            { InventoryItem.L2_5_Candles, "Set of Candles" },
            { InventoryItem.L2_5_Photo, "Photograph" },
            { InventoryItem.L2_5_Silver_key, "Silver Key" }
        };

        public static string Get(InventoryItem item) {
            return DESCRIPTIONS.TryGetValue(item, out var value) ? 
                value : "<UNKNOWN>";
        } 
    }
}
