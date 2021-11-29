using System.Collections;
using Interfaces;
using Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] 
        public Image foregroundImage;
        private float _imageUpdateSpeed = 1f;
        private AbstractUnit _unit;
        public Image elementImage;

      /*  public UnitHealthBar(AbstractUnit unit)
        {
            _unit = unit;
            unit.TakeDamage +=HealthChanged;
            unit.Controller.Model.OnElementChange += ElementChanged;
        }*/
        public void Init(AbstractUnit unit)
        {
            _unit = unit;
            _unit.TakeDamage +=HealthChanged;
            _unit.Controller.Model.OnElementChange += ElementChanged;
            //elementImage.sprite = ElementChanged(_unit.Controller.Model.Element);
            ElementChanged(_unit.Controller.Model.Element);
        }

        private void HealthChanged(float currentHP)
        {
          //  Debug.Log($"{_unit.Controller.Model.HP.GetCurrentHp}- {currentHP}");
          var damage = (_unit.Controller.Model.HP.GetCurrentHp - currentHP)/_unit.Controller.Model.HP.Max;
            _unit.ChildCouroutine(ChangeHealthPicture(damage));
        }

        private void ElementChanged(NameManager.ElementList elementList)
        {
            switch (elementList)
            {
                case NameManager.ElementList.Fire:
                    elementImage.sprite = Resources.Load<Sprite>("Sprite/Fire"); 
                    break;
                case NameManager.ElementList.Water:
                    elementImage.sprite = Resources.Load<Sprite>("Sprite/Water"); 
                    break;
                case NameManager.ElementList.Earth:
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
            while (elapsed < _imageUpdateSpeed)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(fullPictureHP, currentHP, elapsed / _imageUpdateSpeed);
                yield return null;
            }
            foregroundImage.fillAmount = currentHP;
           // Debug.Log($"Filled for {_foregroundImage.fillAmount}");
        }
    }  
}

