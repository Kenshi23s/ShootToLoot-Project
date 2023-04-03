using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    static AsyncOperation _LoadOperation;

    void LateUpdate()
    {
        if (_LoadOperation.isDone)
        {
            _LoadOperation.allowSceneActivation = true;
        }
    }

    public static void LoadASyncLevel(int index)
    {
        SceneManager.LoadScene(3);

        _LoadOperation = SceneManager.LoadSceneAsync(index);
        _LoadOperation.allowSceneActivation = false;
    }

    public static void LoadASyncLevel(int index, LevelData MainLevelData)
    {
        SceneManager.LoadScene(3);

        _LoadOperation = SceneManager.LoadSceneAsync(index);

        MapCreatorManager.instance.ConfigMapCreator(MainLevelData);

        _LoadOperation.allowSceneActivation = false;
    }
}
