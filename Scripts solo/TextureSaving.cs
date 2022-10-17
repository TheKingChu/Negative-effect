using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TextureSaving
{
    public void TextureArray_SaveToDisk(float[,] array, string pathInResourcesFolder)
    {
        TextureToDisk(ArrayToTexture(array), pathInResourcesFolder);
    }

    public Texture2D ArrayToTexture(float[,] array)
    {
        Texture2D tex = new Texture2D(array.GetLength(0), array.GetLength(1));

        for(int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                tex.SetPixel(i, j, new Color(array[i, j], 0f, 0f));
            }
        }

        tex.Apply();
        return tex;
    }

    public void TextureArray_SaveToDisk(Vector3[,] array, string pathInResourcesFolder)
    {
        TextureToDisk(ArrayToTexture(array), pathInResourcesFolder);
    }

    public Texture2D ArrayToTexture(Vector3[,] array)
    {
        Texture2D tex = new Texture2D(array.GetLength(0), array.GetLength(1));

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                tex.SetPixel(i, j, new Color(array[i, j].x, array[i, j].y, array[i, j].z));
            }
        }

        tex.Apply();
        return tex;
    }

    //Saves the texture2d as a png into the folder:
    //
    public void TextureToDisk(Texture2D texture,string pathInResourcesFolder)
    {
        File.WriteAllBytes("Assets/Resources/" + pathInResourcesFolder + ".png", texture.EncodeToPNG());
    }
}
