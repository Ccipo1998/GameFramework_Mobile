using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingAnimator : MonoBehaviour
{
    [SerializeField]
    private float _DelaySeconds;
    
    private TextMeshProUGUI _LoadingTitle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoadingAnimation());
    }

    private IEnumerator StartLoadingAnimation()
    {
        _LoadingTitle = FindObjectOfType<TextMeshProUGUI>();

        for (int i = 1; i < 3; i = (i + 1) % 3)
        {
            yield return new WaitForSeconds(_DelaySeconds);

            _LoadingTitle.text = "Loading" + new string('.', i + 1);
        }
    }
}
