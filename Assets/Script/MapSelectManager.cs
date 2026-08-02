using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour
{
    public void LoadMap(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}