using UnityEngine;
using Photon.Pun;
using System;

public class PlayerSpwan : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    public static event Action StrikerInstantiated;

    void Start()
    {
        GameObject gm = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        gm.name = "Striker" + PhotonNetwork.LocalPlayer.ActorNumber;

        StrikerInstantiated?.Invoke();
    }

    void SpawnStriker() //Spawn striker at first
    {

    }
}
