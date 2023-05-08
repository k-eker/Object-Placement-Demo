using CreativeUrge.Camera;
using CreativeUrge.Items;
using CreativeUrge.Selection;
using CreativeUrge.UndoRedo;
using UnityEngine;

namespace CreativeUrge.Projects
{
    public class ProjectManager : MonoBehaviour
    {
        [SerializeField] private ProjectViewerUI projectViewerUI;
        [SerializeField] private ProjectSerializer projectSerializer;
        
        public ProjectData CurrentProject { get; private set; }
        public static ProjectManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void SaveCurrentProject()
        {
            if (CurrentProject == null)
            {
                return;
            }
            projectSerializer.SaveProjectData(CurrentProject);
        }

        public void CreateNewProject()
        {
            var projectData = new ProjectData();
             
            projectViewerUI.AddProject(projectData);
        
            projectSerializer.SaveProjectData(projectData);
        
            OpenProject(projectData);
        }

        public void OpenProject(ProjectData projectData)
        {
            CameraManager.Instance.ResetCamera();
            
            CurrentProject = projectData;
            
            projectViewerUI.HideViewerUI();

            ItemSpawner.Instance.SpawnItemsFromProjectData(projectData);
        }

        public void CloseCurrentProject()
        {
            SaveCurrentProject();
            TakeThumbnailSnapshot();
            
            CurrentProject = null;
            
            projectViewerUI.ShowViewerUI();
            
            SelectionManager.Instance.DeselectCurrent();
            
            ItemSpawner.Instance.ClearItems();
            
            UndoRedoSystem.Instance.Clear();
        }
        
        private void TakeThumbnailSnapshot()
        {
            if (CurrentProject == null)
            {
                return;
            }
            
            var texture = SnapshotGenerator.Instance.TakeSnapshot();

            CurrentProject.ThumbnailData.ThumbnailTexture = texture;
            
            projectViewerUI.GetProjectUI(CurrentProject).SetThumbnailTexture(texture);
        }

        private void OnApplicationQuit()
        {
            SaveCurrentProject();
        }
        
        private void OnApplicationFocus(bool focus)
        {
            SaveCurrentProject();
            TakeThumbnailSnapshot();
        }
        
        private void OnApplicationPause(bool pause)
        {
            SaveCurrentProject();
            TakeThumbnailSnapshot();
        }
    }
}
