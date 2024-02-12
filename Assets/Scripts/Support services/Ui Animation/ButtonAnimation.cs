using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ButtonAnimation : MonoBehaviour
{
    private Button button;
    private Sequence _punchSequence;

    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartButtonAnimation);
    }

    public void StartButtonAnimation()
    {
        _punchSequence?.Kill();
        button.transform.localScale = Vector3.one;
        _punchSequence = DOTween.Sequence();
        _punchSequence.Append(button.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 2, 0));
    }

    public void OnDestroy()
    {
        _punchSequence?.Kill();
    }

}
