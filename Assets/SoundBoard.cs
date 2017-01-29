using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundBoard : MonoBehaviour {

    public AudioSource Speaker1;
    public AudioSource Speaker2;

    public List<AudioClip> songs;

    public int songIdx;
    private bool m_paused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsPlaying()
    {
        return Speaker1.isPlaying;
    }

    public void PauseSong()
    {
        Speaker1.Pause();
        Speaker2.Pause();
        m_paused = true;
    }

    public void NextSong()
    {
        songIdx++;
        if (songIdx >= songs.Count)
        {
            songIdx = 0;
        }
        m_paused = false;
        if (Speaker1.isPlaying)
        {
            PlaySong();
        }        
    }

    public void PreviousSong()
    {
        songIdx++;
        if (songIdx < 0)
        {
            songIdx = songs.Count - 1;
        }
        m_paused = false;
        if (Speaker1.isPlaying)
        {
            PlaySong();
        }
    }

    public void PlaySong()
    {
        if (m_paused)
        {
            Speaker1.UnPause();
            Speaker2.UnPause();
        } else
        {
            Speaker1.Stop();
            Speaker2.Stop();
            Speaker1.clip = songs[songIdx];
            Speaker2.clip = songs[songIdx];
            Speaker1.Play();
            Speaker2.Play();
        }
    }

}
