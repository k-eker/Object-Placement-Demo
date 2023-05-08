using UnityEngine;
using UnityEngine.UI;

namespace CreativeUrge.Utility
{
    public static class RectUtility
    {
	    public static Rect CalculateScreenRect(Renderer renderer, CanvasScaler canvasScaler)
	    {
		    var screenCorners = CalculateScreenSpaceCoordinates(renderer);

		    CalculateMinMaxScreenPositions(screenCorners, out var min, out var max);

		    var referenceResolutionX = canvasScaler.referenceResolution.x;
		    
		    min.x = min.x * referenceResolutionX / Screen.width;
		    max.x = max.x * referenceResolutionX / Screen.width;

		    return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
	    }

	    private static Vector3[] CalculateScreenSpaceCoordinates(Renderer renderer)
		{
			var screenCorner = new Vector3[8];
			var cam = UnityEngine.Camera.main;
			var bounds = renderer.bounds;
			
			screenCorner[0] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x,
				bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
			screenCorner[1] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x,
				bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
			screenCorner[2] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x,
				bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
			screenCorner[3] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x,
				bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));
			screenCorner[4] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x,
				bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
			screenCorner[5] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x,
				bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
			screenCorner[6] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x,
				bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
			screenCorner[7] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x,
				bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));
			
			return screenCorner;
		}

		private static void CalculateMinMaxScreenPositions(Vector3[] screenPositions, out Vector2 min, out Vector2 max)
		{
			min = new Vector2(screenPositions[0].x, screenPositions[0].y);
			max = new Vector2(screenPositions[0].x, screenPositions[0].y);

			for (int i = 1; i < screenPositions.Length; i++) {
				if (screenPositions[i].x < min.x) min.x = screenPositions[i].x;
				if (screenPositions[i].x > max.x) max.x = screenPositions[i].x;
				if (screenPositions[i].y < min.y) min.y = screenPositions[i].y;
				if (screenPositions[i].y > max.y) max.y = screenPositions[i].y;
			}
		}
    }
}
