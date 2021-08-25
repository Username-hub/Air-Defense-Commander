using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyUnitInfoScript : MonoBehaviour
    {
        public Canvas unitInfoCanvas;

        public Image healthBar;
        private RectTransform rectTransform;
        // Start is called before the first frame update
        void Start()
        {
            unitInfoCanvas = GetComponent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetRotationOffset(float ang)
        {
            rectTransform.localRotation = Quaternion.Euler(new Vector3(0,0,-ang));
        }

        public void UpdateBars(float currentHealth, float maxHealth)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}