using CreativeUrge.Projects;
using CreativeUrge.Selection;
using CreativeUrge.Toolbox;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CreativeUrge.Items
{
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image iconImage;

        private ItemAsset itemAsset;
        private SelectableItem draggedItem;
        private Vector2 initialDraggedItemPos;
        
        public void Initialize(ItemAsset itemAsset)
        {
            this.itemAsset = itemAsset;
            
            iconImage.sprite = itemAsset.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            draggedItem = ItemSpawner.Instance.SpawnItemWithoutSave(itemAsset);
            
            SelectionManager.Instance.Select(draggedItem);
            
            draggedItem.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            draggedItem.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ToolboxManager.Instance.IsHoveringOverDelete)
            {
                
                draggedItem.OnEndDrag(eventData, false);
                ToolboxManager.Instance.ShowBar<InventoryBar>();
                Destroy(draggedItem.gameObject);
                
                draggedItem = null;
                
                SelectionManager.Instance.DeselectCurrent();
                
                return;
            }
            
            draggedItem.OnEndDrag(eventData, true);
            
            var action = new ItemPlacedAction(draggedItem);
            UndoRedoSystem.Instance.RecordAction(action);

            var itemData = new ItemData(itemAsset, draggedItem);
            ProjectManager.Instance.CurrentProject.Items.Add(itemData);
            
            draggedItem.Initialize(itemData);
            
            draggedItem = null;
        }
    }
}
