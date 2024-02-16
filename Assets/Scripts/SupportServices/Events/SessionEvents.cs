using EventBus;

public struct OnRollActivate : IEvent { }
public struct OnRollDeactivate : IEvent { }

public struct OnSupperJumpActivate : IEvent { }
public struct OnSupperJumpFall : IEvent { }
public struct OnSupperJumpDeactivate : IEvent { }

public struct OnStartBehaviourEnd : IEvent { }
public struct OnPlayerDie : IEvent { }
public struct OnPlayergriped : IEvent { }

public struct OnRestart: IEvent { }
public struct OnOpenMenu : IEvent { }
public struct OnTutorialFinish : IEvent { }

public struct OnTutorialMaxSection : IEvent { public int MaxSection; }

public struct OnRepaintAwake : IEvent { public IRepaint marker; }


