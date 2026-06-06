using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIClueManager : MonoBehaviour
{
    [SerializeField] private RectTransform clue;
    [SerializeField] private GameObject tutorialHint;
    private bool tutorialHintHidden = false;
    private Vector2 hiddenPos = new(-580, -980);
    private Vector2 visiblePos = new(-580, -100);
    private bool isShowing = false;

    public void ToggleClue()
    {
        if (!isShowing) 
        {
            if(!tutorialHintHidden) 
            { 
                tutorialHint.SetActive(false); 
                tutorialHintHidden = true; 
            }
            clue.DOAnchorPos(visiblePos, 0.4f).SetEase(Ease.OutBack);
            isShowing = true;
        }
        else
        {
            clue.DOAnchorPos(hiddenPos, 0.3f).SetEase(Ease.InBack);
            isShowing = false;
        }
    }

}
