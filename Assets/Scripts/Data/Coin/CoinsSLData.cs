public class CoinsSLData : ICoinData
{
    private int _coins;

    public int GetCoins()
    {
        return _coins;
    }

    public bool SpendCoins(int coin)
    {
        if (_coins < coin) return false;
        _coins -= coin;
        return true;
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
    }
}
