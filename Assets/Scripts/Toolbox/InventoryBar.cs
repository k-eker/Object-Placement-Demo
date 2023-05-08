using CreativeUrge.Items;
using UnityEngine;

namespace CreativeUrge.Toolbox
{
    public class InventoryBar : MonoBehaviour, IToolboxBar
    {
        [SerializeField] private Transform itemContainer;
        [SerializeField] private ItemUI itemUIPrefab;
        [SerializeField] private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;

        private void Reset()
        {
            rectTransform = this.GetComponent<RectTransform>();
        }
        
        public void AddItem(ItemAsset itemAsset)
        {
            var itemUI = Instantiate(itemUIPrefab, itemContainer);
            itemUI.Initialize(itemAsset);
        }
    }
}
