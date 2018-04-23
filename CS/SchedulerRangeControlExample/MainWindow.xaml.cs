using DevExpress.Xpf.Core;
using DevExpress.XtraScheduler;
using System;

namespace SchedulerRangeControlExample
{
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void schedulerControl1_RangeControlAutoAdjusting(object sender,
                                                              RangeControlAdjustEventArgs e)
        {
            SchedulerViewType activeViewType = scheduler.ActiveViewType;

            if (activeViewType == SchedulerViewType.WorkWeek)
            {
                e.Scales[4].DisplayFormat = "dddd";
                e.Scales[4].Width = 70;
            }

            if (activeViewType == SchedulerViewType.Month)
            {
                e.Scales.Clear();

                TimeScaleMonth monthScale = new TimeScaleMonth();
                monthScale.DisplayFormat = "MMMM yyyy";
                e.Scales.Add(monthScale);

                TwoWeekTimeScale twoWeekTimeScale = new TwoWeekTimeScale();
                twoWeekTimeScale.Width = 120;
                e.Scales.Add(twoWeekTimeScale);

                e.RangeMinimum = new DateTime(e.RangeMinimum.Year, 1, 1);
                e.RangeMaximum = e.RangeMinimum.AddYears(1);
            }
        }
    }

    public class TwoWeekTimeScale : TimeScaleFixedInterval
    {
        public TwoWeekTimeScale()
            : base(TimeSpan.FromDays(14))
        {
        }
        public override DateTime Floor(DateTime date)
        {
            DateTime startOfWeeek = DevExpress.XtraScheduler.Native.DateTimeHelper.GetStartOfWeekUI(date,
                                                                    DevExpress.XtraScheduler.Native.DateTimeHelper.FirstDayOfWeek);
            if (DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear(date) % 2 == 0)
                return startOfWeeek.AddDays(-7);

            return startOfWeeek;
        }
        public override string FormatCaption(DateTime start, DateTime end)
        {
            string dateString = "Week {0} - Week {1}";
            return String.Format(dateString, DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear(start),
                                 DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear(end.AddTicks(-1)));
        }

    }
}
