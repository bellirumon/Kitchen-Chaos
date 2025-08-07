using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClipRefsSO", menuName = "ScriptableObjects/AudioClipRefsSO")]
public class AudioClipRefsSO : ScriptableObject
{
    [SerializeField] private AudioClip[] _chop;
    public AudioClip[] Chop => _chop;


    [SerializeField] private AudioClip[] _deliveryFail;
    public AudioClip[] DeliveryFail => _deliveryFail;


    [SerializeField] private AudioClip[] _deliverySuccess;
    public AudioClip[] DeliverySuccess => _deliverySuccess;


    [SerializeField] private AudioClip[] _footstep;
    public AudioClip[] Footstep => _footstep;


    [SerializeField] private AudioClip[] _objectDrop;
    public AudioClip[] ObjectDrop => _objectDrop;


    [SerializeField] private AudioClip[] _objectPickup;
    public AudioClip[] ObjectPickup => _objectPickup;


    [SerializeField] private AudioClip _stoveSizzle;
    public AudioClip StoveSizzle => _stoveSizzle;


    [SerializeField] private AudioClip[] _trash;
    public AudioClip[] Trash => _trash;


    [SerializeField] private AudioClip[] _warning;
    public AudioClip[] Warning => _warning;
}
