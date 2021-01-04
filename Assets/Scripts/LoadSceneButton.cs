using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public string LevelName;

        public void LoadTargetScene() 
        {

            //Client clt = FindObjectOfType<Client>();
            //if (clt != null)
            //{
            //    Debug.Log("destroy");
            //    Destroy(clt);
            //    Destroy(clt.uiCanvas);
            //}
            

            Client.SceneName = LevelName;
            Debug.Log(Client.SceneName);
            SceneManager.LoadSceneAsync(SceneName);
        }

        public void LoadMenuScene()
        {

            Client clt = FindObjectOfType<Client>();
            clt.EndGame();
            
            Destroy(Client.ca.gameObject);
            Destroy(Client.cli.gameObject);

            Client.SceneName = LevelName;
            Debug.Log(Client.SceneName);
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}
