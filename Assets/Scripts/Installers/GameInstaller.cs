using Zenject;

public class GameInstaller : MonoInstaller
{   
    public override void InstallBindings()
    {
        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<StartState>().AsSingle();

        Container.Bind<PrefabsStorageService>().AsSingle();
        Container.Bind<TimerService>().AsSingle();

        Container.Bind<PlayerKitService>().AsSingle();

        Container.Bind<IFabric>().To<Fabric>().AsSingle();
        Container.Bind<SectionsService>().AsSingle();
    }
}