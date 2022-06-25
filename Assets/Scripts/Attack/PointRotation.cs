using UnityEngine;
using System.Collections;
public class PointRotation : MonoBehaviour
{
    public enum TargetType
    {
        Player,
        Mouse,
        Other
    }

    public TargetType targetType;
    [SerializeField] private Transform target; 
    public float offset = 0f; //Смещение
    public float coefficient = 1f; //Коэффициент
    [SerializeField] private bool movePosToCenter; //Будет ли объект перемещаться к центру
    [SerializeField] private Transform center; //Центр объекта, от которого будет определяться угол
    [SerializeField] private bool useLocalPos; //Будет ли использоваться локальная позиция или глобальная

    private bool stopRotating = false; //Остановка кручения
    private float angle; //Последний угол поворота который был вычислен

    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        if(targetType == TargetType.Player) target = FindObjectOfType<PlayerController>().transform;
    }
    private void Update()
    {
        if (center != null && movePosToCenter) //Движение за центром
        {
            //Движение для локальной и глобальной позиции
            if(useLocalPos) transform.localPosition = new Vector3(center.localPosition.x, center.localPosition.y, 0);
            else transform.position = new Vector3(center.position.x, center.position.y, 0);
        } 

        //Кручение 
        transform.localRotation = Quaternion.Euler(0f, 0f, coefficient * CalculateAngle());
    }

    public void MultiplyCoefficent(float x) { coefficient *= x; }
    private float CalculateAngle() 
    {
        if(!stopRotating)
        {
            Vector2 direction = new Vector2();
    
            //Определение угла по мыши
            if(targetType == TargetType.Mouse)
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                direction = new Vector3(mousePos.x, mousePos.y, 0f) - center.position;
            }
        
            else if(target != null)//По таргету
                direction = target.position - center.position;
        
            angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90f;
            angle += offset;
            return angle;
        }
        return angle;
    }
    public void StopRotating(bool active, float time) { StartCoroutine(_StopRotating(active, time)); }
    private IEnumerator _StopRotating(bool active, float time)
    {
        stopRotating = active;
        yield return new WaitForSeconds(time);
        stopRotating = !active;
    }
}
