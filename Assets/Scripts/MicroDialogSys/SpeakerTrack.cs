using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeOfHitman
{
	[System.Serializable]
	public class SpeakerEvent
	{
		public float time = 0;
		public int speakerIndex = 0;
	}

	[CreateAssetMenu(fileName = "Dialog", menuName = "EoH Dialog")]
	public class SpeakerTrack : ScriptableObject {

        public SpeakerTrack()
        {
            Events = new SpeakerEvent[1];
            SpeakerEvent evt = new SpeakerEvent();
            Events[0] = evt;
        }
		public uint ParticipantNumber = 2;
		public SpeakerEvent[] Events;
		public AudioClip DialogClip;
		public int Length
		{
			get{ return Events.Length;}
		}

		public int GetSpeakerAt(float time)
		{
			SpeakerEvent prevEv = null;
			foreach (SpeakerEvent evt in Events) {
				if (evt.time > time) {
					if (prevEv != null)
						return prevEv.speakerIndex;
					else
						return -1;
				}
				prevEv = evt;
			}
			return -1;
		}

		public int GetNextSpeaker(float time, out float timeTillNextSpeaker)
		{
			timeTillNextSpeaker = 0;
			foreach (SpeakerEvent evt in Events) {
				if (evt.time > time) {
					timeTillNextSpeaker = evt.time - time;
					return evt.speakerIndex;
				}
			}
			return -1;
		}
	}
}
