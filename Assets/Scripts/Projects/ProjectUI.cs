using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CreativeUrge.Projects
{
    public class ProjectUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI projectTitleText;
        [SerializeField] private Button openProjectButton;
        [SerializeField] private Image thumbnailImage;
        public ProjectData ProjectData { get; private set; }

        public void Initialize(ProjectData projectData)
        {
            ProjectData = projectData;
            
            projectTitleText.text = projectData.DisplayName;
            SetThumbnailTexture(projectData.ThumbnailData.ThumbnailTexture);
            
            openProjectButton.onClick.RemoveAllListeners();
            openProjectButton.onClick.AddListener(OnOpenProjectButtonClicked);
        }

        public void SetThumbnailTexture(Texture2D texture)
        {
            if (texture == null)
            {
                return;
            }

            thumbnailImage.sprite = SnapshotGenerator.Instance.ConvertTextureToSprite(texture);
        }
        
        private void OnOpenProjectButtonClicked()
        {
            ProjectManager.Instance.OpenProject(ProjectData);
        }
        
        private void OnDestroy()
        {
            openProjectButton.onClick.RemoveAllListeners();
        }
    }
}
