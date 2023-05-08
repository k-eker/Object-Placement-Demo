using CreativeUrge.Camera;
using CreativeUrge.Items;
using CreativeUrge.Projects;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.DragHandlers
{
    public class HeightDragHandler : DragHandler
    {
        private Vector3 initialPosition;
        
        private const float DRAG_SENSITIVITY = 0.02f;
        private const float MIN_HEIGHT = 0f;
        private const float MAX_HEIGHT = 2f;
        private const float RAYCAST_HEIGHT = 2000f;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            initialPosition = CurrentSelection.Transform.position;
            CurrentSelection.SetInteractable(false);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            var position = CurrentSelection.Transform.position;
            var origin = new Vector3(position.x, RAYCAST_HEIGHT, position.z);
            
            if (Physics.Raycast(origin, Vector3.down, out var hit))
            {
                var minHeight = Mathf.Max(MIN_HEIGHT, hit.point.y);
                var height = position.y + eventData.delta.y * DRAG_SENSITIVITY;
                height = Mathf.Clamp(height, minHeight, MAX_HEIGHT);
                
                position = new Vector3(position.x, height, position.z);
                CurrentSelection.Transform.position = position;
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            CurrentSelection.SetInteractable(true);
            
            if (initialPosition == CurrentSelection.Transform.position)
            {
                return;
            }

            var selectableItem = CurrentSelection as SelectableItem;
            var transform = CurrentSelection.Transform;
            var action = new ItemModifiedAction(selectableItem, initialPosition, transform.rotation, transform.localScale);
            UndoRedoSystem.Instance.RecordAction(action);
            
            selectableItem.ItemData.Position = CurrentSelection.Transform.position;
        }
    }
}
