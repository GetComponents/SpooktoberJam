using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpScare : MonoBehaviour
{
    public static JumpScare Instance;

    [SerializeField]
    Image ghostImage, zombieImage;

    Vector3 startPos, endPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void JumpScarePlayer(JUMPSCAREIMAGE _image)
    {
        switch (_image)
        {
            case JUMPSCAREIMAGE.NONE:
                break;
            case JUMPSCAREIMAGE.ZOMBIE:
                zombieImage.gameObject.SetActive(true);
                break;
            case JUMPSCAREIMAGE.GHOST:
                ghostImage.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void HideImage(JUMPSCAREIMAGE _image)
    {
        switch (_image)
        {
            case JUMPSCAREIMAGE.NONE:
                break;
            case JUMPSCAREIMAGE.ZOMBIE:
                zombieImage.gameObject.SetActive(false);
                break;
            case JUMPSCAREIMAGE.GHOST:
                ghostImage.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}

public enum JUMPSCAREIMAGE
{
    NONE,
    ZOMBIE,
    GHOST
}
