using System;
using UnityEngine;

namespace CreativeUrge.Projects
{
    [Serializable]
    public class ThumbnailData
    {
        [SerializeField] private byte[] textureRaw;
        [SerializeField] private int width;
        [SerializeField] private int height;

        public int Width => width;
        public int Height => height;

        public Texture2D ThumbnailTexture
        {
            get
            {
                if (textureRaw == null || textureRaw.Length == 0)
                {
                    return null;
                }

                var texture = new Texture2D(Width, Height, TextureFormat.RGB24, false);
                texture.LoadRawTextureData(textureRaw);
                texture.Apply();
                return texture;
            }
            set
            {
                textureRaw = value.GetRawTextureData();
                width = value.width;
                height = value.height;
            }
        }
    }
}
