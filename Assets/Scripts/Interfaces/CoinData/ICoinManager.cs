using System;

public interface ICoinManager
{
    public event Action<int> coinsChanged;
    public int GetCoins();
    public bool SpendCoins(int coins);
    public void AddCoins(int coins);

}
