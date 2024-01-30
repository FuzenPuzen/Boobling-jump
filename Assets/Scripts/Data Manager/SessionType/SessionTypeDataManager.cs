using MLALib;
using EventBus;

public interface ISessionTypeDataManager
{
    public void SaveGameType(bool tutorial, int TutorialMaxSection);
    public bool GetTutorialGameType();
    public int GetTutorialMaxSection();
}

public class SessionTypeDataManager : ISessionTypeDataManager
{
    private SessionData _sessionData;
    private const string TutorialKey = "tutorialKey";

    public SessionTypeDataManager()
    {
        GetTutorialGameType();
    }

    public void SaveGameType(bool tutorial, int TutorialMaxSection = 0)
    {
        _sessionData.IsTutorial = tutorial;
        _sessionData.TutorialMaxSection = TutorialMaxSection;
        SaveLoader.SaveItem<SessionData>(_sessionData, TutorialKey);
    }

    public bool GetTutorialGameType()
    {
        _sessionData = SaveLoader.LoadData<SessionData>(_sessionData, TutorialKey);
        if (_sessionData == null)
        {
            _sessionData = new();
            _sessionData.IsTutorial = true;
            _sessionData.TutorialMaxSection = 0;
            SaveLoader.SaveItem<SessionData>(_sessionData, TutorialKey);
        }
        return _sessionData.IsTutorial;
    }

    public int GetTutorialMaxSection()
    {
        return _sessionData.TutorialMaxSection;
    }
}

public class SessionData
{
    public bool IsTutorial;
    public int TutorialMaxSection;
}

