using CreativeUrge.Camera;
using CreativeUrge.Projects;
using CreativeUrge.Selection;
using CreativeUrge.Toolbox;
using CreativeUrge.UndoRedo;
using CreativeUrge.UndoRedo.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.Items
{
    public class SelectableItem : MonoBehaviour, ISelectable
    {
        [SerializeField] private Collider itemCollider;
        [SerializeField] private MeshRenderer renderer;
        
        private Vector3 initialPosition;

        public Transform Transform => transform;
        public Renderer Renderer => renderer;
        public ItemData ItemData { get; private set; }

        private void Reset()
        {
            itemCollider = GetComponentInChildren<Collider>();
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        public void Initialize(ItemData itemData)
        {
            ItemData = itemData;
        }
        
        public void SetInteractable(bool interactable)
        {
            itemCollider.enabled = interactable;
        }
        
        public void OnSelect()
        {
            
        }

        public void OnDeselect()
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SelectionManager.Instance.SetDragged(this);
            CameraManager.Instance.DisableInput();
            SetInteractable(false);
            initialPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var ray = UnityEngine.Camera.main.ScreenPointToRay(eventData.position);
            
            if (Physics.Raycast(ray, out var hit))
            {
                transform.position = hit.point;
            }
        }

        public void OnEndDrag(PointerEventData eventData, bool canDelete)
        {
            SetInteractable(true);
            CameraManager.Instance.EnableInput();
            
            ValidateHeight();
            SelectionManager.Instance.SetDragged(null);

            if (canDelete && ToolboxManager.Instance.IsHoveringOverDelete)
            {
                DeleteItem();
            }
        }

        private void DeleteItem()
        {
            var action = new ItemDeletedAction(this, initialPosition);
            UndoRedoSystem.Instance.RecordAction(action);
            
            ItemSpawner.Instance.DisableItem(this);
            ToolboxManager.Instance.ShowBar<InventoryBar>();
        }

        private void ValidateHeight()
        {
            var position = transform.position;
            var height = Mathf.Max(0, position.y);
            position = new Vector3(position.x, height, position.z);
            transform.position = position;
        }
    }
}