using System.Collections.Generic;
using CreativeUrge.Items;
using UnityEngine;

namespace CreativeUrge.Toolbox
{
    public class ToolboxManager : MonoBehaviour
    {
        [SerializeField] private ItemDatabase itemDatabase;
        [SerializeField] private ToolboxUI toolboxUI;

        private List<IToolboxBar> bars = new List<IToolboxBar>();
        public IToolboxBar ActiveBar { get; private set; }
        
        public static ToolboxManager Instance { get; private set; }
        public bool IsHoveringOverDelete => ActiveBar is DeleteBar;

        private void Awake()
        {
            Instance = this;
            
            DetectAllBars();
            HideAllBarsInstantly();
            
            InitializeInventory();
        }

        private void DetectAllBars()
        {
            foreach (Transform child in toolboxUI.transform)
            {
                var bar = child.GetComponent<IToolboxBar>();
                if (bar != null)
                {
                    bars.Add(bar);
                }
            }
        }
        
        private void HideAllBarsInstantly()
        {
            foreach (var bar in bars)
            {
                bar.HideInstantly();
            }
        }

        private IToolboxBar GetBar<T>() where T: IToolboxBar
        {
            foreach (var bar in bars)
            {
                if (bar is T)
                {
                    return bar;
                }
            }

            return null;
        }
        
        public void ShowBar<T>() where T: IToolboxBar
        {
            if (ActiveBar != null)
            {
                ActiveBar.Hide();
            }

            var bar = GetBar<T>();
            if (bar != null)
            {
                bar.Show();
                ActiveBar = bar;
            }
        }
        
        private void InitializeInventory()
        {
            var inventoryBar = GetBar<InventoryBar>() as InventoryBar;
            
            foreach (var itemData in itemDatabase.Items)
            {
                inventoryBar.AddItem(itemData);
            }
            
            ShowBar<InventoryBar>();
        }
    }
}
