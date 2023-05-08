using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CreativeUrge.Projects
{
    public class ProjectViewerUI : MonoBehaviour
    {
        [SerializeField] private RectTransform projectViewerContainer;
        [SerializeField] private ProjectUI projectUIPrefab;
        [SerializeField] private RectTransform projectUIContainer;
        [SerializeField] private Button newProjectButton;
        [SerializeField] private Button closeProjectButton;

        private List<ProjectUI> projectUIs = new List<ProjectUI>();

        private const float SLIDE_DURATION = 0.2f;
        
        private void Awake()
        {
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            newProjectButton.onClick.AddListener(OnNewProjectButtonClicked);
            closeProjectButton.onClick.AddListener(OnCloseProjectButtonClicked);
        }

        private void OnCloseProjectButtonClicked()
        {
            ProjectManager.Instance.CloseCurrentProject();
        }

        private void OnNewProjectButtonClicked()
        {
            ProjectManager.Instance.CreateNewProject();
        }

        public void InitializeProjects(IEnumerable<ProjectData> projectData)
        {
            ClearExistingProjects();
            
            foreach (var data in projectData)
            {
                AddProject(data);
            }
        }

        private void ClearExistingProjects()
        {
            foreach (Transform child in projectUIContainer.transform)
            {
                if (child.GetComponent<ProjectUI>())
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public void AddProject(ProjectData projectData)
        {
            var projectUI = Instantiate(projectUIPrefab, projectUIContainer);
            projectUI.Initialize(projectData);
            
            newProjectButton.transform.SetSiblingIndex(projectUIContainer.childCount);
            
            projectUIs.Add(projectUI);
        }

        private void OnDestroy()
        {
            newProjectButton.onClick.RemoveAllListeners();
            closeProjectButton.onClick.RemoveAllListeners();
        }

        public void HideViewerUI()
        {
            projectViewerContainer.DOAnchorPosX(-projectViewerContainer.rect.width, SLIDE_DURATION);
        }

        public void ShowViewerUI()
        {
            projectViewerContainer.DOAnchorPosX(0, SLIDE_DURATION);
        }

        public ProjectUI GetProjectUI(ProjectData projectData)
        {
            foreach (var projectUI in projectUIs)
            {
                if (projectUI.ProjectData == projectData)
                {
                    return projectUI;
                }
            }

            return null;
        }
    }
}
