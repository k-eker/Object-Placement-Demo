using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.Utility
{
    public static class TouchUtility
    {
        public static bool IsPointerOverUIObject()
        {
            var eventSystem = EventSystem.current;
            var eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;
            
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(eventData, results);
            
            return results.Count > 0;
        }
    }
}
