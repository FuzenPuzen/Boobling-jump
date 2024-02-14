using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SectionBehavioursService : ISectionBehavioursService
{
    private List<ISectionBehaviour> _sectionBehaviours = new();
    private ISectionBehaviour _currentBehaviour;

    private ISectionBehaviour _sectionSimpleJumpBehaviour;
    private ISectionBehaviour _sectionSuperJumpBehaviour;
    private ISectionBehaviour _sectionRollBehaviour;
    

    public void ActivateService()
    {
        _sectionBehaviours.Add(_sectionSimpleJumpBehaviour);
        _sectionBehaviours.Add(_sectionSuperJumpBehaviour);
        _sectionBehaviours.Add(_sectionRollBehaviour);
    }

    [Inject]
    public void Constructor(SectionSimpleJumpBehaviour sectionSimpleJumpBehaviour,
                            SectionRollBehaviour sectionRollBehaviour,
                            SectionSuperJumpBehaviour sectionSuperJumpBehaviour)
    {
        _sectionSuperJumpBehaviour = sectionSuperJumpBehaviour;
        _sectionSimpleJumpBehaviour = sectionSimpleJumpBehaviour;
        _sectionRollBehaviour = sectionRollBehaviour;
        _sectionRollBehaviour = sectionRollBehaviour;
    }

    public void SetBehaviour<T>() where T : ISectionBehaviour
    {
        _currentBehaviour?.StopBehaviour();
        _currentBehaviour = _sectionBehaviours.OfType<T>().FirstOrDefault();
        _currentBehaviour.StartBehaviour();
    }

    public void DeactivateService()
    {
        _currentBehaviour?.StopBehaviour();
    }

}
