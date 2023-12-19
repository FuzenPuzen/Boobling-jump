public interface ICoinData 
{
    public int GetCoins();
    public bool SpendCoins(int coin);
    public void AddCoins(int coins);
}
