using Zenject;

public class GameInstaller : MonoInstaller
{  
    public override void InstallBindings()
    {
        Container.Bind<StateMachine>().AsSingle();
        Container.Bind<StartState>().AsSingle();

        //Container.Bind<PlayerController>().AsSingle();
        Container.Bind<PrefabsStorageService>().AsSingle();
        Container.Bind<PlayerFabric>().AsSingle();

        Container.Bind<SectionsService>().AsSingle();
        Container.Bind<SectionFabric>().AsSingle();
    }

    private void SpawnPlayer()
    {
        
    }
}