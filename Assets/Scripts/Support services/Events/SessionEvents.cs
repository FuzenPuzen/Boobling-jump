﻿using EventBus;

public struct OnRollActivate : IEvent { }
public struct OnRollDeactivate : IEvent { }

public struct OnSupperJumpActivate : IEvent { }
public struct OnSupperJumpFall : IEvent { }
public struct OnSupperJumpDeactivate : IEvent { }

public struct OnStartBehaviourEnd : IEvent { }
public struct OnPlayerDie : IEvent { }

public struct OnRestart: IEvent { }
public struct OnOpenMenu : IEvent { }

