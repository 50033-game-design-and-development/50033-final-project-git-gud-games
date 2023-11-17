using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HotbarItemDragHandler : DragCallbacks {
    private readonly int _hotbarItemIndex;
    
    public HotbarItemDragHandler(int hotbarItemIndex) {
        _hotbarItemIndex = hotbarItemIndex;
    }
    
    public void OnDragStart(IPointerEvent evt) {
        if (_hotbarItemIndex >= GameState.inventory.Count) { return; }
        Debug.Log("currently"+ GameState.selectedInventoryItem.HasValue.ToString());
        GameState.selectedInventoryItem = GameState.inventory[_hotbarItemIndex];
        Debug.Log("now"+ GameState.selectedInventoryItem.HasValue.ToString());
        Debug.Log(_hotbarItemIndex.ToString());
        GameState.isDraggingInventoryItem = true;
    }
}

public class InventoryRenderer : MonoBehaviour {
    // hotbar USS slot class to assign to filled inventory slots
    private const string OCCUPIED_SLOT_CLASS = "filled";
    
    private float _padding;
    private float hotbarItemSize;
    
    public UIDocument hotbarUI;
    public int hotbarItemGap = 40;
    public int numHotbarItems = 5;

    // gap between bar item images
    private readonly List<DraggableButton> _hotbarItems = new();

    public void OnInventoryUpdate() {
        if (GameState.inventoryOpened) {
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

        VisualElement hotbarElement = (
            root.Q<VisualElement>("HotbarWrapper").Q<VisualElement>("Hotbar")
        );
        _padding = (
            hotbarElement.resolvedStyle.paddingTop 
            + hotbarElement.resolvedStyle.paddingBottom
        ) / 2.0f;

        var hotbarHeight = hotbarElement.resolvedStyle.height;
        hotbarItemSize = hotbarHeight - _padding * 2;

        for (int k = 0; k < numHotbarItems; k++) {
            // Create hotbar item slots (each slot is a UIElement Button)
            var dragCallback = new HotbarItemDragHandler(k);
            DraggableButton inventoryItem = new DraggableButton(dragCallback);
            
            // DragAndDropManipulator manipulator = new(inventoryItem);
            inventoryItem.style.width = hotbarItemSize;
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
