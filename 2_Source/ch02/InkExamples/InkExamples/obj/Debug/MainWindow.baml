   M S B A M L   `   `   `  ����F  BInkExamples, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:InkExamples.UserControlsInkExamples.UserControls  5   	       InkExamples.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5uc&clr-namespace:InkExamples.UserControls     5   	     � Title$!  自定义墨迹画板示例��5   	   $	��460��6   $	��980��6(   ���	#FFF0F9D8 � WindowStartupLocation$ CenterScreen=�6K   .��5      � -   5         �Name$
 root��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  -   5          Name$ ribbon��5      $��0��6!    � 	Resources 5      6  0System.Windows.Controls.Ribbon.RibbonRadioButton �� 
TargetType �� Property �� Value��%  '        �� 5      "  6   .�5      ��  �@ LargeImageSource$ /images/b1.png��69   �� 5       �@ SmallImageSource$ /images/b1.gif��69   �� 5       �@ CornerRadius$ 13��65   �� 5       � ��$ 5 0 0 0q�6/   -   5        5      5      	   ApplicationMenu	 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      -   5        
  Name$
 appMenu1��5   (   $y�Visible=�6:    � ToolTip$ 菜单��6O    5      >  8System.Windows.Controls.Ribbon.RibbonApplicationMenuItem3  -System.Windows.Controls.Ribbon.RibbonMenuItem��%x   '        �� 5      "  6    .�5      ��  �@ ImageSource$ /images/b1.gif��6<   -   5        5      5      .@�5        $��打开��60     5      $��	另存为��60     5      $��退出��60   5       .@�5"      *   $InkExamples.UserControls.MyRibbonTab  -   5          Name$	 rt2��5"      .  (System.Windows.Controls.Ribbon.RibbonTab   
IsSelected . $
��例2��6<     5#      -   5        $	 rt3��5#      $
��例3��6*     5$      -   5        $	 rt4��5$      $
��例4��6*     5%      -	   5        $	 rt5��5%      $
��例5��6*     5&      -
   5        $	 rt6��5&      $
��例6��6*     5'      -   5        $	 rt7��5'      $
��例7��6*   5(      � 5)   
   -   5        $ grid1��5)      $��10q�6   $��1��6*   $y�Visible=�67   .+�5*      �� $��2��6   
������� �� RadiusX$ 14��5+       �� RadiusY$ 14��6%   
����  � �� StrokeDashArray$ 3R�5,   &    � 5-      -   5        $��frame1��5-      $��10q�6"   $��1q�6.   
����  �  � NavigationUIVisibility$ Hidden=�5.      5/      50      51      