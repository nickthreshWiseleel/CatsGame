using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoseScreen : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}