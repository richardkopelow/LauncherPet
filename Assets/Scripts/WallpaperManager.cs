using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallpaperManager : MonoBehaviour
{
    public AndroidHelper AndroidInterop;
    public Image Wallpaper;

    private void Start()
    {
        Texture2D wallpaperTexture = AndroidInterop.Wallpaper;
        Sprite wallSprite = Sprite.Create(wallpaperTexture, new Rect(0, 0, wallpaperTexture.width, wallpaperTexture.height), new Vector2(0.5f, 0.5f));
        Wallpaper.sprite = wallSprite;
    }
}
