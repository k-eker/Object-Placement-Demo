using System;
using CreativeUrge.Camera;
using CreativeUrge.Items;
using CreativeUrge.Projects;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.DragHandlers
{
    public class ScaleDragHandler : DragHandler
    {
        private Vector3 initialScale;
        
        private const float DRAG_SENSITIVITY = 0.1f;
        private const float MIN_SCALE = 1f;
        private const float MAX_SCALE = 10f;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            initialScale = CurrentSelection.Transform.localScale;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            var scaleChange = eventData.delta.y * DRAG_SENSITIVITY;
            var newScale = CurrentSelection.Transform.localScale + Vector3.one * scaleChange;

            newScale.x = Mathf.Clamp(newScale.x, MIN_SCALE, MAX_SCALE);
            newScale.y = Mathf.Clamp(newScale.y, MIN_SCALE, MAX_SCALE);
            newScale.z = Mathf.Clamp(newScale.z, MIN_SCALE, MAX_SCALE);
            
            CurrentSelection.Transform.localScale = newScale;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (initialScale == CurrentSelection.Transform.localScale)
            {
                return;
            }
            
            var selectableItem = CurrentSelection as SelectableItem;
            var transform = CurrentSelection.Transform;
            var action = new ItemModifiedAction(selectableItem, transform.position, transform.rotation, initialScale);
            UndoRedoSystem.Instance.RecordAction(action);
                
            selectableItem.ItemData.Scale = CurrentSelection.Transform.localScale;
        }
    }
}