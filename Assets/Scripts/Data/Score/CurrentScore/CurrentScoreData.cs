public class CurrentScoreData : ICurrentScoreData
{
    private int _score = 0;

    public int Score { get => _score; set => _score = value; }
}
