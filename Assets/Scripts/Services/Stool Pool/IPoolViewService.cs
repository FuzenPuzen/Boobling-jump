public interface IPoolViewService
{
    public SectionView GetSection();
    public void ReturnSection(SectionView section);
}