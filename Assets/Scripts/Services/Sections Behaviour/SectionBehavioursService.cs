using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SectionBehavioursService : ISectionBehavioursService
{
    private List<ISectionBehaviour> _sectionBehaviours = new();
    private ISectionBehaviour _currentBehaviour;

    private ISectionBehaviour _sectionSimpleJumpBehaviour;
    private ISectionBehaviour _sectionRollBehaviour;
    

    public void ActivateService()
    {
        _sectionBehaviours.Add(_sectionSimpleJumpBehaviour);
        _sectionBehaviours.Add(_sectionRollBehaviour);
    }

    [Inject]
    public void Constructor(SectionSimpleJumpBehaviour sectionSimpleJumpBehaviour,
                            SectionRollBehaviour sectionRollBehaviour)
    {
        _sectionSimpleJumpBehaviour = sectionSimpleJumpBehaviour;
        _sectionRollBehaviour = sectionRollBehaviour;
    }

    public void SetBehaviour<T>() where T : ISectionBehaviour
    {
        if (_currentBehaviour != null)
            _currentBehaviour.StopBehaviour();
        _currentBehaviour = _sectionBehaviours.OfType<T>().FirstOrDefault();
        _currentBehaviour.StartBehaviour();
    }
}
