using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Zenject;

public interface ILoaderSceneService : IService
{

}

public class LoaderSceneService : ILoaderSceneService
{
    public static LoaderSceneService Instance { get; private set; }
    private GameScenes _bufScene;

    public void ActivateService()
    {
        Instance = this;
    }

    public void LoadBufScene()
	{
		SceneManager.LoadSceneAsync((int)_bufScene);
	}

	public void SetBufScene(GameScenes gameScenes)
	{
        _bufScene = gameScenes;
    }
}

public enum GameScenes
{
    MenuScene = 0,
	SessionScene = 1,
	TestScene = 2
}
