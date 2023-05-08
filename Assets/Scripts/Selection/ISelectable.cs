using UnityEngine;

namespace CreativeUrge.Selection
{
    public interface ISelectable
    {
        Transform Transform { get; }
        Renderer Renderer { get; }
        void SetInteractable(bool interactable);
        void OnSelect();
        void OnDeselect();
    }
}
