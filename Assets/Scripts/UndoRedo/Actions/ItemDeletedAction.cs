using CreativeUrge.Items;
using UnityEngine;

namespace CreativeUrge.UndoRedo.Actions
{
    public class ItemDeletedAction : IUndoRedoAction
    {
        private readonly SelectableItem item;
        private readonly Vector3 position;

        public ItemDeletedAction(SelectableItem item, Vector3 position)
        {
            this.item = item;
            this.position = position;
        }
        
        public void Undo()
        {
            ItemSpawner.Instance.EnableItem(item);
            item.transform.position = position;
        }

        public void Redo()
        {
            ItemSpawner.Instance.DisableItem(item);
        }

        public void Dispose()
        {
            if (item != null && !item.gameObject.activeSelf)
            {
                Object.Destroy(item.gameObject);
            }
        }
    }
}
