   M S B A M L   `   `   `  ����  ch05)    #ch05.Examples.CancellationTokenPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  CancellationTokenPage��5      .��5	      ]� .+�5
   
   �� $	��Top=�6   $!��取消任务的基本用法��6%      
LabelStyle#��[  69   �� 5   
   $��Bottom=�6     BorderStyle#��[ 6)   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��btnStart��5      $��5 0 5 0q�6)   $��启动任务��6;   6J   �� 5      -   5        $��	btnCancel��5      $��5 0 5 0q�6*   $��20 0 0 0q�6<   $��取消任务执行��6N   6_   5      5      �� 5   
   -   5        $��progressStackPanel��5      $	��Top=�6/   $��10q�6D   
�������6P   �� 5   
   .��5      �� 
������� �� 
LineHeight$ 15��6,   .+�5      �� -   5         ��Name$ 
textBlock1��5      $��5q�60   $
��Wrap=�6;   5      5      5      5      