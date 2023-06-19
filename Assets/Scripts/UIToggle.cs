using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private RectTransform handle;
    [SerializeField] private bool onAwake;

    /// <summary>
    /// �g�O���̒l
    /// </summary>
    public bool Value;

    private float handlePosX;
    private Sequence sequence;

    private static readonly Color OFF_BG_COLOR = new Color(144/256f, 144/256f, 144/256f);
    private static readonly Color ON_BG_COLOR = new Color(0.2f, 0.84f, 0.3f);

    private const float SWITCH_DURATION = 0.36f;

    private void Start()
    {
        handlePosX = Mathf.Abs(handle.anchoredPosition.x);
        Value = onAwake;
        UpdateToggle(0);
    }

    /// <summary>
    /// �g�O���̃{�^���A�N�V�����ɐݒ肵�Ă���
    /// </summary>
    public void SwitchToggle()
    {
        Value = !Value;
        UpdateToggle(SWITCH_DURATION);
    }

    /// <summary>
    /// ��Ԃ𔽉f������
    /// </summary>
    private void UpdateToggle(float duration)
    {
        var bgColor = Value ? ON_BG_COLOR : OFF_BG_COLOR;
        var handleDestX = Value ? handlePosX : -handlePosX;

        sequence?.Complete();
        sequence = DOTween.Sequence();
        sequence.Append(backgroundImage.DOColor(bgColor, duration))
            .Join(handle.DOAnchorPosX(handleDestX, duration / 2));
        if (Value == true)
        {
            //Value��true�̎��̏����i�K�C�h��\������A�Ƃ��j������
        }
        else
        {
            //Value��false�̎��̏����i�K�C�h���폜����A�Ƃ��j������
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
