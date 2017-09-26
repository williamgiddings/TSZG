using UnityEngine;
using System.Collections;

public class TOD : MonoBehaviour
{
    float slider;
    float slider2;
    float Hour;
    private float Tod;

    Light sun;

    float speed = 50;

    Color NightFogColor;
    Color DuskFogColor;
    Color MorningFogColor;
    Color MiddayFogColor;

    Color NightAmbientLight;
    Color DuskAmbientLight;
    Color MorningAmbientLight;
    Color MiddayAmbientLight;

    Color NightTint;
    Color DuskTint;
    Color MorningTint;
    Color MiddayTint;

    Material SkyBoxMaterial1;
    Material SkyBoxMaterial2;

    Color SunNight;
    Color SunDay;

    GameObject Water;
    bool IncludeWater = false;
    Color WaterNight;
    Color WaterDay;


    void OnGUI ()
    {

        if(slider >= 1.0f)
        {
	        slider = 0;
        }

        Hour= slider*24;
        Tod= slider2*24;
        sun.transform.localEulerAngles = new Vector3((slider*360)-90, 0, 0);
        slider = slider +Time.deltaTime/speed;
        sun.color = Color.Lerp (SunNight, SunDay, slider*2);


        if (IncludeWater == true)
        {
	        Water.GetComponent<Renderer>().material.SetColor("_horizonColor", Color.Lerp (WaterNight, WaterDay, slider2*2-0.2f));
        }

        if(slider<0.5f)
        {
            slider2= slider;
        }
        if(slider>0.5f)
        {
            slider2= (1-slider);
        }
        sun.intensity = (slider2-0.2f)*1.7f;


        if(Tod<4)
        {
            //it is Night
            RenderSettings.skybox=SkyBoxMaterial1;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            SkyBoxMaterial1.SetColor ("_Tint", NightTint);
            RenderSettings.ambientLight = NightAmbientLight;
            RenderSettings.fogColor = NightFogColor;
        }
        if(Tod>4&&Tod<6)
        {
            RenderSettings.skybox=SkyBoxMaterial1;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            RenderSettings.skybox.SetFloat("_Blend", (Tod/2)-2);
            SkyBoxMaterial1.SetColor ("_Tint", Color.Lerp (NightTint, DuskTint, (Tod/2)-2) );
            RenderSettings.ambientLight = Color.Lerp (NightAmbientLight, DuskAmbientLight, (Tod/2)-2);
            RenderSettings.fogColor = Color.Lerp (NightFogColor,DuskFogColor, (Tod/2)-2);
            //it is Dusk

        }
        if(Tod>6&&Tod<8)
        {
            RenderSettings.skybox=SkyBoxMaterial2;
            RenderSettings.skybox.SetFloat("_Blend", 0);
            RenderSettings.skybox.SetFloat("_Blend", (Tod/2)-3);
            SkyBoxMaterial2.SetColor ("_Tint", Color.Lerp (DuskTint,MorningTint,  (Tod/2)-3) );
            RenderSettings.ambientLight = Color.Lerp (DuskAmbientLight, MorningAmbientLight, (Tod/2)-3);
            RenderSettings.fogColor = Color.Lerp (DuskFogColor,MorningFogColor, (Tod/2)-3);
            //it is Morning

        }
        if(Tod>8&&Tod<10)
        {
            RenderSettings.ambientLight = MiddayAmbientLight;
            RenderSettings.skybox=SkyBoxMaterial2;
            RenderSettings.skybox.SetFloat("_Blend", 1);
            SkyBoxMaterial2.SetColor ("_Tint", Color.Lerp (MorningTint,MiddayTint,  (Tod/2)-4) );
            RenderSettings.ambientLight = Color.Lerp (MorningAmbientLight, MiddayAmbientLight, (Tod/2)-4);
            RenderSettings.fogColor = Color.Lerp (MorningFogColor,MiddayFogColor, (Tod/2)-4);

            //it is getting Midday

        }
    }
}