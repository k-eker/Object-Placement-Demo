using System;
using System.Collections.Generic;
using UnityEngine;

namespace CreativeUrge.Projects
{
    [Serializable]
    public class ProjectData
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private List<ItemData> items = new List<ItemData>();
        [SerializeField] private ThumbnailData thumbnailData;

        public string Id => id;
        public string DisplayName => displayName;
        public List<ItemData> Items => items;
        
        public ThumbnailData ThumbnailData => thumbnailData;

        public ProjectData()
        {
            id = Guid.NewGuid().ToString();
            displayName = GetCurrentTimeString();
            thumbnailData = new ThumbnailData();
        }

        private string GetCurrentTimeString()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
    }
}