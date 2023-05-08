using CreativeUrge.Items;
using CreativeUrge.Projects;
using UnityEngine;

namespace CreativeUrge.UndoRedo.Actions
{
    public class ItemModifiedAction : IUndoRedoAction
    {
        private readonly SelectableItem item;
        private readonly Vector3 previousPosition;
        private readonly Quaternion previousRotation;
        private readonly Vector3 previousScale;
        private readonly Vector3 newPosition;
        private readonly Quaternion newRotation;
        private readonly Vector3 newScale;

        public ItemModifiedAction(SelectableItem item, Vector3 previousPosition, Quaternion previousRotation, Vector3 previousScale)
        {
            this.item = item;
            this.previousPosition = previousPosition;
            this.previousRotation = previousRotation;
            this.previousScale = previousScale;
            
            var transform = item.transform;
            newPosition = transform.position;
            newRotation = transform.rotation;
            newScale = transform.localScale;
        }
        
        public void Undo()
        {
            var transform = item.transform;
            transform.position = previousPosition;
            transform.rotation = previousRotation;
            transform.localScale = previousScale;

            SaveItemData();
        }

        public void Redo()
        {
            var transform = item.transform;
            transform.position = newPosition;
            transform.rotation = newRotation;
            transform.localScale = newScale;

            SaveItemData();
        }

        public void Dispose()
        {
            
        }

        private void SaveItemData()
        {
            var transform = item.transform;
            item.ItemData.Position = transform.position;
            item.ItemData.Rotation = transform.rotation;
            item.ItemData.Scale = transform.localScale;
        }
    }
}
