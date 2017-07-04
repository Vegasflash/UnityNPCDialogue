using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour
{
    public TextAsset textFile;
    public string[] textLines;

	// Use this for initialization
	void Start ()
    {
        textLines = (textFile.text.Split('\n'));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
