public interface ISectionBehavioursService : IService
{
    public void SetBehavior<T>() where T : ISectionBehavior;
}
