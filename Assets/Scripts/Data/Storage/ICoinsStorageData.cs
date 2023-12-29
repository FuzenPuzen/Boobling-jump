using System;

public interface ICoinsStorageData 
{
    public event Action<ICoinData> CoinDataChanged;
    public void SetCoinsSLData(ICoinData coinsSLData);
    public ICoinData GetCoinsSLData();

}
