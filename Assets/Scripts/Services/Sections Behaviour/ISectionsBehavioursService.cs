public interface ISectionBehaviorService : IService
{
    public void SetBehavior<T>() where T : ISectionBehavior;
}
