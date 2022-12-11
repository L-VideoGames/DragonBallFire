using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer offSprite;
    //private bool isMute;// = false;

    // Start is called before the first frame update
    void Start()
    {

        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 1;
            this.offSprite.enabled = false;

        }
        else
        {
            AudioListener.volume = 0;
            this.offSprite.enabled = true;

        }
    }

    void OnMouseDown()
    {

        //mainCamera.GetComponent<AudioListener>().enabled = isMute;
        if (AudioListener.volume >0)
        {
            AudioListener.volume = 0;
            this.offSprite.enabled = true;
        }
        else
        {
            AudioListener.volume = 1;
            this.offSprite.enabled = false;
        }

        
        
    }


}
