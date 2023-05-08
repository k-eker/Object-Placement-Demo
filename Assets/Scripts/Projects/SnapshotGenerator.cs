using UnityEngine;

namespace CreativeUrge.Projects
{
    public class SnapshotGenerator : MonoBehaviour
    {
        public static SnapshotGenerator Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public Texture2D TakeSnapshot()
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
            var mainCamera = UnityEngine.Camera.main;
            
            mainCamera.targetTexture = renderTexture;
            mainCamera.Render();
            
            var texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            RenderTexture.active = renderTexture;
            
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            mainCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(renderTexture);
            
            return texture;
        }
        
        public Sprite ConvertTextureToSprite(Texture2D texture)
        {
            var rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture, rect, pivot, 100.0f);
        }
    }
}
