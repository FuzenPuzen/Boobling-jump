using System;

public interface ICoinDataManager
{
    public event Action<int> coinsChanged;
    public int GetCoins();
    public bool SpendCoins(int coins);
    public void AddCoins(int coins);

    public void CollectCoins(int coins = 1);
    public int GetSesionCollectedCoins();

}
