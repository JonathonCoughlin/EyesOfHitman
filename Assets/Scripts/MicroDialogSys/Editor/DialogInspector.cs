using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace EyeOfHitman
{
    [CustomEditor(typeof(Dialog))]
    //[CanEditMultipleObjects]
    public class DialogInspector : Editor
    {
        void OnEnable()
        {
            // TODO: find properties we want to work with
            //serializedObject.FindProperty();
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            // TODO: Draw UI here
            //EditorGUILayout.PropertyField();
			DrawDefaultInspector();
			
            Dialog diag = (Dialog)target;
            if (EditorApplication.isPlaying) {
				if (!diag.IsPlaying () && GUILayout.Button ("Play")) {
					diag.StartDialog ();
				}
			}

            ConversationTable table = diag.GetComponent<ConversationTable>();
            if (diag.Track == null && table != null && table.m_speakerMonologue != null)
            {
                if (GUILayout.Button("Port from Conversation"))
                {
                    SpeakerTrack track = new SpeakerTrack();
                    track.name = table.m_speakerMonologue.name;
                    track.DialogClip = table.m_speakerMonologue;
                    track.ParticipantNumber = (uint)diag.Participants.Length;
                    diag.Track = track;
                    AssetDatabase.CreateAsset(track, "Assets/"+track.name+".asset");
                }
            }

            if (GUILayout.Button("Auto-Feed Participants from Children"))
            {
                diag.Participants = diag.GetComponentsInChildren<DialogParticipant>();
            }


                // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
                serializedObject.ApplyModifiedProperties();
        }
    }
}
