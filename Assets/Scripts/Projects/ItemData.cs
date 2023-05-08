using System;
using CreativeUrge.Items;
using UnityEngine;

namespace CreativeUrge.Projects
{
    [Serializable]
    public class ItemData
    {
        [SerializeField] private ItemAsset itemAsset;
        [SerializeField] private Vector3 position;
        [SerializeField] private Quaternion rotation;
        [SerializeField] private Vector3 scale;
        
        public ItemAsset ItemAsset => itemAsset;
        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public Quaternion Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        public Vector3 Scale
        {
            get => scale;
            set => scale = value;
        }

        public ItemData(ItemAsset itemAsset, SelectableItem selectableItem)
        {
            this.itemAsset = itemAsset;
            
            var transform = selectableItem.transform;
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }
        
        public ItemData (ItemAsset itemAsset, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.itemAsset = itemAsset;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}
