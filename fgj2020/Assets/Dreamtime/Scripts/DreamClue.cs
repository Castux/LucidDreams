using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamClue : MonoBehaviour
{
    public Clue clueData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Found a clue! Clue text: " + clueData.clueText);
            // add clue to memory
            FindObjectOfType<PlayerProgression>().AddClue(clueData);
            FindObjectOfType<DreamCluePopUpText>().ShowClue(clueData.clueText);

            Destroy(gameObject);
        }
    }
}
