/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class NarrativeText : TriggerableObject {
	private const float MAX_TIME_BETWEEN_LETTERS = 0.05f;

	/*[SerializeField]
	[TextArea]*/
	private string m_TextToWrite = "";
	private int mIndexToWrite = 0;

	private TextMesh mTextMesh;

	private float timeBetweenLetters = 0f;

	protected override void Start () {
		base.Start ();
		mTextMesh = GetComponent<TextMesh> ();
		m_TextToWrite = mTextMesh.text;
		mTextMesh.text = "";
	}
	
	protected void Update()
	{
		WriteText ();
	}

	protected override void Trigger()
	{
		mIsTriggered = true;
	}

	private void WriteText()
	{
		if (mIsTriggered) {
			if (timeBetweenLetters >= MAX_TIME_BETWEEN_LETTERS)
			{
				timeBetweenLetters = 0f;
				WriteNextLetter();
			}
			else{
				timeBetweenLetters += Time.deltaTime;
			}
		}
	}

	private void WriteNextLetter()
	{
		char characterToWrite = m_TextToWrite [mIndexToWrite];
		if (characterToWrite.Equals (" ") || characterToWrite.Equals ('\n')) {
			mTextMesh.text += characterToWrite;
			mIndexToWrite++;
			WriteNextLetter ();
		} else {
			mTextMesh.text += characterToWrite;
			mIndexToWrite++;
		}

		if (mIndexToWrite >= m_TextToWrite.Length) {
			this.enabled = false;
		}
	}
}
