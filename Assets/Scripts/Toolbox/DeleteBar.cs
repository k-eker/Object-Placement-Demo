using UnityEngine;

namespace CreativeUrge.Toolbox
{
    public class DeleteBar : MonoBehaviour, IToolboxBar
    {
        [SerializeField] private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform;

        private void Reset()
        {
            rectTransform = this.GetComponent<RectTransform>();
        }
    }
}
