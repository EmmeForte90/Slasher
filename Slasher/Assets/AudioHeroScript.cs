using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Spine.Unity;
using Spine;

public class AudioHeroScript : MonoBehaviour
{
    [Header("Audio")]
    [HideInInspector] public float basePitch = 1f;
    [HideInInspector] public float randomPitchOffset = 0.1f;
    [SerializeField] public AudioClip[] listSound; // array di AudioClip contenente tutti i suoni che si vogliono riprodurre
    private AudioSource[] sgm; // array di AudioSource che conterrà gli oggetti AudioSource creati
    public AudioMixer SFX;
    private bool sgmActive = false;

    private SkeletonAnimation _skeletonAnimation;
    private Spine.AnimationState _spineAnimationState;
    private Spine.Skeleton _skeleton;
    Spine.EventData eventData;

private void Awake()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (_skeletonAnimation == null) {
            Debug.LogError("Componente SkeletonAnimation non trovato!");
        }        
        _spineAnimationState = GetComponent<Spine.Unity.SkeletonAnimation>().AnimationState;
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.skeleton;
        //rb = GetComponent<Rigidbody2D>();
        sgm = new AudioSource[listSound.Length]; // inizializza l'array di AudioSource con la stessa lunghezza dell'array di AudioClip
        for (int i = 0; i < listSound.Length; i++) // scorre la lista di AudioClip
        {
            sgm[i] = gameObject.AddComponent<AudioSource>(); // crea un nuovo AudioSource come componente del game object attuale (quello a cui è attaccato lo script)
            sgm[i].clip = listSound[i]; // assegna l'AudioClip corrispondente all'AudioSource creato
            sgm[i].playOnAwake = false; // imposto il flag playOnAwake a false per evitare che il suono venga riprodotto automaticamente all'avvio del gioco
            sgm[i].loop = false; // imposto il flag playOnAwake a false per evitare che il suono venga riprodotto automaticamente all'avvio del gioco

        }
 // Aggiunge i canali audio degli AudioSource all'output del mixer
        foreach (AudioSource audioSource in sgm)
        {
        audioSource.outputAudioMixerGroup = SFX.FindMatchingGroups("Master")[0];
        }
        }


//EVENTS
 public void PlayMFX(int soundToPlay)
    {
        if (!sgmActive)
        {
            sgm[soundToPlay].pitch = Random.Range(.9f, 1.1f);
            sgm[soundToPlay].Play();
           // sgmActive = true;
        }
    }
public void StopMFX(int soundToPlay)
    {
        if (!sgmActive)
        {
            //sgm[soundToPlay].pitch = Random.Range(.9f, 1.1f);
            sgm[soundToPlay].Stop();
           // sgmActive = true;
        }
    }
void HandleEvent (TrackEntry trackEntry, Spine.Event e) {


//Normal VFX
    if (e.Data.Name == "wall") {     
    // Controlla se la variabile "SwSl" è stata inizializzata correttamente.
        PlayMFX(0);
    }
}
}
