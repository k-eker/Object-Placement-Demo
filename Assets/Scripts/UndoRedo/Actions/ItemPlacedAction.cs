using CreativeUrge.Items;
using UnityEngine;

namespace CreativeUrge.UndoRedo.Actions
{
    public class ItemPlacedAction : IUndoRedoAction
    {
        private readonly SelectableItem item;
        
        public ItemPlacedAction(SelectableItem item)
        {
            this.item = item;
        }
        
        public void Undo()
        {
            ItemSpawner.Instance.DisableItem(item);
        }

        public void Redo()
        {
            ItemSpawner.Instance.EnableItem(item);
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