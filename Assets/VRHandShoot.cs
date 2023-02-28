using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;

    public List<ActiveStateSelector> poses;
    public TMPro.TextMeshProUGUI text;

    [SerializeField] private Transform shootLocation;
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    private bool isClosedHand;
    //private OVRGrabbable grabbable;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        //grabbable = GetComponent<OVRGrabbable>();
        audio = GetComponent<AudioSource>();

    }
    /*
    private void SetTextToPoseName(string newText)
    {
        text.text = newText;
    }*/

    private void CheckPoseName(string poseName)
    {
        if(poseName == "RockPose")
        {
            isClosedHand = true;
            print("rock pose");
        }
        if(isClosedHand && poseName == "PaperPose")
        {
            print("paper pose");
            isClosedHand = false;
            Shoot();
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in poses)
        {
            item.WhenSelected += () => CheckPoseName(item.gameObject.name);
            item.WhenSelected += () => print(item.gameObject.name);
            //item.WhenUnselected += () => SetTextToPoseName("");
        }
    }

    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, shootLocation.position, shootLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, shootLocation.position, shootLocation.rotation).GetComponent<Rigidbody>().AddForce(shootLocation.forward * shotPower);

    }
}
