using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static NameManager;

namespace Unit
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] 
        public Image foregroundImage;
        private AbstractUnit _unit;
        public Image elementImage;
        public void Init(AbstractUnit unit)
        {
            _unit = unit;
            _unit.TakeDamage +=HealthChanged;
            _unit.Controller.Model.OnElementChange += ElementChanged;
            ElementChanged(_unit.Controller.Model.Element);
        }

        private void HealthChanged(float damageValue)
        {
            if (!_unit.isActiveAndEnabled) return;
          //  Debug.Log($"{_unit.Controller.Model.HP.GetCurrentHp}- {currentHP}");
          var damage = (_unit.Controller.Model.HP.GetCurrentHp - damageValue)/_unit.Controller.Model.HP.Max;
            _unit.ChildCouroutine(ChangeHealthPicture(damage));
        }

        private void ElementChanged(ElementList elementList)
        {
            switch (elementList)
            {
                case ElementList.Fire:
                    elementImage.sprite = Resources.Load<Sprite>("Sprite/Fire"); 
                    break;
                case ElementList.Water:
                    elementImage.sprite = Resources.Load<Sprite>("Sprite/Water"); 
                    break;
                case ElementList.Earth:
                    elementImage.sprite = Resources.Load<Sprite>("Sprite/Earth"); 
                    break;
                default:
                  break;
            }
        }
        public void ResetBar(float maxVal)
        {
            _unit.ChildCouroutine(ChangeHealthPicture(maxVal));
        }

        public void RenewBar(IUnitController unit)
        {
            var filled = _unit.Controller.Model.HP.GetCurrentHp /_unit.Controller.Model.HP.Max;
            _unit.ChildCouroutine(ChangeHealthPicture(filled));
        }

        private IEnumerator ChangeHealthPicture(float currentHP)
        {
            var fullPictureHP = foregroundImage.fillAmount;
            var elapsed = 0f;
            while (elapsed < ImageUpdateSpeed)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(fullPictureHP, currentHP, elapsed / ImageUpdateSpeed);
                yield return null;
            }
            foregroundImage.fillAmount = currentHP;
           // Debug.Log($"Filled for {_foregroundImage.fillAmount}");
        }
    }  
}

