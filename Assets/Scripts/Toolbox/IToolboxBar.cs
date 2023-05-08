using DG.Tweening;
using UnityEngine;

namespace CreativeUrge.Toolbox
{
    public interface IToolboxBar
    {
        const float ANIMATION_DURATION = 0.5f;
        RectTransform RectTransform { get; }
        void Show()
        {
            RectTransform.DOAnchorPosY(0, ANIMATION_DURATION);
        }
        
        void Hide()
        {
            RectTransform.DOAnchorPosY(-RectTransform.sizeDelta.y, ANIMATION_DURATION);
        }

        void ShowInstantly()
        {
            RectTransform.anchoredPosition = Vector2.zero;
        }
        
        void HideInstantly()
        {
            RectTransform.anchoredPosition = new Vector2(0, -RectTransform.sizeDelta.y);
        }
    }
}
