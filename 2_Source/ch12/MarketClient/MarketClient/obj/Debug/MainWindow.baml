   M S B A M L   `   `   `  ����  MarketClient    MarketClient.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$-  '商场服务系统客户端（示例）��5   	   $	��600��6   $
��1000��6,   ���	#FFF0F9D8 � WindowStartupLocation$ CenterScreen=�6P   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5
   
   �� $��5q�6   ���	#FFF0F9D8
����  � �� StrokeDashArray$ 3R�5      9� 5   
    9� ExpandDirection$ Right=�6   $��10q�5       9� 
IsExpanded . .��5      �� .��5      �� 
������� � 	Resources 5       �� 
TargetType �� Property	 �� Value��%�  '��       '9� �     �� 5      " ��6    .�5      ��  � ��$	 	10 5 10 0q�67   �� 5       � ��$	 	10 0 10 0q�68   �� 5       � ��	 6<   Z� �� 5       � ��
	 ��  �6;   �� 5       � ��
	 ���� 6;   -   5        5      �� 5      " 9�6    .�5      ��  � ��
	 �����6;   �� 5       �@9�
IsExpanded	 . 6;   5      5      .+�5      9� $��	管理员��6   .��5       �� 
�������.+�5!      �� $��动态绘图��6%   

 � Tag$
 /Manager/InkPage.xaml��64   �� 5"      $��监控查看��6%   $
 /Manager/Monitor.xaml��64   5#      5$      9� 5%      $��	结算员��6   .��5&      �� 
�������.+�5'      �� $��购物结算��6%   $ 
 /Account/CurrentSales.xaml��64   �� 5(      $��当日统计��6%   $#
 /Account/CurrentDaySales.xaml��64   5)      5*      9� 5+      $��顾客��6   .��5,      �� 
�������.+�5-      �� $��商场概览��6%   $ 
 /Customer/MarketGuide.xaml��64   �� 5.      $��促销活动��6%   $
 /Customer/Promotions.xaml��64   5/      50      9� 51      $��辅助功能��6   .��52      �� 
�������.+�53      �� $��密码修改��6%   $!
 /Common/ChangePassword.xaml��64   �� 54      $��操作帮助��6%   $
 /Common/Help.xaml��64   55      56      57      58      59       � 5:   
   -   5        $��frame1��5:      $��1��6   $��5q�6.   
�������$��1q�5<      
����  �  � NavigationUIVisibility$ Hidden=�5=        � Source$ /Common/StartPage.xaml?�5>      5?      5@      