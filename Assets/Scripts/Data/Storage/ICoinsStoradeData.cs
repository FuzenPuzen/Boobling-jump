using System;

public interface ICoinsStoradeData 
{
    public event Action<ICoinData> CoinDataChanged;
    public void SetCoinsSLData(ICoinData coinsSLData);
    public ICoinData GetCoinsSLData();

}
