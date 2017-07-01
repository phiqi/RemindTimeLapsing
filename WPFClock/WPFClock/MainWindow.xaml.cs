using System;
using System.Windows;
using System.Timers;

namespace WPFClock
{
    public partial class MainWindow : Window
    {
        private Timer m_timer;
        private int m_nSecond;
        //private Action m_action;  //会报“不允许跨线程访问”
        private delegate void UpdateTimer();    //通过委托来调用定时器方法，否则会报“不允许跨线程访问”
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;
            m_nSecond = 0;
            m_timer = new Timer();
            m_timer.Interval = 1000;
            m_timer.Elapsed += HandleElapsed;
            m_timer.Start();
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;
            //m_action = MyEventFunc;
        }

        void HandleElapsed(object o, EventArgs e)
        {
            //m_action();
            Dispatcher.BeginInvoke(new UpdateTimer(MyEventFunc));
        }

        void MyEventFunc()
        {
            m_nSecond++;
            if (m_nSecond == 1)
            {
                Hide();
            }
            else if (m_nSecond == 60)
            {
                m_nSecond = 0;
                Show();
            }
        }
    }
}
