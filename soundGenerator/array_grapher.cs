using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class array_grapher
{
    //this class will store methods for visualizing data from float[]

    public static void graph_points(Line_RENDERER lineRender,float[] data)
    {   
        Vector2[] points = new Vector2[data.Length];

        for(int i = 0;i<data.Length;i++)
        {
            points[i].x = i * lineRender.gridSize.x / data.Length;
            points[i].y = data[i] * lineRender.gridSize.y;
        }

        lineRender.points.Clear();
        lineRender.points.AddRange(points);
    }
}
