using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour {

    // The color mode
    public enum ColorMode { FIXED, GRADIENT, TIME_BASED }

    // The current color of the show
    public Color ShapeColor { get; set; }
    public ColorMode ModifierMode { get; set; }

    public Gradient ColorGradier { get; set; }

    GameController GameControllerRef;

    SpriteRenderer SpriteComponent;
    UnityEngine.UI.Text TextComponent;
    UnityEngine.UI.Outline OutlineComponent;
   
    // 
    float CurrentAngle;
    Color CurrentColor;

	// Use this for initialization
	void Start ()
    {
        // Cache the sprite component
        SpriteComponent = GetComponentInParent<SpriteRenderer>();
        TextComponent = GetComponentInParent<UnityEngine.UI.Text>();
        OutlineComponent = GetComponentInParent<UnityEngine.UI.Outline>();

        // Modifier mode
        ModifierMode = ColorMode.TIME_BASED;

        // Random start value
        CurrentAngle = Random.Range(0, 360);

        // Cache the game controller component
        GameControllerRef = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Based the 
        switch (ModifierMode)
        {
            case ColorMode.TIME_BASED:
                // Update the current angle
                CurrentAngle = (CurrentAngle > 360) ? 0 : CurrentAngle + 5;

                // Update the color value
                CurrentColor = Color.HSVToRGB(CurrentAngle / 360f, 1, 1);
                break;

            case ColorMode.GRADIENT:
                // Update the current angle
                CurrentAngle = (CurrentAngle > 360) ? 0 : CurrentAngle + 1 * Time.deltaTime;

                // Update the color value
                CurrentColor = ColorGradier.Evaluate(CurrentAngle / 360f);
                break;

            case ColorMode.FIXED:
                break;
        }
                
        if (SpriteComponent)
            SpriteComponent.color = CurrentColor;

        if (TextComponent)
            TextComponent.color = CurrentColor;

        if (OutlineComponent)
            OutlineComponent.effectColor = CurrentColor;
    }
}
