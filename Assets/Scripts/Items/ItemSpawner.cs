using System.Collections.Generic;
using CreativeUrge.Projects;
using CreativeUrge.Selection;
using UnityEngine;

namespace CreativeUrge.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        private List<SelectableItem> spawnedItems = new List<SelectableItem>();
        public static ItemSpawner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnItemsFromProjectData(ProjectData projectData)
        {
            foreach (var itemData in projectData.Items)
            {
                var selectableItem = SpawnItem(itemData);
            }
        }
        
        private SelectableItem SpawnItem(ItemData itemData)
        {
            var item = Instantiate(itemData.ItemAsset.SelectableItemPrefab);
            
            var itemTransform = item.transform;
            itemTransform.position = itemData.Position;
            itemTransform.rotation = itemData.Rotation;
            itemTransform.localScale = itemData.Scale;

            item.Initialize(itemData);
            
            spawnedItems.Add(item);

            return item;
        }

        public void ClearItems()
        {
            foreach (var item in spawnedItems)
            {
                if (item == null)
                {
                    continue;
                }
                
                Destroy(item.gameObject);
            }
            
            spawnedItems.Clear();
        }

        public SelectableItem SpawnItemWithoutSave(ItemAsset itemAsset)
        {
            var item = Instantiate(itemAsset.SelectableItemPrefab);
            
            spawnedItems.Add(item);

            return item;
        }

        public void EnableItem(SelectableItem item)
        {
            item.gameObject.SetActive(true);
            
            ProjectManager.Instance.CurrentProject.Items.Add(item.ItemData);
        }

        public void DisableItem(SelectableItem item)
        {
            SelectionManager.Instance.DeselectCurrent();
            
            item.gameObject.SetActive(false);
            
            ProjectManager.Instance.CurrentProject.Items.Remove(item.ItemData);
        }
    }
}
