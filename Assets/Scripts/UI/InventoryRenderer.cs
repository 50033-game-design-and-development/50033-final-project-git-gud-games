using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;


public class HotbarItemDragHandler : DragCallbacks {
    private readonly int _hotbarItemIndex;
    private readonly InventoryRenderer _inventoryRenderer;
    
    public HotbarItemDragHandler(
        InventoryRenderer inventoryRenderer, int hotbarItemIndex
    ) {
        _inventoryRenderer = inventoryRenderer;
        _hotbarItemIndex = hotbarItemIndex;
    }
    
    public void OnDragStart(IPointerEvent evt) {
        _inventoryRenderer.OnDragStart(_hotbarItemIndex);
    }

    public void OnHoverStart(MouseEnterEvent evt) {
        _inventoryRenderer.OnHoverOverItem(_hotbarItemIndex);
    }

    public void OnHoverEnd(MouseLeaveEvent evt) {
        _inventoryRenderer.OnHoverLeaveItem(_hotbarItemIndex);
    }

    public void OnDragEnd(IPointerEvent evt) {
        _inventoryRenderer.OnDragEnd(_hotbarItemIndex);
    }
}

public class InventoryRenderer : MonoBehaviour {
    // hotbar USS slot class to assign to filled inventory slots
    private const string OCCUPIED_SLOT_CLASS = "filled";
    // hotbar USS slot class to assign to inventory slot description text
    private const string HOVERING_SLOT_CLASS = "show";

    private InventoryItem? _hoveringItemType = null;
    private InventoryItem? _draggingItemType = null;

    private float _padding;
    private float _hotbarItemSize;
    private Label _descriptionLabel;
    
    public UIDocument hotbarUI;
    public int numHotbarItems = 5;

    // gap between bar item images
    private readonly List<DraggableButton> _hotbarItems = new();

    public void OnInventoryUpdate() {
        if (GameState.isInventoryOpened) {
            Show();
            PopulateHotbar();
        } else {
            Hide();
        }
    }

    private void Show() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Visible;
    }

    private void Hide() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Hidden;
    }

    public void OnDragStart(int hotbarItemIndex) {
        if (hotbarItemIndex >= GameState.inventory.Count) { return; }
        var selectedInventoryItem = GameState.inventory[hotbarItemIndex];
       
        GameState.selectedInventoryItem = selectedInventoryItem;
        GameState.isDraggingInventoryItem = true;
        _draggingItemType = selectedInventoryItem.itemType;
        UpdateInventoryDescription();
    }

    public void OnDragEnd(int _) {
        _draggingItemType = null;
        UpdateInventoryDescription();
    }

    public void OnHoverOverItem(int hotbarItemIndex) {
        _hoveringItemType = GameState.inventory[hotbarItemIndex].itemType;
        UpdateInventoryDescription();
    }
    
    public void OnHoverLeaveItem(int _) {
        _hoveringItemType = null;
        UpdateInventoryDescription();
    }

    /// <summary>
    /// display the inventory item description during
    /// either item dragging or hovering over the hotbar slot
    /// </summary>
    private void UpdateInventoryDescription() {
        InventoryItem ?displayedItemType = null;
        
        // prioritize showing dragged item names over hovered items
        if (_draggingItemType != null) {
            displayedItemType = _draggingItemType;
        } else if (_hoveringItemType != null) {
            displayedItemType = _hoveringItemType;
        }

        if (displayedItemType != null) {
            var text = InventoryDescriptions.Get(
                (InventoryItem) displayedItemType
            );
            _descriptionLabel.text = text;
        }
        
        if (displayedItemType == null) {
            _descriptionLabel.RemoveFromClassList(HOVERING_SLOT_CLASS);
        } else {
            _descriptionLabel.AddToClassList(HOVERING_SLOT_CLASS);
        }
    }

    /// <summary>
    ///  Initialize the hotbar item slot UI elements
    /// </summary>
    private void InitializeHotbar() {
        // wait for UI document to render,
        // necessary before reading hotbar padding value
        VisualElement root = hotbarUI.rootVisualElement;
        var buttons = root.Query<Button>().ToList();
        // Remove pre-existing buttons from the inventory
        foreach (var button in buttons) {
            button.RemoveFromHierarchy();
        }

        var wrapper = root.Q<VisualElement>("HotbarWrapper");
        var hotbarElement = wrapper.Q<VisualElement>("Hotbar");
        _descriptionLabel = wrapper.Q<Label>("ItemDescription");
        
        _padding = (
            hotbarElement.resolvedStyle.paddingTop 
            + hotbarElement.resolvedStyle.paddingBottom
        ) / 2.0f;

        var hotbarHeight = hotbarElement.resolvedStyle.height;
        _hotbarItemSize = hotbarHeight - _padding * 2;

        for (int k = 0; k < numHotbarItems; k++) {
            // Create hotbar item slots (each slot is a UIElement Button)
            var dragCallback = new HotbarItemDragHandler(this, k);
            DraggableButton inventoryItem = new DraggableButton(dragCallback);
            
            // DragAndDropManipulator manipulator = new(inventoryItem);
            inventoryItem.style.width = _hotbarItemSize;
            hotbarElement.Add(inventoryItem);
            _hotbarItems.Add(inventoryItem);
        }
    }

    /// <summary>
    /// fill up the hotbar items slot UI elements based
    /// on the list of inventory items in GameState
    /// </summary>
    private void PopulateHotbar() {
        for (var k = 0; k < _hotbarItems.Count; k++) {
            var hotbarSlot = _hotbarItems[k];
            
            Debug.Assert(
                GameState.inventory.Count < _hotbarItems.Count,
                "Theres more inventory items than hotbar slots available!"
            );

            if (k < GameState.inventory.Count) {
                // set hotbar slot background image and make slot active
                var collectable = GameState.inventory[k];
                hotbarSlot.AddToClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = (
                    new StyleBackground(collectable.itemSprite)
                );

                // make the inventory slot visible
                hotbarSlot.style.display = DisplayStyle.Flex;
            } else {
                // remove hotbar slot background image and make slot inactive
                // if theres no corresponding inventory item for the hotbar slot
                hotbarSlot.RemoveFromClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = null;
                
                // make the inventory slot invisible
                hotbarSlot.style.display = DisplayStyle.None;
            }
        }
    }

    private IEnumerator RenderHotbar() {
        // wait for UI document to render
        // before populating it with hotbar UI elements
        yield return new WaitForEndOfFrame();
        InitializeHotbar();
        PopulateHotbar();
    }

    // Start is called before the first frame update
    void OnEnable() {
        Hide();
        StartCoroutine(RenderHotbar());
    }
}
