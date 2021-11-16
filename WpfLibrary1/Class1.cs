using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfLibrary1
{
    public class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TempProperty;
        private string windDirection;
        private int windSpeed;
        private int skyClarity;
        private string sky;

        public int Temp
        {
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }
        public string WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }
        public int WindSpeed
        {
            get => windSpeed;
            set => windSpeed = value;
        }
        private int SkyClarity
        {
            get => skyClarity;
            set
            {
                if (skyClarity == 0)
                {
                    sky = "Солнечно";
                }
                else if (skyClarity == 1)
                {
                    sky = "Облачно";
                }
                else if (skyClarity == 2)
                {
                    sky = "Дождь";
                }
                else if (skyClarity == 3)
                {
                    sky = "Снег";
                }
                else if (skyClarity < 0 && skyClarity > 3)
                {
                    sky = "Ошибка";
                }
            }
        }
        public WeatherControl(int temp, string windDirection, int windSpeed, int skyClarity)
        {
            this.Temp = temp;
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.SkyClarity = skyClarity;
        }
        static WeatherControl()
        {
            TempProperty = DependencyProperty.Register(
                nameof(Temp),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(ValidateTemp));
        }

        private static bool ValidateTemp(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
            {
                return v;
            }
            else
            {
                return 0;
            }
        }

        public string Print()
        {
            return $"Температура: {Temp}, направление ветра: {windDirection}, скорость ветра: {windSpeed}, наличие осадков: {sky}";
        }
    }
}
