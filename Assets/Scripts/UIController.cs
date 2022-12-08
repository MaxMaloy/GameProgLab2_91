using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform livesContainer;
    [SerializeField] private RectTransform timerContainer;
    [SerializeField] private RectTransform escContainer;
    [SerializeField] private RectTransform menu;
    private bool isMenuVisible = true;

    [SerializeField] private List<Image> hearts;
    [SerializeField] private TextMeshProUGUI timer;

    private void Awake()
    {
        SetHudVisibility(false, 0);
        SetMenuVisibility(false, 0);
    }

    public void SetHudVisibility(bool visibility, float animSpeed = 1)
    {
        livesContainer.DOAnchorPosY(visibility ?  -20:200, 0.5f * animSpeed).SetEase(Ease.OutBounce);
        timerContainer.DOAnchorPosY(visibility ? -150:200, 0.5f * animSpeed).SetEase(Ease.OutBounce);
        escContainer.DOAnchorPosY(visibility ? 0:200, 0.5f * animSpeed).SetEase(Ease.OutBounce);
    }

    public void ToggleMenuVisibility()
    {
        SetMenuVisibility(!isMenuVisible);
    }

    public void SetMenuVisibility(bool visibility, float animSpeed = 1)
    {
        if (isMenuVisible == visibility)
        {
            return;
        }
        SetHudVisibility(!visibility, .5f);
        isMenuVisible = visibility;
        GameManager.Instance.InputController.InputModeType = InputModeType.Blocked;
        menu.DOScale(visibility ? 1 : 0, 0.5f * animSpeed).SetEase(Ease.OutBounce).OnComplete(()=>GameManager.Instance.InputController.InputModeType = visibility ? InputModeType.UI : InputModeType.Gameplay);
    }

    public void ContinueMenuButton()
    {
        SetMenuVisibility(false);
    }

    public void ExitMenuButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
