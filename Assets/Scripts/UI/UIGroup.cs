using UnityEngine;

[RequireComponent(typeof(UIFade))]
public class UIGroup : MonoBehaviour
{
    protected UIFade m_fade;

    protected virtual void Awake()
    {
        m_fade = GetComponent<UIFade>();
        m_fade.SetOpacity(0);
    }

    public virtual void Start()
    {

    }

    public virtual void Show()
    {
        m_fade.Show();
    }

    public virtual void Hide()
    {
        m_fade.Hide();
    }
}
