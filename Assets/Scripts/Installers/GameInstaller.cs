using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IMarkerService>().To<MarkerService>().AsSingle();
        Container.Bind<ILoaderSceneService>().To<LoaderSceneService>().AsSingle();

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

        Container.Bind<ScoreRewardDataCombiner>().AsSingle();
        Container.Bind<PlayerBehaviourDataCombiner>().AsSingle().NonLazy();
        Container.Bind<CoinDataCombiner>().AsSingle().NonLazy();
        Container.Bind<RecordScoreDataCombiner>().AsSingle().NonLazy();
        Container.Bind<GiftScoreDataCombiner>().AsSingle().NonLazy();
        Container.Bind<PlayerSkinDataCombiner>().AsSingle().NonLazy();

        Container.Bind<IPoolsViewService>().To<PoolsViewService>().AsSingle();
        Container.Bind<IPlayerBehaviourDataManager>().To<PlayerBehaviourDataManager>().AsSingle();
        Container.Bind<IPlayerSkinDataManager>().To<PlayerSkinDataManager>().AsSingle();


        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<IScoreDataManager>().To<ScoreDataManager>().AsSingle();

        Container.Bind<SuperJumpWavesService>().AsSingle();

        Container.Bind<RoomViewService>().AsSingle();
        Container.Bind<BlackBoardViewService>().AsSingle();
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
        Container.Bind<EndPageViewService>().AsSingle();
        Container.Bind<TutorialEndPageViewService>().AsSingle();
        Container.Bind<TutorialEndPanelViewService>().AsSingle();
        Container.Bind<TutorialEndService>().AsSingle();

        Container.Bind(typeof(IPlayerBehaviourService)).To(typeof(PlayerBehaviourService)).AsSingle();
        Container.Bind(typeof(ISessionTypeDataManager)).To(typeof(SessionTypeDataManager)).AsSingle();

        Container.Bind<PlayerStoolDestroyerService>().AsSingle();

        Container.Bind<InfinitySectionPoolViewService>().AsSingle();
        Container.Bind<RollSectionPoolViewService>().AsSingle();
        Container.Bind<TutorialSectionPoolViewService>().AsSingle();
        Container.Bind<SuperJumpSectionPoolViewService>().AsSingle();

        Container.Bind<SectionSimpleJumpBehaviour>().AsSingle();
        Container.Bind<SectionRollBehaviour>().AsSingle();
        Container.Bind<SectionSuperJumpBehaviour>().AsSingle();
        Container.Bind<ISectionBehavioursService>().To<SectionBehavioursService>().AsSingle();

        Container.Bind<SessionStateMachine>().AsSingle();
        Container.Bind<EndGameState>().AsSingle();
        Container.Bind<TutorialFinishState>().AsSingle();
        Container.Bind<PreStartState>().AsSingle();
        Container.Bind<SuperJumpState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.Bind<RollingState>().AsSingle();
        Container.Bind<StartState>().AsSingle();
        Container.Bind<SessionLastState>().AsSingle();
    }
}