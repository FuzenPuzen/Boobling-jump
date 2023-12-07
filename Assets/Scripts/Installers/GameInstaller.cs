using Zenject;

public class GameInstaller : MonoInstaller
{   
    public override void InstallBindings()
    {
        Container.Bind<ConfigSO>().AsSingle().NonLazy();
        Container.Bind<PrefabsStorageService>().AsSingle();
        Container.Bind<IFabric>().To<Fabric>().AsSingle();

        Container.Bind<ITimerService>().To<TimerService>().AsSingle();
        Container.Bind<RecordService>().AsSingle();
        Container.Bind<ScoreService>().AsSingle();
        Container.Bind<TutorialService>().AsSingle();
        Container.Bind<EndPanelService>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviorService), typeof(Iservice)).To(typeof(PlayerBehaviorService)).AsSingle();

        Container.Bind<SectionsService>().AsSingle();

        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<EndGameState>().AsSingle();        
        Container.Bind<SuperJumpState>().AsSingle();
        Container.Bind<BasicGameState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
    }
}