using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage; //Logo
        [SerializeField] private TMP_Text tittle; //Titulo
        [SerializeField] private TMP_Text description; //Descrição

        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false); //Resetar imagem
            tittle.text = ""; //Resetar titulo
            description.text = ""; //Resetar descrição
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true); //Mostar imagem 
            itemImage.sprite = sprite; //Adicionar imagem
            tittle.text = itemName;
            description.text = itemDescription;
        }
    }
}