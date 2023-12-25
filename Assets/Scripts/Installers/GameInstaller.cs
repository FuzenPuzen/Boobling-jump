using Zenject;

public class GameInstaller : MonoInstaller
{   
    public override void InstallBindings()
    {
        Container.Bind<ConfigSO>().AsSingle().NonLazy();
        Container.Bind(typeof(IPrefabStorageService), typeof(ISOStorageService)).To(typeof(StorageService)).AsSingle();
        Container.Bind<IFabric>().To<Fabric>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviourStorageData), typeof(ICoinsStoradeData)).To(typeof(SessionStorageData)).AsSingle();
        Container.Bind<PlayerBehaviourDataCombiner>().AsSingle().NonLazy();        
        Container.Bind<CoinDataCombiner>().AsSingle().NonLazy();        
        

        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<CoinsPoolService>().AsSingle();
        Container.Bind<CoinsService>().AsSingle();
        Container.Bind<CoinsPanelService>().AsSingle();
        

        Container.Bind<ITimerService>().To<TimerService>().AsSingle();
        Container.Bind<RecordService>().AsSingle();
        Container.Bind<ScoreService>().AsSingle();
        Container.Bind<TutorialPanelService>().AsSingle();
        Container.Bind<EndPanelService>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviorService), typeof(Iservice)).To(typeof(PlayerBehaviorService)).AsSingle();
        

        Container.Bind<SectionsService>().AsSingle();

        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<EndGameState>().AsSingle();        
        Container.Bind<PreStartState>().AsSingle();        
        Container.Bind<SuperJumpState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.Bind<RollingState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
    }
}