using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class synth_output : MonoBehaviour
{
    public float[] hz = new float[1];
    private int sampleHz = 48000;
    public float gain = 1;

    public Line_RENDERER lineRender;
    bool setVerts;

    private void OnAudioFilterRead(float[] data, int channels)
    {   //using onAudioFilterRead to over-write the empty signal from the sound source,
          //a sound-source must be attached to this gameobject (the gameobject this class is attached to)


        audio_synthesis.sine_Series(data, hz, sampleHz, gain, channels);
        //use a static method to set points on graph renderer
        if (lineRender != null) { array_grapher.graph_points(lineRender, data);
            setVerts = true;
        }
    }
    private void LateUpdate()
    {
        if (lineRender != null&&setVerts) { lineRender.SetVerticesDirty(); }
    }




}
public class audio_synthesis
{
    public static float[] generate_sine(float hz, float sampleHz, float gain,int channels)
   {    
        //some basic code for generating a sound signal~
        //which is not so much straightforward as expected, due to the way sound is sampled on a computer

        float[] data = new float[2048];

        float increment = 2 * Mathf.PI * hz / sampleHz;

        float phase = 0;


        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            data[i] = gain * Mathf.Sin(phase);

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2)) { phase = 0; }
        }

        return data;
   }
    public static void set_sine(float[] data,float hz, float sampleHz, float gain, int channels)
    {
        
        float increment = 2 * Mathf.PI * hz / sampleHz;
        float phase = 0;


        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            data[i] = gain * Mathf.Sin(phase);
          
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2)) { phase = 0; }
        }
    }

    public static void sine_Series(float[] data,float[] hz, float sampleHz, float gain, int channels)
    {
        //for each frequency in the array, add that term
        for(int i=0;i<hz.Length;i++)
        {
            float[] sine = generate_sine(hz[i], sampleHz, gain, channels);
            
            //for each frequency in HZ array, add the generated sinewave^
            for(int x=0; x<data.Length;x++)
            {  //to each point in the data array~ the data array is the sound signal we're trying to create!
                data[x] += sine[x] / hz.Length;
            }
        }
    }


}

