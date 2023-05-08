using CreativeUrge.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.DragHandlers
{
    public abstract class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected ISelectable CurrentSelection => SelectionManager.Instance.Selected;
        
        public abstract void OnBeginDrag(PointerEventData eventData);

        public abstract void OnDrag(PointerEventData eventData);

        public abstract void OnEndDrag(PointerEventData eventData);
    }
}