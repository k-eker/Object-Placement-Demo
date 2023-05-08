using UnityEngine;

namespace CreativeUrge.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "CREATIVE URGE/Item", order = 1)]
    public class ItemAsset : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private SelectableItem selectableItemPrefab;
        
        public Sprite Icon => icon;
        public SelectableItem SelectableItemPrefab => selectableItemPrefab;
    }

}