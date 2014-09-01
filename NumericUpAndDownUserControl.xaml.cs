using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace qiquanui
{
    /// <summary>
    /// NumericUpAndDownUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class NumericUpAndDownUserControl : UserControl
    {
        private readonly Regex _numMatch;

		/// <summary>Initializes a new instance of the NumericBoxControlLib.NumericBox class.</summary>
        public NumericUpAndDownUserControl()
		{
			InitializeComponent();

			_numMatch = new Regex(@"^-?\d+$");
            //_numMatch = new Regex(@"^-?[1-9]\d*$");
			Maximum = int.MaxValue;
			Minimum = int.MinValue;
			//Value = 0;
		}

		private void ResetText(TextBox tb)
		{
			tb.Text = 0 < Minimum ? Minimum.ToString() : "0";

			tb.SelectAll();
		}
	}

	// Internal event handlers
    public partial class NumericUpAndDownUserControl
	{
		private void value_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var tb = (TextBox)sender;
			var text = tb.Text.Insert(tb.CaretIndex, e.Text);

			e.Handled = !_numMatch.IsMatch(text);
		}

		private void value_TextChanged(object sender, TextChangedEventArgs e)
		{

			var tb = (TextBox)sender;
            //bool test = _numMatch.IsMatch(tb.Text);
            if (!_numMatch.IsMatch(tb.Text))
                ResetText(tb);
            else
                Value = Convert.ToInt32(tb.Text);
			if (Value < Minimum) 
                Value = Minimum;
			if (Value > Maximum) 
                Value = Maximum;

			RaiseEvent(new RoutedEventArgs(ValueChangedEvent));


            //MainWindow.otm.LimitTradingNum();     //交易区
            //MainWindow.otm.changeBuyOrSellByTradingNum();

            ////平仓界面
            //CloseOutPlaceOrderWindow.copowm.LimitTradingNum();
            //CloseOutPlaceOrderWindow.copowm.changeBuyOrSellByTradingNum();

		}

		private void Increase_Click(object sender, RoutedEventArgs e)
		{
			Value++;
			RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
		}

		private void Decrease_Click(object sender, RoutedEventArgs e)
		{
			Value--;
			RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
		}
    }

    // Properties
    public partial class NumericUpAndDownUserControl
    {
        /// <summary>The Value property represents the value of the control.</summary>
        /// <returns>The current value of the control</returns>
        [DefaultValue(0)]

        //public int Value
        //{
        //    get { return Int32.Parse(value.Text); }
        //    set { this.value.Text = value.ToString(); }
        //}

        ////////////////////////////////////////////////////////////////
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                this.Textvalue.Text = value.ToString();
            }
        }


        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
      

        public static readonly DependencyProperty ValueProperty =
           DependencyProperty.Register("Value", typeof(int), typeof(NumericUpAndDownUserControl), new PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged)));


        //public static readonly DependencyProperty ValueProperty =
        //   DependencyProperty.Register("Value", typeof(int), typeof(NumericUpAndDownUserControl), new PropertyMetadata(0));

        static void OnValueChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            NumericUpAndDownUserControl source = (NumericUpAndDownUserControl)sender;
            source.Textvalue.Text = ((int)args.NewValue).ToString();
        }

        /////////////////////////////////////////////////////////////////


        /// <summary>The Maximum property represents the maximum value of the control.</summary>
        /// <returns>The maximum possible value of the control</returns>
        [DefaultValue(Int32.MaxValue)]
        public int Maximum { get; set; }

        /// <summary>The Minimum property represents the minimum value of the control.</summary>
        /// <returns>The minimum possible value of the control</returns>
        [DefaultValue(Int32.MinValue)]
        public int Minimum { get; set; }
    }

    // Event handlers
    public partial class NumericUpAndDownUserControl
    {
        // Value changed
        private static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NumericUpAndDownUserControl));

        /// <summary>The ValueChanged event is called when the value of the control changes.</summary>
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        //Increase button clicked
        private static readonly RoutedEvent IncreaseClickedEvent =
            EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NumericUpAndDownUserControl));

        /// <summary>The IncreaseClicked event is called when the Increase button clicked</summary>
        public event RoutedEventHandler IncreaseClicked
        {
            add { AddHandler(IncreaseClickedEvent, value); }
            remove { RemoveHandler(IncreaseClickedEvent, value); }
        }

        //Increase button clicked
        private static readonly RoutedEvent DecreaseClickedEvent =
            EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NumericUpAndDownUserControl));

        /// <summary>The DecreaseClicked event is called when the Decrease button clicked</summary>
        public event RoutedEventHandler DecreaseClicked
        {
            add { AddHandler(DecreaseClickedEvent, value); }
            remove { RemoveHandler(DecreaseClickedEvent, value); }
        }
    }
}
