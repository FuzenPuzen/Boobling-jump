public interface ISectionBehavioursService : IService
{
    public void SetBehaviour<T>() where T : ISectionBehaviour;
    public void DeactivateService();
}
