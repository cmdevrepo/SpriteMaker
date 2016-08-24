﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawRect : BaseDrawCommand {

	public float CenterX, CenterY;
	public float Width;
	public float Height;
	public Color rectColor;

	private int pixelPosX;
	private int pixelPosY;
	private int pixelWidth;
	private int pixelHeight;

	public override Color[] DrawToColorArray (Color[] _input, int _width, int _height)
	{

		pixelPosX = Mathf.CeilToInt(CenterX * (float)_width);
		pixelPosY = Mathf.CeilToInt(CenterY * (float)_height);
		pixelWidth = Mathf.CeilToInt(Width * (_width));
		pixelHeight = Mathf.CeilToInt(Height * (_height));




		int leftBounds = pixelPosX - (pixelWidth / 2);
		int rightBounds = pixelPosX + (pixelWidth / 2);
		int lowerBounds = pixelPosY - (pixelHeight / 2);
		int upperBounds = pixelPosY + (pixelHeight / 2);



		for (int x = leftBounds; x <= rightBounds; x++) {
			if (x >= 0 && x < _width) {
				for (int y = lowerBounds; y <= upperBounds; y++) {
					if (y >= 0 && y < _height) {


						Color c = rectColor;


						c.r = (c.r * c.a) + ((1 - c.a) * _input [y * _width + x].r);
						c.g = (c.g * c.a) + ((1 - c.a) * _input [y * _width + x].g);
						c.b = (c.b * c.a) + ((1 - c.a) * _input [y * _width + x].b);

						c.a = c.a + _input [y * _width + x].a;

						_input [y * _width + x] = c;

					}
				}
			}
		}


		return base.DrawToColorArray (_input, _width, _height);
	}

	public override void DrawControls ()
	{

		rectColor = EditorGUILayout.ColorField ("Color", rectColor);
		CenterX = float.Parse(EditorGUILayout.TextField ("X Position", CenterX.ToString()));
		CenterY = float.Parse(EditorGUILayout.TextField ("Y Position", CenterY.ToString()));
		Width = float.Parse(EditorGUILayout.TextField ("Width", Width.ToString()));
		Height = float.Parse(EditorGUILayout.TextField ("Height", Height.ToString()));


		base.DrawControls ();
	} 

}
