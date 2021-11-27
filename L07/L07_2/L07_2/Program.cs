using System;

namespace L07_2
{
    public class Color
    {
        private string hexValue { set; get; }
        public Color(string hexValue)
        {
            this.hexValue = hexValue;
        }

        override public string ToString()
        {
            return $"#{hexValue}";
        }
    }
    public interface IFigure
    {
        void move(double x, double y);
    }

    public interface IHasInterior
    {
        void SetInteriorColor(Color color);
        Color GetInteriorColor();

    }

    public class Point: IFigure
    {
        private double x, y;
        public Point(double x, double y) 
        {
            this.x = x;
            this.y = y;
        }

        public void move(double x, double y)
        {
            this.x += x;
            this.y += y;
        }
    }

    public class Triangle: IFigure, IHasInterior
    {
        private Point a, b, c;
        private Color innerColor;
        Triangle(Point a, Point b, Point c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public Triangle(Point a, Point b, Point c, Color color) : this(a, b, c)
        {
            this.innerColor = color;
        }
        public void move(double x, double y)
        {
            a.move(x, y);
            b.move(x, y);
            c.move(x, y);
        }
        public Color GetInteriorColor()
        {
            return innerColor;
        }
        public void SetInteriorColor(Color color)
        {
            innerColor = color;
        }
    }

    class Program
    {
        public static void PrintAllColors(object[] tableOfObject)
            {
                foreach (object obj in tableOfObject) {
                    if(obj is IHasInterior)
                    {
                        Console.WriteLine((obj as IHasInterior).GetInteriorColor());
                    }
                    else
                    {
                        Console.WriteLine("no color");
                    }
                }
            }
        static void Main(string[] args)
        {
            Color green = new Color("00FF00");
            Color red = new Color("FF0000");
            Point p1 = new Point(0.2, 4.3);
            Point p2 = new Point(22.2, 3.3);
            Point p3 = new Point(44.2, 4.3);

            object[] allMyObjects = { green, p1, p2, "", new Triangle(p1, p2, p3, red), 14, new Triangle(p1, p2, p3, green) };

            PrintAllColors(allMyObjects);
        }
    }
}
