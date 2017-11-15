using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace DaleranGames.UI
{
    public class LoadSceneOnClick : MonoBehaviour
    {

        public void LoadByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadByName(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
