using System.Collections.Generic;
using UnityEngine;

namespace CreativeUrge.Items
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "CREATIVE URGE/Item Database", order = 0)]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemAsset> items = new List<ItemAsset>();
        
        public List<ItemAsset> Items => items;
    }

}