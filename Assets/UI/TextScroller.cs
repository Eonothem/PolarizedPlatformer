using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour {
	public Text textBox;
	//Store all your text in this string array
	string[] goatText = new string[]{"Flintstones.","Meet the Flintstones.", "They're the modern stone age family.","From the", "town of Bedrock,","They're a page right out of history."};
	int currentlyDisplayingText = 0;
	private int charPerFrame = 1;

	void Awake () {
		StartCoroutine(AnimateText());
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.E)){
			SkipToNextText ();
		}
	}
	//This is a function for a button you press to skip to the next text
	public void SkipToNextText(){
		StopAllCoroutines();
		currentlyDisplayingText++;
		//If we've reached the end of the array, do anything you want. I just restart the example text
		if (currentlyDisplayingText>=goatText.Length) {
			currentlyDisplayingText=0;
		}
		StartCoroutine(AnimateText());
	}
	//Note that the speed you want the typewriter effect to be going at is the yield waitforseconds (in my case it's 1 letter for every      0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
	IEnumerator AnimateText(){

		for (int i = 0; i < (goatText[currentlyDisplayingText].Length+1); i=i+charPerFrame)
		{
			textBox.text = goatText[currentlyDisplayingText].Substring(0, i);
			yield return null;
		}
		//Debug.Log ("AAA");
		textBox.text = goatText[currentlyDisplayingText].Substring(0, goatText[currentlyDisplayingText].Length);
		//for (int i = 0; i < 60; i++) {
			//Debug.Log (i);
			//yield return null
		//}
	}
}
