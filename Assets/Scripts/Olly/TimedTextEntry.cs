using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimedTextEntry : MonoBehaviour {
	public TMP_Text targetText;
	public string fullText = "This is a test.";
	public float letterDelay = 0.2f;

	// Use this for initialization
	void Start () {
		targetText.text = "";
		StartCoroutine (EnterFullText ());
	}

	IEnumerator EnterFullText () {
		char[] fullTxtArray = fullText.ToCharArray();
		string nextTxt = "";
		for (int i = 0; i < fullTxtArray.Length; i++){
			nextTxt += fullTxtArray[i];

			targetText.text = nextTxt;

			yield return new WaitForSeconds(letterDelay);
		}
	}
}