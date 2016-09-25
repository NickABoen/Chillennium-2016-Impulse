using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public AudioSource ambiance, eerie;
    public float fade_factor = 1;

    public enum AudioState { Ambiance, FadeToNothing, Nothing, FadeToEerie, Eerie}
    public AudioState current_state;
	// Use this for initialization
	void Start () {
        current_state = AudioState.Ambiance;
	}
	
	// Update is called once per frame
	void Update () {
        MessageSystem.enAggressiveness aggressiveness = gameObject.GetComponent<MessageSystem>().aggressiveness;
        switch (current_state)
        {
            case AudioState.Ambiance:
                if (!ambiance.isPlaying) ambiance.Play();
                if (eerie.isPlaying) eerie.Stop();

                if (aggressiveness != MessageSystem.enAggressiveness.Low) current_state = AudioState.FadeToNothing;
                break;
            case AudioState.Nothing:
                if (ambiance.isPlaying) ambiance.Stop();
                if (eerie.isPlaying) eerie.Stop();
                if (aggressiveness != MessageSystem.enAggressiveness.Moderate) current_state = AudioState.FadeToEerie;
                break;
            case AudioState.Eerie:
                if (ambiance.isPlaying) ambiance.Stop();
                if (!eerie.isPlaying) eerie.Play();
                break;
            case AudioState.FadeToNothing:
                if (CrossFadeStep(ambiance, null)) current_state = AudioState.Nothing;
                break;
            case AudioState.FadeToEerie:
                if (CrossFadeStep(null, eerie)) current_state = AudioState.Eerie;
                break;
        }
	}

    bool CrossFadeStep(AudioSource fade_out, AudioSource fade_in)
    {
        if ((fade_out == null || fade_out.volume == 0.0f) && (fade_in == null || fade_in.volume == 1.0f)) return true;

        float fade_step = fade_factor * Time.deltaTime;
        if (fade_out != null)
        {
            if (fade_out.isPlaying && fade_out.volume > 0.0) fade_out.volume = Mathf.Max(fade_out.volume - fade_step, 0.0f);
            if (fade_out.volume == 0 && fade_out.isPlaying) fade_out.Stop();
        }

        if(fade_in != null)
        {
            if (!fade_in.isPlaying) fade_in.Play();
            if (fade_in.volume < 1.0f) fade_in.volume = Mathf.Min(fade_in.volume + fade_step, 1.0f);
        }
        return false;
    }
}
