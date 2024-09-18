using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject talk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(PlayerInfo.Instance().GetPlayerPos(), transform.position) < 5) {
            talk.SetActive(true);
        } else {
            talk.SetActive(false);
        }
    }
}
