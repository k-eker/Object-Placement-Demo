using CreativeUrge.Camera;
using CreativeUrge.Items;
using CreativeUrge.Projects;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.DragHandlers
{
    public class YRotationDragHandler : DragHandler
    {
        private Quaternion initialRotation;
        
        private const float DRAG_SENSITIVITY = 1f;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            initialRotation = CurrentSelection.Transform.rotation;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            CurrentSelection.Transform.Rotate(Vector3.up, -eventData.delta.x * DRAG_SENSITIVITY);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (initialRotation == CurrentSelection.Transform.rotation)
            {
                return;
            }

            var selectableItem = CurrentSelection as SelectableItem;
            var transform = CurrentSelection.Transform;
            var action = new ItemModifiedAction(selectableItem, transform.position, initialRotation, transform.localScale);
            UndoRedoSystem.Instance.RecordAction(action);
            
            selectableItem.ItemData.Rotation = CurrentSelection.Transform.rotation;
        }
    }
}
