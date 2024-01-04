using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ConfigSO>().AsSingle().NonLazy();
        Container.Bind(
            typeof(IPrefabStorageService),
            typeof(ISOStorageService)
            ).To(typeof(StorageService)).AsSingle();
        Container.Bind<IFabric>().To<Fabric>().AsSingle();

        Container.Bind(
            typeof(IPlayerBehaviourStorageData),
            typeof(ICoinsStorageData),
            typeof(ICurrentScoreStorageData),
            typeof(IRecordScoreStorageData),
            typeof(IGiftScoreStorageData)
            ).To(typeof(SessionStorageData)).AsSingle();

        Container.Bind<PlayerBehaviourDataCombiner>().AsSingle().NonLazy();
        Container.Bind<CoinDataCombiner>().AsSingle().NonLazy();
        Container.Bind<RecordScoreDataCombiner>().AsSingle().NonLazy();
        Container.Bind<GiftScoreDataCombiner>().AsSingle().NonLazy();


        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<IScoreDataManager>().To<ScoreDataManager>().AsSingle();
        Container.Bind<IRoomViewManager>().To<RoomViewManager>().AsSingle().NonLazy();

        Container.Bind<CoinPoolViewManager>().AsSingle();
        Container.Bind<CoinsService>().AsSingle();
        Container.Bind<GiftService>().AsSingle();
        Container.Bind<CoinsPanelService>().AsSingle();


        Container.Bind<ITimerService>().To<TimerService>().AsSingle();
        Container.Bind<RecordScorePanelService>().AsSingle();
        Container.Bind<CurrentScorePanelService>().AsSingle();
        Container.Bind<GiftScorePanelService>().AsSingle();
        Container.Bind<TutorialPanelService>().AsSingle();
        Container.Bind<EndPanelService>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviorService)).To(typeof(PlayerBehaviorService)).AsSingle();

        Container.Bind<InfinityStoolPoolViewService>().AsSingle();

        Container.Bind<SectionSimpleJumpBehaviour>().AsSingle();
        Container.Bind<ISectionBehavioursService>().To<SectionBehavioursService>().AsSingle();

        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<EndGameState>().AsSingle();
        Container.Bind<PreStartState>().AsSingle();
        Container.Bind<SuperJumpState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.Bind<RollingState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
    }
}