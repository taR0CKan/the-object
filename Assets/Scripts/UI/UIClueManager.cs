using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIClueManager : MonoBehaviour
{
    [SerializeField] private RectTransform clue;
    [SerializeField] private GameObject tutorialHint;
    private bool tutorialHintHidden = false;
    [SerializeField] private RectTransform hiddenPos;
    [SerializeField] private RectTransform visiblePos;
    private bool isShowing = false;

    public void ToggleClue()
    {
        Debug.Log(isShowing);
        if (!isShowing) 
        {
            if(!tutorialHintHidden) 
            { 
                tutorialHint.SetActive(false); 
                tutorialHintHidden = true; 
            }
            clue.DOAnchorPos(visiblePos.position, 0.4f).SetEase(Ease.OutBack);
            isShowing = true;
        }
        else
        {
            clue.DOAnchorPos(hiddenPos.position, 0.3f).SetEase(Ease.InBack);
            isShowing = false;
        }
    }

}
