   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      F BDactyloTest, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DactyloTestDactyloTest 5      !   DactyloTest.IndividualGraph   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      +localclr-namespace:DactyloTest     5      $	��467��5   Y   $	��850��6f   .��5	      �� .+�5
   
   �� $��51��6   $	��383��6    $
��Bold"�6,     �� Left$	  295��6>   
 �� Top$ 4��6P   $��30(�6_      Poppins#��[  6m   
������� �� TextAlignment$ Center=�6�   
Mes scores6�   �� 5   
   -   5         ��Name$ Score��5        RoundBasicBtn#��[ 65   $  28��6\   $ 74��6m   $��98��6}   .��6�   Score6�   �� 5   
   -   5        $	 CPS��5      #��[ 63   $  46��6Z   $	 124��6k   $��80��6|   .��6�   CPS6�   �� 5   
   -   5        $	 WPM��5      #��[ 63   $  46��6Z   $	 174��6k   $��80��6|   .��6�   WPM6�   �� 5   
   -   5        $ Accuracy��5      #��[ 68   $  28��6_   $	 224��6p   $��98��6�   .��6�   
Accuracy6�   #   LiveCharts.Wpf.CartesianChart  5   
   -   5          Name$ 	MainGraph��5      $��Poppins)�6T   
�������&    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6|   $	��323��6�   $	  178��6�   $ 60��6�   $	��662��6�      Series 60   �� *SeriesCollection+   AxisX 5         LiveCharts.Wpf.Axis     LabelsRotation$	 -30Q�5      	   LabelFormatter	 5      �� *XDateFormatter+
   	Separator
 5         LiveCharts.Wpf.Separator  5         Step$ 1Q�6(   5         DataTooltip 5      #   LiveCharts.Wpf.DefaultTooltip  5      
���   �   SelectionMode SharedXValues68   6X   5      5      5      