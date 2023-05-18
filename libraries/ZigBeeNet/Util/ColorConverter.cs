using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Util
{
    public class ColorConverter
    {
        public static CieColor RgbToCie(double red, double green, double blue)
        {
            // Apply a gamma correction to the RGB values, which makes the color more vivid and more the like the color displayed on the screen of your device
            var r = (red > 0.04045) ? Math.Pow((red + 0.055) / (1.0 + 0.055), 2.4) : (red / 12.92);
            var g = (green > 0.04045) ? Math.Pow((green + 0.055) / (1.0 + 0.055), 2.4) : (green / 12.92);
            var b = (blue > 0.04045) ? Math.Pow((blue + 0.055) / (1.0 + 0.055), 2.4) : (blue / 12.92);

            // RGB values to XYZ using the Wide RGB D65 conversion formula
            var X = r * 0.664511 + g * 0.154324 + b * 0.162028;
            var Y = r * 0.283881 + g * 0.668433 + b * 0.047685;
            var Z = r * 0.000088 + g * 0.072310 + b * 0.986039;

            // Calculate the xy values from the XYZ values
            var x = (X / (X + Y + Z));
            var y = (Y / (X + Y + Z));

            return new CieColor(x, y);
        }
        public static void CieToRgb(double x, double y, double brightness, out double red, out double green, out double blue)
        {
			var z = 1.0 - x - y;
			var Y = brightness;
			var X = (Y / y) * x;
			var Z = (Y / y) * z;

			//Convert to RGB using Wide RGB D65 conversion
			red = X * 1.656492 - Y * 0.354851 - Z * 0.255038;
			green = -X * 0.707196 + Y * 1.655397 + Z * 0.036152;
			blue = X * 0.051713 - Y * 0.121364 + Z * 1.011530;

			//If red, green or blue is larger than 1.0 set it back to the maximum of 1.0
			if (red > blue && red > green && red > 1.0)
			{
				green = green / red;
				blue = blue / red;
				red = 1.0;
			}
			else if (green > blue && green > red && green > 1.0)
			{
				red = red / green;
				blue = blue / green;
				green = 1.0;
			}
			else if (blue > red && blue > green && blue > 1.0)
			{
				red = red / blue;
				green = green / blue;
				blue = 1.0;
			}

			//Reverse gamma correction
			red = red <= 0.0031308 ? 12.92 * red : (1.0 + 0.055) * Math.Pow(red, (1.0 / 2.4)) - 0.055;
			green = green <= 0.0031308 ? 12.92 * green : (1.0 + 0.055) * Math.Pow(green, (1.0 / 2.4)) - 0.055;
			blue = blue <= 0.0031308 ? 12.92 * blue : (1.0 + 0.055) * Math.Pow(blue, (1.0 / 2.4)) - 0.055;


			if (double.IsNaN(red))
				red = 0;

			if (double.IsNaN(green))
				green = 0;

			if (double.IsNaN(blue))
				blue = 0;
		}
    }
}
