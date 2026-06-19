using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CG_Paint.Data.Scene;

namespace Project_CG_Paint.Services
{
    public class SceneManager
    {
        public List<Scene> Scenes { get; private set; } = new List<Scene>();
        public Scene CurrentScene { get; private set; } = new Scene();
        public void AddScene(Scene scene)
        {
            Scenes.Add(scene);
            if (CurrentScene == null)
            {
                CurrentScene = scene;
            }
        }
    }
}
