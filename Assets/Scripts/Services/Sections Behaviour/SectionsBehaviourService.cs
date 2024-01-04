using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SectionsBehaviourService : ISectionBehaviorService
{
    private List<ISectionBehavior> _sectionBehaviors = new();
    private ISectionBehavior _currentBehavior;

    private ISectionBehavior _sectionSimpleJumpBehaviour;
    

    public void ActivateService()
    {
        _sectionBehaviors.Add(_sectionSimpleJumpBehaviour);
    }

    [Inject]
    public void Constructor(SectionSimpleJumpBehaviour sectionSimpleJumpBehaviour)
    {
        _sectionSimpleJumpBehaviour = sectionSimpleJumpBehaviour;
    }

    public void SetBehavior<T>() where T : ISectionBehavior
    {
        if (_currentBehavior != null)
            _currentBehavior.StopBehavior();
        _currentBehavior = _sectionBehaviors.OfType<T>().FirstOrDefault();
        _currentBehavior.StartBehavior();
    }
}
