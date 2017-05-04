using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EyeOfHitman
{
    public class DialogParticipant : MonoBehaviour {

        public SkinnedMeshRenderer ToSwap = null;
        public float MinMouthStateTime = 0.2f;
        public float MaxMouthStateTime = 0.5f;

        private Coroutine TalkingCoroutine = null;
        private bool IsTalking = false;
        private Texture CloseMouthTexture;
        private Texture OpenMouthTexture;

        void Start()
        {
            RefreshTextures();
        }
        public void SetTalking(bool talking)
        {
            if (IsTalking == talking)
                return;

            IsTalking = talking;
            if (!IsTalking)
            {
                if (TalkingCoroutine != null)
                    StopCoroutine(TalkingCoroutine);
                ToSwap.material.mainTexture = CloseMouthTexture;
            }
            else
            {
                TalkingCoroutine = StartCoroutine(Talk());
            }
        }

        private IEnumerator Talk()
        {
            bool mouthOpen = false;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(MinMouthStateTime, MaxMouthStateTime));
                ToSwap.material.mainTexture = mouthOpen ? OpenMouthTexture : CloseMouthTexture;
                mouthOpen = !mouthOpen;
            }
        }


        public Transform GetHeadTransform()
        {
            return ToSwap.transform;
        }

        public void RefreshTextures()
        {
            Texture headTexture = ToSwap.material.mainTexture;
            if (headTexture != null)
            {
                CloseMouthTexture = headTexture;
                string pathMouthOpen = "Textures/ClownFaces/" + headTexture.name + "_MouthOpen";
                OpenMouthTexture = (Texture)Resources.Load(pathMouthOpen, typeof(Texture));
            }
            else
                OpenMouthTexture = null;
        }
    }   

}