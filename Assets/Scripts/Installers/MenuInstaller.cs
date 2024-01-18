using Zenject;

public class MenuInstaller : MonoInstaller
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

        Container.Bind<CoinDataCombiner>().AsSingle().NonLazy();
        Container.Bind<PlayerBehaviourDataCombiner>().AsSingle().NonLazy();
        Container.Bind<RecordScoreDataCombiner>().AsSingle().NonLazy();
        Container.Bind<IPlayerBehaviourDataManager>().To<PlayerBehaviourDataManager>().AsSingle();

        Container.Bind<MenuMainPageCanvasViewService>().AsSingle();

        Container.Bind<MenuSkinShopPageCanvasViewService>().AsSingle();

        Container.Bind<MenuUpgradePageCanvasViewService>().AsSingle();

        Container.Bind<MenuCoinPageCanvasViewService>().AsSingle();


        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<IScoreDataManager>().To<ScoreDataManager>().AsSingle();

        Container.Bind<MenuStateMachine>().AsSingle();
        Container.Bind<MenuStartState>().AsSingle();
        Container.Bind<MenuMainPageState>().AsSingle();
        Container.Bind<MenuSkinShopPage>().AsSingle();
        Container.Bind<MenuUpgradePageState>().AsSingle();

    }
}
