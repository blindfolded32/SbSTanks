using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace SbSTanks
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _foregroundImage;
        private float _imageUpdateSpeed = 1f;
        private Enemy _enemy;

        void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
            _enemy.HealthChanged += HealthChanged;
        }

        private void HealthChanged(float currentHP)
        {
            Debug.Log($"ТЕКУЩЕЕ Здоровье !!! ---- {_enemy.Parameters.Hp.GetCurrentHp}");
            Debug.Log($"ТЕКУЩЕЕ Здоровье из Action'a!!! ---- {currentHP}");
            Debug.Log($"МАКС Здоровье !!! ---- {_enemy.Parameters.Hp.Max}");
            var damage = 0.4f;
            StartCoroutine(ChangeHealthPicture(damage));
        }

        private IEnumerator ChangeHealthPicture(float currentHP)
        {
            var fullPictureHP = _foregroundImage.fillAmount;
            var elapsed = 0f;
            
            while (elapsed < _imageUpdateSpeed)
            {
                elapsed += Time.deltaTime;
                _foregroundImage.fillAmount = Mathf.Lerp(fullPictureHP, currentHP, elapsed / _imageUpdateSpeed);
                yield return null;
            }
            _foregroundImage.fillAmount = currentHP;
        }
    }  
}

