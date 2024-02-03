using EventBus;

public struct OnClickGame : IEvent{ }
public struct OnClickSkinShop : IEvent{ }
public struct OnClickUpgrade : IEvent{ }
public struct OnClickMenu : IEvent{ }
public struct OnTryBuySkin : IEvent{ public PlayerSkinData playerSkinData; }
public struct OnBuySkin : IEvent{ public PlayerSkinData playerSkinData; }
public struct OnChangeSkin : IEvent{ public PlayerSkinData playerSkinData; }
public struct OnMarkerAwake : IEvent { public Marker marker; }


