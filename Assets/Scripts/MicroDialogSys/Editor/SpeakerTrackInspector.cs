using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace EyeOfHitman
{
    [CustomEditor(typeof(SpeakerTrack))]
    //[CanEditMultipleObjects]
    public class SpeakerTrackInspector : Editor
    {
        void OnEnable()
        {
            // TODO: find properties we want to work with
            //serializedObject.FindProperty();
        }

	
		bool playing = false;
		private Type GetAudioUtil()
		{
			Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
			return unityEditorAssembly.GetType("UnityEditor.AudioUtil");

		}

		private void Call(AudioClip clip, string fctionName)
		{
			Type audioUtilClass = GetAudioUtil();
			MethodInfo method = audioUtilClass.GetMethod(
				fctionName,
				BindingFlags.Static | BindingFlags.Public,
				null,
				new System.Type[] {
					typeof(AudioClip)
				},
				null
			);
			method.Invoke(
				null,
				new object[] {
					clip
				}
			);
					
		}


		private T Get<T>(AudioClip clip, string fctionName)
		{
			Type audioUtilClass = GetAudioUtil();
			MethodInfo method = audioUtilClass.GetMethod(
				fctionName,
				BindingFlags.Static | BindingFlags.Public,
				null,
				new System.Type[] {
					typeof(AudioClip)
				},
				null
			);
			return (T) method.Invoke(
				null,
				new object[] {
					clip
				}
			);
		}

		void AddSpeaker( SpeakerTrack myScript, int speakerIndex, float time)
		{
			SpeakerEvent[] n = new SpeakerEvent[myScript.Length + 1];

			SpeakerEvent ev = new SpeakerEvent ();
			ev.speakerIndex = speakerIndex;
			ev.time = time;
			bool newEvtAdded = false;
			int i = 0;
            foreach (SpeakerEvent ev1 in myScript.Events) {
				if (!newEvtAdded && ev.time < ev1.time) {
					n [i++] = ev;
					newEvtAdded = true;
				} else {
					n [i++] = ev1;
				}
			}
            if (newEvtAdded == false)
                n[n.Length - 1] = ev;

            myScript.Events = n;
		}

		private bool paused = false;
        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

			SpeakerTrack myScript = (SpeakerTrack)target;
            playing = false;
            if (myScript.DialogClip != null)
                playing = Get<bool> (myScript.DialogClip, "IsClipPlaying");

			if (!playing) {
				if (myScript.DialogClip != null && GUILayout.Button ("Play")) {
					playing = true;
					Call (myScript.DialogClip, "PlayClip");
					playing = true;
				}
				if (GUILayout.Button ("Clear Speaker Track")) {
					myScript.Events = new SpeakerEvent[0];
				}
			}

			if(playing)
			{
                EditorUtility.SetDirty(target);
                float time = Get<float>(myScript.DialogClip, "GetClipPosition");

				if (!paused) {
					if (GUILayout.Button ("Pause")) {
						Call (myScript.DialogClip, "PauseClip");
						paused = true;
					}
				} else {
					if (GUILayout.Button ("Resume")) {
						Call (myScript.DialogClip, "ResumeClip");
						paused = false;
					}

				}
				if (GUILayout.Button ("Stop")) {
					Call (myScript.DialogClip, "StopClip");
					playing = false;
				}

				int currentSpeaker = myScript.GetSpeakerAt (time);
				float timeTillNextSpeaker = 0;
				int nextSpeaker = myScript.GetNextSpeaker (time, out timeTillNextSpeaker);
				for (int i = 0; i < myScript.ParticipantNumber; i++) {
					string label = "" + (i + 1);
					if(i == currentSpeaker)
						label = "->"+label;
					if(i == nextSpeaker)
						label = "next:"+label;
					
					if(GUILayout.Button(label))
						AddSpeaker (myScript,i, time);
				}

				string label2 = "Silence";
				if(-1 == currentSpeaker)
					label2 = "->"+label2 ;
				if(-1 == nextSpeaker)
					label2 = "next:"+label2;


				if (GUILayout.Button (label2)) {
					AddSpeaker (myScript,-1, time);					
				}
                

                GUILayout.HorizontalSlider (time, 0, myScript.DialogClip.length); 
				GUILayout.Label (time +" / " + myScript.DialogClip.length);
				GUILayout.Label ("Next Speaker (" +((nextSpeaker==-1)?"None":(""+(nextSpeaker+1)))+ ") in " + timeTillNextSpeaker);


			}



			//EditorGUILayout.PropertyField();
            DrawDefaultInspector();

            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
