using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DayCycle : MonoBehaviour
{

    class TimeOfDay{
        float hour;
        float dayLenghtRealTime;

        public TimeOfDay(float h, float dayLength){
            hour = h;
            dayLenghtRealTime = dayLength;
        }

        public void PassTime(){
            hour += Time.deltaTime * 24 / dayLenghtRealTime;
            if(hour >= 24f){
                hour -= 24;
            }
        }

        public int GetHour(){
            return (int)Mathf.Floor(hour);
        }

        int GetMinute(){
            return (int)(hour % 1 * 60);
        }

        public string GetTimeString(){
            return GetHour().ToString("D2") + ":"  + GetMinute().ToString("D2");
        }

        public float GetLightLevel(){
            float x = Mathf.Sin(2*hour/7.6394f -1.6f)/2+0.5f;
            return Mathf.Lerp(0.1f, 0.9f, x);
        }

        public void PassHours(float x){
            hour += x;
            if(hour >= 24f){
                hour -= 24;
            }
        }

    }

    TimeOfDay timeOfDay;
    [SerializeField] static float dayLenghtInSeconds = 30f;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] Light2D dayLight;

    public static DayCycle instance;

    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeOfDay = new TimeOfDay(8, dayLenghtInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay.PassTime();
        timeText.text = timeOfDay.GetTimeString();
        dayLight.intensity = timeOfDay.GetLightLevel();
    }

    public static float GetDayLengthInSeconds(){
        return dayLenghtInSeconds;
    }

    public void AddHours(float x){
        timeOfDay.PassHours(x);
    }

    public float GetHour(){
        return timeOfDay.GetHour();
    }
}
