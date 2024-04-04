using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactions : MonoBehaviour
{
    //General
    [Header("General")]
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickUpUI; //Crossair
    private FirstPersonController fpc;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3f;
    private RaycastHit hit;
    private RaycastHit hit2;
    private RaycastHit hit3;
    private RaycastHit hit4;

    //Items
    [Header("Items")]
    [SerializeField] private InventorySO inventoryData;    
    [SerializeField] private LayerMask pickableLayerMask;
    private GameObject itemm;

    //Posters
    [Header("Posters")]
    [SerializeField] private LayerMask posterLayerMask;
    [SerializeField] private GameObject posterGb;
    [SerializeField] private Image posterImg;
    private Sprite posterSprite;
    private bool isPosteractive = false;
    [SerializeField] private AudioSource posterAudio;
    [SerializeField] private AudioClip posterClip;

    //Recorder
    [Header("Recorder")]
    [SerializeField] private LayerMask recorderLayerMask;
    private GameObject recorderGb;
    private Recorder recorder;

    //VendingMachine
    [Header("Vending Machine")]
    private VendingMachine vendingMachine;
    [SerializeField] private LayerMask vending;
    public bool isVended = false;

    private void Start()
    {        
        posterImg.enabled = false;
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        PickItems();
        PickPosters();
        UseRecorders();
        UseVendingMachines();
    }

    private void PickItems()
    {
        if (hit.collider != null)
        {            
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);

            itemm = hit.transform.gameObject;
            Item item = itemm.GetComponent<Item>();

            if (Input.GetKeyDown(KeyCode.F))
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    Destroy(itemm);
                    pickUpUI.SetActive(false);
                }
                else
                    item.Quantity = reminder;
            }
        }
    }

    private void PickPosters()
    {
        if (hit2.collider != null)
        {            
            pickUpUI.SetActive(false);            
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit2, hitRange, posterLayerMask))
        {            
            pickUpUI.SetActive(true);

            posterGb = hit2.transform.gameObject;
            Posters posters = posterGb.GetComponent<Posters>();
            posterSprite = posters.sprite;
            posterImg.sprite = posterSprite;

            if (Input.GetKeyDown(KeyCode.F) && isPosteractive == false)
            {
                posterImg.enabled = true;                
                isPosteractive = true;
                fpc.enabled = false;                
                posterAudio.clip = posterClip;
                posterAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.F) && isPosteractive)
            {
                posterImg.enabled = false;
                isPosteractive = false;
                fpc.enabled = true;               
            }
        }
    }

    private void UseRecorders()
    {
        if (hit3.collider != null)
        {
            pickUpUI.SetActive(false);                 
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit3, hitRange, recorderLayerMask))
        {
            pickUpUI.SetActive(true);           

            recorderGb = hit3.transform.gameObject;
            recorder = recorderGb.GetComponent<Recorder>();

            if (Input.GetKey(KeyCode.F) && !recorder.recorderAudio.isPlaying)
            {
                recorder.recorderAudio.Play();
            }
        }
    }

    private void UseVendingMachines()
    {
        if (hit4.collider != null)
        {
            hit4.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);          
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit4, hitRange, vending))
        {
            hit4.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);                   

            if (Input.GetKey(KeyCode.F) && !isVended)
            {
                vendingMachine = hit4.transform.gameObject.GetComponentInParent<VendingMachine>();
                vendingMachine.SpawnDrink();
                isVended = true;
            }
        }
    }
}

