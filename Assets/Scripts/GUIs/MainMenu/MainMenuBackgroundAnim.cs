using UnityEngine;
using DG.Tweening;

public class MainMenuBackgroundAnim : MonoBehaviour
{
    Sequence sequence;
    [SerializeField] RectTransform bg;

    private void Start()
    {
        sequence = DOTween.Sequence();

        sequence.Append(bg.DOAnchorPos(Vector2.right * 100, 10f))
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);

        sequence.Play();
    }
}