using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRoot
{
    public class GameAutostarter
    {
        public static string StartScene;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            StartScene = SceneManager.GetActiveScene().name;

            if (SceneManager.GetActiveScene().name != Scenes.BOOT)
                SceneManager.LoadScene(Scenes.BOOT);
        }
    }
}