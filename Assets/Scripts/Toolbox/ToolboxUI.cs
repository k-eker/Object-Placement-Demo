using CreativeUrge.Selection;
using CreativeUrge.Toolbox;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.Toolbox
{
    public class ToolboxUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SelectionManager.Instance.Dragged != null)
            {
                ToolboxManager.Instance.ShowBar<DeleteBar>();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SelectionManager.Instance.Dragged != null)
            {
                ToolboxManager.Instance.ShowBar<InventoryBar>();
            }
        }
    }
}
