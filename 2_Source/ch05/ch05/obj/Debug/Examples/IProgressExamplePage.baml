   M S B A M L   `   `   `  ����  ch05(    "ch05.Examples.IProgressExamplePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  IProgressExamplePage��5      .��5	      ]� .+�5
   
   �� $	��Top=�6   $-��'获取任务执行进度的基本用法��6%      
LabelStyle#��[  6=   �� 5   
   $��Bottom=�6     BorderStyle#��[ 6)   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��btnStart��5      $��70��6)   $��启动任务��64   6C   �� 5      -   5        $��	btnCancel��5      $��20 0 0 0q�6*   $��90��6<   $��取消任务执行��6G   6X   5      5      �� 5   
   .+�5      �� $��0 10 0 0q�6   $��任务1（可取消）：��6*   � 5      
�������.+�5      � -   5        $��progressBar1��5      
�������$��20��6I   $��10q�6U   �� 5      -   5         ��Name$ 
textBlock1��5      $��Center=�60   $��Center=�6M   5      �� 5      $"��任务2（不可取消）：��6   � 5      
�������.+�5      � -   5        $��progressBar2��5      
�������$��20��6I   $��10q�6U   �� 5      -   5        $ 
textBlock2��5      $��Center=�60   $��Center=�6M   5      5      5      5      