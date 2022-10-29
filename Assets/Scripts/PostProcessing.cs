using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour {
    public Shader[] postProcessingShaders;
    public bool[] useShaders;
    private Material[] postProcessingMaterials;
    private RenderTexture[] textures;

    //called every frame by unity, which automatically passes in what's already rendered as the source and the camera's target (in most cases, the screen) as the destination
    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        InitTextures(source.width, source.height);
        int totalUsedShaders = 0;
        for (int i = 0; i < useShaders.Length; i++) {
            if (useShaders[i]) { totalUsedShaders++; }
        }
        int currUsedShaders = 0;
        if (totalUsedShaders == 0) {
            Graphics.Blit(source, destination);
        } else if (totalUsedShaders == 1) {
            Graphics.Blit(source, destination, postProcessingMaterials[0]);
        } else {
            for (int i = 0; i < postProcessingMaterials.Length; i++) {
                if (useShaders[i]) {
                    if (currUsedShaders == 0) {
                        Graphics.Blit(source, textures[0], postProcessingMaterials[0]);
                    } else if (currUsedShaders == totalUsedShaders - 1) {
                        Graphics.Blit(textures[currUsedShaders % 2 - 1], destination, postProcessingMaterials[currUsedShaders]);
                    } else {
                        Graphics.Blit(textures[currUsedShaders % 2 - 1], textures[currUsedShaders % 2], postProcessingMaterials[currUsedShaders]);
                    }
                    currUsedShaders++;
                }
            }
        }
    }

    private void OnValidate() {
        if (postProcessingShaders != null) {
            postProcessingMaterials = new Material[postProcessingShaders.Length];
            for (int i = 0; i < postProcessingShaders.Length; i++) {
                if (postProcessingShaders[i] != null) {
                    postProcessingMaterials[i] = new Material(postProcessingShaders[i]);
                }
            }
            if (useShaders != null) {
                if (useShaders.Length != postProcessingShaders.Length) {
                    bool[] temp = new bool[postProcessingShaders.Length];
                    for (int i = 0; i < Mathf.Min(useShaders.Length, postProcessingShaders.Length); i++) {
                        temp[i] = useShaders[i];
                    }
                    useShaders = temp;
                }
            }
        }
    }

    private void InitTextures(int width, int height) {
        if (textures == null || textures.Length != 2) {
            textures = new RenderTexture[2];
        }
        for (int i = 0; i < 2; i++) {
            if (textures[i] == null || textures[i].width != width || textures[i].height != height) {
                if (textures[i] != null) {
                    textures[i].Release();
                }
                textures[i] = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            }
        }
    }
}
