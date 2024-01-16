using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMarkerService>().To<MarkerService>().AsSingle();

        Container.Bind(
            typeof(IPrefabStorageService),
            typeof(ISOStorageService)
            ).To(typeof(StorageService)).AsSingle();

        Container.Bind<IViewFabric>().To<ViewFabric>().AsSingle();
        Container.Bind<IServiceFabric>().To<ServiceFabric>().AsSingle();

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

        Container.Bind<IPoolsViewService>().To<PoolsViewService>().AsSingle();


        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<IScoreDataManager>().To<ScoreDataManager>().AsSingle();
        Container.Bind<RoomViewService>().AsSingle();

        Container.Bind<GiftService>().AsSingle();
        Container.Bind<GiftCollectorViewService>().AsSingle();
        Container.Bind<CoinCollectorViewService>().AsSingle();

        Container.Bind<CoinsPanelService>().AsSingle();
        Container.Bind<CoinPalleteViewService>().AsSingle();
        Container.Bind<SuperJumpBonusBlenderViewService>().AsSingle();
        Container.Bind<RollBonusBlenderViewService>().AsSingle();


        Container.Bind<ITimerService>().To<TimerService>().AsSingle();
        Container.Bind<RecordScorePanelService>().AsSingle();
        Container.Bind<CurrentScorePanelService>().AsSingle();
        Container.Bind<GiftScorePanelService>().AsSingle();
        Container.Bind<TutorialPanelService>().AsSingle();
        Container.Bind<EndPanelService>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviourService)).To(typeof(PlayerBehaviourService)).AsSingle();
        Container.Bind(typeof(ISessionTypeDataManager)).To(typeof(SessionTypeDataManager)).AsSingle();

        Container.Bind<InfinityStoolPoolViewService>().AsSingle();
        Container.Bind<RollStoolPoolViewService>().AsSingle();
        Container.Bind<TutorialStoolPoolViewService>().AsSingle();
        Container.Bind<SuperJumpStoolPoolViewService>().AsSingle();

        Container.Bind<SectionSimpleJumpBehaviour>().AsSingle();
        Container.Bind<SectionRollBehaviour>().AsSingle();
        Container.Bind<SectionSuperJumpBehaviour>().AsSingle();
        Container.Bind<ISectionBehavioursService>().To<SectionBehavioursService>().AsSingle();

        Container.Bind<SessionStateMachine>().AsSingle();
        Container.Bind<EndGameState>().AsSingle();
        Container.Bind<PreStartState>().AsSingle();
        Container.Bind<SuperJumpState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.Bind<RollingState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
    }
}