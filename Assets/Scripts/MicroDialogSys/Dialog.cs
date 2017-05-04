using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EyeOfHitman
{
	public class Dialog : MonoBehaviour {

		public SpeakerTrack Track;
		public DialogParticipant[] Participants;
		public AudioSource DialogSoundSource;


		private DialogParticipant CurrentSpeaker = null;
		private Coroutine coroutine;	

		public void StartDialog()
		{
            if (Track == null)
                return;

            StopDialog();
            StartCoroutine (PlayDialog ());
		}

        public void StopDialog()
        {
            if (coroutine != null)
            {
                DialogSoundSource.Stop();
                StopCoroutine(coroutine);
            }
        }

        public bool IsPlaying()
		{
			return coroutine != null;
		}

		private IEnumerator PlayDialog()
		{
            float time = 0;
			DialogSoundSource.clip = Track.DialogClip;
			DialogSoundSource.Play ();
			SpeakerEvent nextEv = null;
			for (int nextEventIndex = 0; nextEventIndex < Track.Length;++nextEventIndex) {
				nextEv = Track.Events[nextEventIndex];
				while (nextEv.time > time) {
					yield return null;
					time = DialogSoundSource.time;
				}
				Debug.Log ("setting speaker :" + nextEv.speakerIndex + " at time " + nextEv.time);
				if (nextEv.speakerIndex == -1)
					ShutUpCurrentSpeaker ();
				else
					SetSpeaker (nextEv.speakerIndex);
			}

			Debug.Log ("Waiting for end");
			//wait for tOhe end of the  clip.
			yield return new WaitForSeconds (DialogSoundSource.clip.length - time);

			Debug.Log ("Deactivating Last speaker");
			//stop last speaker
			ShutUpCurrentSpeaker();
		}


		public void ShutUpCurrentSpeaker()
		{
			if(CurrentSpeaker != null)
				CurrentSpeaker.SetTalking(false);
			if (DialogSoundSource != null)
				DialogSoundSource.transform.parent = null;

			CurrentSpeaker = null;
		}
		public void SetSpeaker(int index)
		{
			if(index >= Participants.Length)
			{
				Debug.LogError("Trying to make participant #" + index + " the speaker but there are only " + (Participants.Length - 1) + " participants");
				return;
			}
			DialogParticipant newParticipant = Participants[index];
			if(newParticipant == null)
			{
				Debug.LogError("Speaker of " + index + "is null");
				return;
			}

			ShutUpCurrentSpeaker ();

			CurrentSpeaker = newParticipant;
			CurrentSpeaker.SetTalking(true);
			if (DialogSoundSource != null) {
				DialogSoundSource.transform.parent = CurrentSpeaker.GetHeadTransform ();
				DialogSoundSource.transform.localPosition = Vector3.zero;
			}
		}
	}
}