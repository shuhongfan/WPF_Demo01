   M S B A M L   `   `   `  ����  InkCanvasExample!    InkCanvasExample.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  '墨迹画板（InkCanvas）基本用法��5   	   $	��360��6%   $	��680��62   ���	#FFF0F9D8 � WindowStartupLocation$ CenterScreen=�6U   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5
   
   +  %System.Windows.Controls.Ribbon.Ribbon  -   5          Name$ ribbon��5
      $��0��6!    � 	Resources 5      6  0System.Windows.Controls.Ribbon.RibbonRadioButton �� 
TargetType �� Property �� Value��%  '        �� 5      "  6   .�5      ��  �@ LargeImageSource$ /images/b1.png��69   �� 5       �@ SmallImageSource$ /images/b1.gif��69   �� 5       �@ CornerRadius$ 13��65   �� 5       � ��$ 5 0 0 0q�6/   -   5        5      5         ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      $y�	Collapsed=�6(   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  -   5        	  Name$		 rt1��5      $
��例1��6%   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��笔画类型��6   .@�5        -   5        
  Name$
 rrbPen��5   (      Label$ 钢笔��68    d� 	IsChecked$
 TrueY�6C     5      $ 	荧光笔��6(   5        5      $��钢笔颜色��6   .@�5        $ 红色��6(   $
 TrueY�63     5      $ 绿色��6(     5      $ 蓝色��6(   5         5!      $��编辑选项��6   .@�5"        -   5        $
 rrbInk��5"   (   $ 墨迹��68   $
 TrueY�6C    	� 	GroupName$
 edit��6T     5#      $ 手势��6(   $
 edit��63     5$      $ 套索选择��6(   $
 edit��65     5%      $ 	点擦除��6(   $
 edit��64     5&      $ 笔画擦除��6(   $
 edit��65   5'      5(      5)      �� 5*   
   -   5        $
��ink1��5*      $��1��6     �� 
Background �	#FFFFF9F56-   5+      5,      