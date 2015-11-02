using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double _x;
        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                if (_x != value)
                {
                    _x = value;

                    RaisePropertyChanged();
                }
            }
        }

        private double _y;
        private bool isClick;
        private Point _initMovePosition;

        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (_y != value)
                {
                    _y = value;

                    RaisePropertyChanged();
                }
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
            {
                return;
            }

            e.Handled = true;
            isClick = true;

            var boardPosition = e.GetCurrentPoint(scrollView).Position;

            _initMovePosition = new Point(boardPosition.X - X,
                                          boardPosition.Y - Y);


            Debug.WriteLine(_initMovePosition);
        }

        private void ScrollViewer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;

            if (!isClick)
            {
                return;
            }

            var boardElement = scrollView;
            var mousePosition = e.GetCurrentPoint(boardElement).Position;

            Point diffPosition = new Point(mousePosition.X - _initMovePosition.X,
                                           mousePosition.Y - _initMovePosition.Y);
            

            Move(diffPosition);

        }

        private void Image_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
            {
                return;
            }

            e.Handled = true;
            isClick = false;
        }

        private void Move(Point position)
        {
            X = position.X;
            Y = position.Y;

            Debug.WriteLine(position);
        }

        //private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        //{
        //    var position = e.Delta.Translation;

        //    Move(position);

        //}

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
