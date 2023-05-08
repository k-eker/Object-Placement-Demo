using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CreativeUrge.Projects
{
    public class ProjectSerializer : MonoBehaviour
    {
        [SerializeField] private ProjectViewerUI projectViewerUI;
        
         private const string SAVE_FOLDER = "Projects";
        
        private static string ProjectsPath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, SAVE_FOLDER);
            }
        }
        
        private void Awake()
        {
            InitializeProjects();
        }

        public void InitializeProjects()
        {
            var projects = LoadAllProjectData();
            projectViewerUI.InitializeProjects(projects);
        }
        
        public void ClearProjectData() {
            DirectoryInfo di = new DirectoryInfo(ProjectsPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete(); 
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true); 
            }
        }
        
        private List<ProjectData> LoadAllProjectData()
        {
            var projectsPath = ProjectsPath;
        
            if (!Directory.Exists(projectsPath))
            {
                Directory.CreateDirectory(projectsPath);
            }
        
            DirectoryInfo directory = new DirectoryInfo(projectsPath);
        
            var projects = new List<ProjectData>();
        
            foreach (var file in directory.GetFiles("*.json"))
            {
                var contents = File.ReadAllText(file.ToString());
                var projectData = JsonUtility.FromJson<ProjectData>(contents);
        
                projects.Add(projectData);
            }

            return projects;
        }
        
        public void SaveProjectData(ProjectData projectData)
        {
            var json = JsonUtility.ToJson(projectData);
            var path = Path.Combine(ProjectsPath, projectData.Id + ".json");
        
            File.WriteAllText(path, json);
        }
       
    }
}
