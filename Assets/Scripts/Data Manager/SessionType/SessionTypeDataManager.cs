using MLALib;

public interface ISessionTypeDataManager
{
    public void SaveTutorialGameType(bool tutorial);
    public bool GetTutorialGameType();
}

public class SessionTypeDataManager : ISessionTypeDataManager
{
    private bool _tutorial;
    private const string TutorialKey = "tutorialKey";

    public SessionTypeDataManager()
    {
        SaveTutorialGameType(true);
        GetTutorialGameType();
    }

    public void SaveTutorialGameType(bool tutorial)
    {
         SaveLoader.SaveItem<bool>(tutorial, TutorialKey);
    }

    public bool GetTutorialGameType()
    {
        _tutorial = SaveLoader.LoadItem<bool>(TutorialKey);
        return _tutorial;
    }
}

