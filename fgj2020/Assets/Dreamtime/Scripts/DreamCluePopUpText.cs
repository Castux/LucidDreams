using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DreamCluePopUpText : MonoBehaviour
{
    public TextMeshProUGUI dreamText;

    private bool showing = false;
    public float showTime;
    private float showTimer;

    private bool hiding = false;
    public float hideTime;
    private float hideTimer;
    public float hideDelay;

    private Material textMaterial = null;

    private void Awake()
    {
        Clear();
    }

    public void ShowClue(string clueText)
    {
        textMaterial = new Material(dreamText.fontSharedMaterial);
        dreamText.SetText(clueText);
        SetHideValues(1f);
        dreamText.enabled = true;

        StartShowAnimation();

        Invoke("StartHideAnimation", hideDelay);
    }

    public void StartShowAnimation()
    {
        if (showTime <= 0f)
        {
            Debug.LogWarning("Show time is 0. No animation shown.");
            SetShowValues(1f);
            return;
        }

        showing = true;
        showTimer = 0f;
    }

    public void StartHideAnimation()
    {
        if (hideTime <= 0f)
        {
            Debug.LogWarning("Show time is 0. No animation shown.");
            SetHideValues(1f);
            return;
        }

        hiding = true;
        hideTimer = 0f;
    }

    private void Update()
    {
        if (showing)
        {
            if (showTimer < showTime)
            {
                SetShowValues(showTimer / showTime);
                showTimer += Time.deltaTime;
            }
            else
            {
                SetShowValues(1f);
                showTimer = 0f;
                showing = false;
            }
        }
        else if (hiding)
        {
            if (hideTimer < hideTime)
            {
                SetHideValues(hideTimer / hideTime);
                hideTimer += Time.deltaTime;
            }
            else
            {
                SetHideValues(1f);
                hideTimer = 0f;
                hiding = false;
                Clear();
            }
        }
    }

    void SetShowValues(float progress)
    {
        // if (progress < 0.3f)
        // {
        //     dreamText.material.SetFloat(ShaderUtilities.ID_soft, 1f);
        // }
        // else
        // {
        //     dreamText.material.SetFloat("_Softness", Mathf.Lerp(1f, 0f, (progress - 0.3f) / 0.7f));
        // }

        dreamText.fontSharedMaterial = textMaterial;

        if (progress < 0.7f)
        {
            dreamText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 1f);
        }
        else
        {
            dreamText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Lerp(1f, 0.5f, (progress - 0.7f) / 0.3f));
        }

        dreamText.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, progress / 0.8f));
    }

    void SetHideValues(float progress)
    {
        // if (progress < 0.7f)
        // {
        //     dreamText.material.SetFloat("_Softness", 0f);
        // }
        // else
        // {
        //     dreamText.material.SetFloat("_Softness", Mathf.Lerp(0f, 1f, (progress - 0.7f) / 0.3f));
        // }

        dreamText.fontSharedMaterial = textMaterial;

        dreamText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, Mathf.Lerp(0.5f, 1f, progress / 0.3f));

        if (progress < 0.2f)
        {
            dreamText.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            dreamText.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, (progress - 0.2f) / 0.8f));
        }
    }

    void Clear()
    {
        dreamText.SetText("");
        dreamText.enabled = false;
    }
}
