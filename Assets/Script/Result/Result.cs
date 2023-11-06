using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    enum Phase
    {
        ITEM_COLLECTION,
        CLEAR,
        CONTINUE
    }

    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] TextMeshProUGUI itemCounter;

    int collectGoal;
    int collected;

    float collectNumIncreasing;
    [SerializeField] float collectNumTime = 0.5f;
        float collectNumSpeed;

    float clearImageDelay = 0.0f;
    [SerializeField] float clearImageDelayMax = 0.5f;

    bool isClear;
    [SerializeField] Image clearImage;
    [SerializeField] Image failImage;
    [SerializeField] float animationSpeed = 0.5f;
    [Min(1.0f)]
    [SerializeField] float animationMaxSize = 1.2f;
    Image resultImage = null;
    float animationDelta = 0.0f;

    bool active = false;
    Phase activePhase = Phase.ITEM_COLLECTION;

    private void Awake()
    {
        GameResult r = GameManager.Instance.GetLastGameResult();
        stageText.text = "스테이지 : " + r.stageName;
        collectGoal = r.collectGoal;
        collected = r.collectedItems;
        isClear = r.isClear;

        collectNumSpeed = (collected) / collectNumTime;

        itemCounter.text = "0/" + collectGoal;
    }

    private void Update()
    {
        if (!active)
        {
            if (Input.anyKeyDown)
            {
                GameManager.Instance.ForceFadeIn();
                active = true;
            }
            if (!GameManager.Instance.IsFading)
            {
                active = true;
            }
            return;
        }
        if (activePhase == Phase.ITEM_COLLECTION)
        {
            if (Input.anyKeyDown) TapSkip();

            collectNumIncreasing = Mathf.Min(collectNumIncreasing + Time.deltaTime * collectNumSpeed, collected);
            itemCounter.text = (int)collectNumIncreasing + "/" + collectGoal;

            clearImageDelay += Time.deltaTime;
            if (clearImageDelay >= clearImageDelayMax && collectNumIncreasing >= collected)
            {
                if (isClear) resultImage = clearImage;
                else resultImage = failImage;
                resultImage.gameObject.SetActive(true);
                activePhase = Phase.CLEAR;
            }
        }
        else if (activePhase == Phase.CLEAR)
        {
            if (Input.anyKeyDown) TapSkip();

            animationDelta += Time.deltaTime * animationSpeed;
            animationDelta = Mathf.Min(1.0f, animationDelta);

            Vector2 size = new Vector2(animationMaxSize - animationDelta * (animationMaxSize - 1.0f)
                , animationMaxSize - animationDelta * (animationMaxSize - 1.0f));
            Color color = Color.white;
            color.a = animationDelta;

            resultImage.rectTransform.localScale = size;
            resultImage.color = color;

            if (animationDelta >= 1.0f)
                activePhase = Phase.CONTINUE;
        }
        else
        {
            if (Input.anyKeyDown)
                GameManager.Instance.ChangeScene("Lobby");
        }
    }

    private void TapSkip()
    {
        clearImageDelay = clearImageDelayMax;
        collectNumIncreasing = collected;
        animationDelta = 1.0f;
    }
}
