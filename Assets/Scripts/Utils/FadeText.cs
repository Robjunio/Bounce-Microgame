using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool fade;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Fade();
    }

    void Fade()
    {
        _text.alpha += fade ? 0.005f : -0.005f;
        if (_text.alpha <= 0.2 || _text.alpha >= 1)
        {
            fade = !fade;
        }
    }
}
