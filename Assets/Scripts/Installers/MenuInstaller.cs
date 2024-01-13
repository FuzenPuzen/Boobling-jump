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
        Container.Bind<RecordScoreDataCombiner>().AsSingle().NonLazy();

        Container.Bind<MenuMainPageCanvasViewService>().AsSingle().NonLazy();
        Container.Bind<MenuTutorialPanelViewService>().AsSingle().NonLazy();
        Container.Bind<MenuSkinShopPanelViewService>().AsSingle().NonLazy();
        Container.Bind<MenuUpgradePanelViewService>().AsSingle().NonLazy();

        Container.Bind<MenuSkinShopPageCanvasViewService>().AsSingle().NonLazy();


        Container.Bind<ICoinDataManager>().To<CoinDataManager>().AsSingle();
        Container.Bind<IScoreDataManager>().To<ScoreDataManager>().AsSingle();

        Container.Bind<MenuStateMachine>().AsSingle();
        Container.Bind<MenuStartState>().AsSingle();
        Container.Bind<MenuMainPageState>().AsSingle();
        Container.Bind<MenuSkinShopPage>().AsSingle();
        Container.Bind<MenuBonusPageState>().AsSingle();

    }
}
