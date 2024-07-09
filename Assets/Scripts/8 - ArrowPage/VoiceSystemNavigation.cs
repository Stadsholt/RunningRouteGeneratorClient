using UnityEngine;

public class VoiceSystemNavigation : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    //private bool _isMuted = false;

    // Initialize audio clips and audio source on Awake
    void Awake()
    {

        // Load all audio clips from the "Audio" folder in Resources
        audioClips = Resources.LoadAll<AudioClip>("Audio");
        

        
        // If no AudioSource component found, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Get AudioClip by matching its name with the given instruction
    private AudioClip GetAudioClipByName(string instruction)
    {
        string instructionLower = instruction.ToLower();
        string clipName = "";
        int longestMatchLength = 0;

        // Find the clip name that matches the most with the instruction
        foreach (AudioClip clip in audioClips)
        {
            string clipInstruction = clip.name.ToLower();

            if (instructionLower.Contains(clipInstruction) && clipInstruction.Length > longestMatchLength)
            {
                longestMatchLength = clipInstruction.Length;
                clipName = clip.name;
            }
        }

        // Return the AudioClip with the matching name
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }

        return null;
    }

    // Play audio clip based on the given instruction
    public void PlayAudioOnInstrucs(string instruction)
    {
        //if (_isMuted == false)
        //{
            AudioClip clipToPlay = GetAudioClipByName(instruction);

            // Play the AudioClip if it is found
            if (clipToPlay != null)
            {
                // Stop the audio source if it is currently playing
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                // Play the AudioClip
                audioSource.clip = clipToPlay;
                audioSource.PlayOneShot(clipToPlay);
            }
            else
            {
                Debug.LogWarning("No audio clip found for instruction: " + instruction);
            }
            //}
    }

    //public void ToggleMute()
    //{
        //if (_isMuted == false)
        //{
        //    _isMuted = true;
            //}
            //else
            //{
            //    _isMuted = false;
            //}
        
            //}
}
