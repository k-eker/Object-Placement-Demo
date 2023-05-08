using CreativeUrge.Items;
using CreativeUrge.Projects;
using CreativeUrge.Toolbox;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.DragHandlers
{
    public class MoveDragHandler : DragHandler
    {
        private Vector3 initialPosition;
        private SelectableItem CurrentSelectionItem => CurrentSelection as SelectableItem;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            initialPosition = CurrentSelection.Transform.position;
            
            CurrentSelectionItem.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            CurrentSelectionItem.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (initialPosition != CurrentSelection.Transform.position && !ToolboxManager.Instance.IsHoveringOverDelete)
            {
                var selectableItem = CurrentSelection as SelectableItem;
                var transform = CurrentSelection.Transform;
                
                var action = new ItemModifiedAction(selectableItem, initialPosition, transform.rotation, transform.localScale);
                UndoRedoSystem.Instance.RecordAction(action);
                
                selectableItem.ItemData.Position = CurrentSelection.Transform.position;
            }

            CurrentSelectionItem.OnEndDrag(eventData, true);
        }
    }
}
