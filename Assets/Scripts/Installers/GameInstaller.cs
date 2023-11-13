using Zenject;

public class GameInstaller : MonoInstaller
{   
    public override void InstallBindings()
    {
        Container.Bind<PrefabsStorageService>().AsSingle();
        Container.Bind<IFabric>().To<Fabric>().AsSingle();
        Container.Bind<Loader>().AsSingle();

        Container.Bind<ITimerService>().To<TimerService>().AsSingle();
        Container.Bind<RecordService>().AsSingle();
        Container.Bind<ScoreService>().AsSingle();
        Container.Bind<TutorialService>().AsSingle();
        Container.Bind<EndPanelService>().AsSingle();

        Container.Bind<PlayerKitService>().AsSingle();
    
        Container.Bind<SectionsService>().AsSingle();
        
        Container.Bind<EndGameState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
        Container.Bind<StateMachine>().AsSingle();
    }
}