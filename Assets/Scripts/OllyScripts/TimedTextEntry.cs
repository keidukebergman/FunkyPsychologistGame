using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimedTextEntry : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text textBox;

    [Header("Text")]
    [TextArea(5, 20)]
    [SerializeField] private string fullText;

    [Header("Settings")]
    [SerializeField] private int maxCharacters = 123;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float pauseBetweenPages = 1f;


    [SerializeField] private float maxCorruption = 100f;
    [SerializeField] private float glitchRefreshRate = 0.05f;

    private string currentTypedText = "";
    private Coroutine glitchCoroutine;
    [SerializeField] private FatigueManager fatigueManager;

    private void Start()
    {
        StartCoroutine(TypeText());
        glitchCoroutine = StartCoroutine(GlitchRoutine());
    }

    private string GlitchString(string original, float corruption)
    {
        char[] chars = original.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];

            // ❌ Never touch spaces or punctuation
            if (!char.IsLetter(c))
                continue;

            // chance of corruption
            if (Random.value > corruption)
                continue;

            // 👉 ONLY replace with other letters (no symbols)
            bool upper = char.IsUpper(c);

            char newChar;

            do
            {
                newChar = (char)Random.Range('a', 'z' + 1);
                if (upper)
                    newChar = char.ToUpper(newChar);

            } while (newChar == c); // avoid replacing with itself

            chars[i] = newChar;
        }

        return new string(chars);
    }

    IEnumerator TypeText()
    {
        int startIndex = 0;

        while (startIndex < fullText.Length)
        {
            int endIndex = Mathf.Min(startIndex + maxCharacters, fullText.Length);

            // Don't cut a word in half
            if (endIndex < fullText.Length)
            {
                while (endIndex > startIndex &&
                       !char.IsWhiteSpace(fullText[endIndex]))
                {
                    endIndex--;
                }

                // If there wasn't a whitespace in this range,
                // just cut normally to avoid an infinite loop.
                if (endIndex == startIndex)
                    endIndex = Mathf.Min(startIndex + maxCharacters, fullText.Length);
            }

            string page = fullText.Substring(startIndex, endIndex - startIndex).Trim();

            bool hasPrevious = startIndex > 0;
            bool hasNext = endIndex < fullText.Length;

            string displayText =
                (hasPrevious ? "... " : "") +
                page +
                (hasNext ? " ..." : "");

            textBox.text = "";

            foreach (char c in displayText)
            {
                currentTypedText += c;
                textBox.text = currentTypedText;
                yield return new WaitForSeconds(typingSpeed);
            }

            if (!hasNext)
                yield break;

            yield return new WaitForSeconds(pauseBetweenPages);

            startIndex = endIndex;

            // Skip whitespace before next page
            while (startIndex < fullText.Length &&
                   char.IsWhiteSpace(fullText[startIndex]))
            {
                startIndex++;
            }
        }
    }
    private IEnumerator GlitchRoutine()
    {
        while (true)
        {
            if (!string.IsNullOrEmpty(currentTypedText))
            {
                //corruption -- variable from fatigue
                float corruption =
                    Mathf.Clamp01(fatigueManager.GetFatigue() / maxCorruption);

                textBox.text = GlitchString(currentTypedText, corruption);
            }

            yield return new WaitForSeconds(glitchRefreshRate);
        }
    }
}

/*
 * using System.Collections;
using TMPro;
using UnityEngine;

public class TimedTextEntry : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text textBox;

    [Header("Settings")]
    [SerializeField] private int maxCharacters = 123;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float pauseBetweenPages = 1f;

    private string fullText = "";
    private Coroutine typingCoroutine;

    public void DisplayText(string newText)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        fullText = newText;
        textBox.text = "";

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        int startIndex = 0;

        while (startIndex < fullText.Length)
        {
            int endIndex = Mathf.Min(startIndex + maxCharacters, fullText.Length);

            
            if (endIndex < fullText.Length)
            {
                while (endIndex > startIndex &&
                       !char.IsWhiteSpace(fullText[endIndex]))
                {
                    endIndex--;
                }

                
                if (endIndex == startIndex)
                    endIndex = Mathf.Min(startIndex + maxCharacters, fullText.Length);
            }

            string page = fullText.Substring(startIndex, endIndex - startIndex).Trim();

            bool hasPrevious = startIndex > 0;
            bool hasNext = endIndex < fullText.Length;

            string displayText =
                (hasPrevious ? "... " : "") +
                page +
                (hasNext ? " ..." : "");

            textBox.text = "";

            foreach (char c in displayText)
            {
                textBox.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            if (!hasNext)
                yield break;

            yield return new WaitForSeconds(pauseBetweenPages);

            startIndex = endIndex;

            while (startIndex < fullText.Length &&
                   char.IsWhiteSpace(fullText[startIndex]))
            {
                startIndex++;
            }
        }

        typingCoroutine = null;
    }
}
*/