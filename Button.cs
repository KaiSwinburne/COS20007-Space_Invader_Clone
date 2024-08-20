using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Custom_project
{
    public class Button 
    {
        private bool _selected, _isclicked;
        private string _text;
        private int _textsize;
        private Rectangle _rectangle;       

        public Button(string text, int textsize, int width, int height, double x, double y)
        {
            _selected = false;
            _isclicked = false;
            _rectangle = new Rectangle();
            _rectangle.Width = width;
            _rectangle.Height = height;
            _rectangle.X = x;
            _rectangle.Y = y;
            _text = text;
            _textsize = textsize;
        }

        public void Draw()
        {         
            //SplashKit.DrawRectangleOnWindow(Demo.window, Color.Yellow, _rectangle);
            
            switch (_selected)
            {
                default:
                    SplashKit.DrawTextOnWindow(Demo.window, _text, Color.White, Demo.font, _textsize, _rectangle.X + 10, _rectangle.Y + 10);
                    break;
                case true:
                    SplashKit.DrawTextOnWindow(Demo.window, _text, Color.Yellow, Demo.font, _textsize, _rectangle.X + 10, _rectangle.Y + 10);
                    break;
            }

            if (_isclicked)
            {
                SplashKit.DrawTextOnWindow(Demo.window, _text, SplashKit.RGBAColor(192,192,192,1), Demo.font, _textsize, _rectangle.X + 10, _rectangle.Y + 10);
            }
        }

        public void Update(Point2D pt)
        {
            MouseHoverButton(pt);
            ButtonIsClicked(pt);
        }

        public bool IsAt(Point2D point)
        {
            double shape_farright = _rectangle.X + _rectangle.Width;     //get the far right corner of the shape
            double shape_bottom = _rectangle.Y + _rectangle.Height;      //get the bottom corner of the shape
            bool status = false;

            // if positioned inside the shape
            if (((point.X >= _rectangle.X) && (point.X <= shape_farright)) & ((point.Y >= _rectangle.Y) && (point.Y <= shape_bottom)))
            {
                status = true;
            }
            return status;
        }

        public void MouseHoverButton(Point2D pt)
        {
            if (IsAt(pt))
            {
                _selected = true;
            }
            else
            {
                _selected = false;
            }
        }

        public void ButtonIsClicked(Point2D pt)
        {
            if (IsAt(pt) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _isclicked = true;
            }
            else
            {
                _isclicked = false;
            }

        }

        public bool IsClicked
        {
            set { _isclicked = value; }
            get { return _isclicked; }
        }

    }
}
