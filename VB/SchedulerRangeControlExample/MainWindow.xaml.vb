Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Core
Imports DevExpress.XtraScheduler
Imports System

Namespace SchedulerRangeControlExample
	Partial Public Class MainWindow
		Inherits DXWindow
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub schedulerControl1_RangeControlAutoAdjusting(ByVal sender As Object, ByVal e As RangeControlAdjustEventArgs)
			scheduler.OptionsRangeControl.AutoFormatScaleCaptions = True
			Dim activeViewType As SchedulerViewType = scheduler.ActiveViewType

			If activeViewType = SchedulerViewType.WorkWeek Then
				scheduler.OptionsRangeControl.AutoFormatScaleCaptions = False
				e.Scales(4).DisplayFormat = "dddd"
				e.Scales(4).Width = 70
			End If

			If activeViewType = SchedulerViewType.Month Then
				scheduler.OptionsRangeControl.AutoFormatScaleCaptions = False

				e.Scales.Clear()

				Dim monthScale As New TimeScaleMonth()
				monthScale.DisplayFormat = "MMMM yyyy"
				e.Scales.Add(monthScale)

				Dim twoWeekTimeScale As New TwoWeekTimeScale()
				twoWeekTimeScale.Width = 120
				e.Scales.Add(twoWeekTimeScale)

				e.RangeMinimum = New DateTime(e.RangeMinimum.Year, 1, 1)
				e.RangeMaximum = e.RangeMinimum.AddYears(1)
			End If
		End Sub
	End Class

	Public Class TwoWeekTimeScale
		Inherits TimeScaleFixedInterval
		Public Sub New()
			MyBase.New(TimeSpan.FromDays(14))
		End Sub
		Public Overrides Function Floor(ByVal [date] As DateTime) As DateTime
			Dim startOfWeeek As DateTime = DevExpress.XtraScheduler.Native.DateTimeHelper.GetStartOfWeekUI([date], DevExpress.XtraScheduler.Native.DateTimeHelper.FirstDayOfWeek)
			If DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear([date]) Mod 2 = 0 Then
				Return startOfWeeek.AddDays(-7)
			End If

			Return startOfWeeek
		End Function
		Public Overrides Function FormatCaption(ByVal start As DateTime, ByVal [end] As DateTime) As String
			Dim dateString As String = "Week {0} - Week {1}"
			Return String.Format(dateString, DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear(start), DevExpress.XtraScheduler.Native.DateTimeHelper.GetWeekOfYear([end].AddTicks(-1)))
		End Function

	End Class
End Namespace
