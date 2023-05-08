using System;
using CreativeUrge.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CreativeUrge.Selection
{
	public class SelectionUI : MonoBehaviour
	{
		[SerializeField] private CanvasScaler canvasScaler;
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private RectTransform rectTransform;

		private ISelectable targetSelectable;

		private const float FADE_DURATION = 0.2f;
		private const float MIN_SIZE_DELTA = 500f;
		private const float LERP_SPEED = 20f;

		private void Update()
		{
			if (targetSelectable == null)
			{
				return;
			}

			SetPosition();
			SetSizeDelta();
		}

		private void SetPosition()
		{
			var targetPosition = UnityEngine.Camera.main.WorldToScreenPoint(targetSelectable.Transform.position);
			var currentPosition = transform.position;
			var lerpedPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * LERP_SPEED);
			transform.position = lerpedPosition;
		}

		private void SetSizeDelta()
		{
			var targetRect = RectUtility.CalculateScreenRect(targetSelectable.Renderer, canvasScaler);
			var currentSize = rectTransform.sizeDelta;
			
			var lerpedSize = Vector2.Lerp(currentSize, targetRect.size, Time.deltaTime * LERP_SPEED);
			lerpedSize.x = Mathf.Max(lerpedSize.x, MIN_SIZE_DELTA);
			lerpedSize.y = Mathf.Max(lerpedSize.y, MIN_SIZE_DELTA);
			
			rectTransform.sizeDelta = lerpedSize;
		}


		public void SetTarget(ISelectable selectable)
		{
			Debug.Log("setting target: " + selectable);
			targetSelectable = selectable;

			SetVisibility(selectable != null);
		}

		private void SetVisibility(bool visible)
		{
			canvasGroup.DOFade(visible ? 1 : 0, FADE_DURATION);

			SetInteractable(visible);
		}

		public void SetInteractable(bool interactable)
		{
			canvasGroup.interactable = interactable;
			canvasGroup.blocksRaycasts = interactable;
		}
	
	}
}