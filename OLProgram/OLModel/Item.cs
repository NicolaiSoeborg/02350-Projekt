using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: Kan vi slette dette?

namespace OLProgram.OLModel
{
    public class Item : NotifyBase
    {
        private static int counter = 0;

        public int Number { get; }

        private double x = 200;
        public double X { get { return x; } set { x = value; } }
        //public double X { get { return x; } set { x = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); } }

        private double y = 200;
        public double Y { get { return y; } set { y = value; } }
        //public double Y { get { return y; } set { y = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); } }

        private double width = 100;
        public double Width { get { return width; } set { width = value; } }
        //public double Width { get { return width; } set { width = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); NotifyPropertyChanged(() => CenterX); } }

        private double height = 100;
        public double Height { get { return height; } set { height = value; } }

        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; } }
        //public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }

        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; } }
        //public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged(() => Y); } }

        public double CenterX => Width / 2;

        public double CenterY => Height / 2;

        //public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }

        public Item()
        {
            Number = ++counter;
        }

        public override string ToString() => Number.ToString();

    }
}
