using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class HUDChoice : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] _textFields;

    private UIGroup _group;

    public UIGroup Group => _group;

    private void Awake()
    {
        _group = GetComponent<UIGroup>();
    }

    public void PresentChoices(string[] choices)
    {
        _group.Show();

        for (int i = 0; i < _textFields.Length; i++)
        {
            if (i > choices.Length - 1)
            {
                _textFields[i].transform.parent.gameObject.SetActive(false);
            }
            else
            {
                _textFields[i].text = choices[i];
                _textFields[i].transform.parent.gameObject.SetActive(true);
            }
        }
    }

    public void OnChoiceMade()
    {
        _group.Hide();
    }

}
