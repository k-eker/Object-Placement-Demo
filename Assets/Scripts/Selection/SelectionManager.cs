using CreativeUrge.Items;
using CreativeUrge.Utility;
using UnityEngine;

namespace CreativeUrge.Selection
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private SelectionUI selectionUI;
        
        private UnityEngine.Camera mainCamera;
        private ISelectable pressedSelectable;

        public ISelectable Selected { get; private set; }
        public ISelectable Dragged { get; private set; }
        
        public static SelectionManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            mainCamera = UnityEngine.Camera.main;
        }

        private void Start()
        {
            selectionUI.SetTarget(null);
        }

        private void Update()
        {
            DetectSelection();
        }

        private void DetectSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                pressedSelectable = GetSelectableUnderPointer();
            }
            else if (Input.GetMouseButtonUp(0) && pressedSelectable != null)
            {
                var selectable = GetSelectableUnderPointer();
                if (selectable == pressedSelectable)
                {
                    Select(selectable);
                }

            }
        }

        private ISelectable GetSelectableUnderPointer()
        {
            if (TouchUtility.IsPointerOverUIObject())
            {
                return null;
            }
                
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(ray, out var hit))
            {
                var selectable = hit.collider.GetComponentInParent<ISelectable>();
                    
                if (selectable != null)
                {
                    return selectable;
                }
            }

            return null;
        }

        public void Select(ISelectable selectable)
        {
            if (Selected != null)
            {
                DeselectCurrent();
            }
            
            Selected = selectable;
            Selected.OnSelect();
            selectionUI.SetTarget(Selected);
        }
        
        private void Deselect(ISelectable selectable)
        {
            if (Selected != selectable)
            {
                return;
            }
            
            Selected.OnDeselect();
            Selected = null;
            selectionUI.SetTarget(null);
        }

        public void DeselectCurrent()
        {
            if (Selected == null)
            {
                return;
            }

            Deselect(Selected);
        }

        public void SetDragged(SelectableItem selectableItem)
        {
            Dragged = selectableItem;

            var dragReleased = selectableItem == null;
            selectionUI.SetInteractable(dragReleased);
        }
    }
}
