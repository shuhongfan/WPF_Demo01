   M S B A M L   `   `   `  ����  ch06*    $ch06.Examples.ParallelInvokeExample2   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  ParallelInvokeExample2��5       � 	Resources 5	         a0  a1  a2  a3  a4  a5��%,  &	        &	 (     &	 P     &	 x     &	 �     &	 �     �� 5
   
   $��/images/a0.jpg��6   �� 5   
   $��/images/a1.jpg��6   �� 5   
   $��/images/a2.jpg��6   �� 5   
   $��/images/a3.jpg��6   �� 5   
   $��/images/a4.jpg��6   �� 5   
   $��/images/a5.jpg��6   5      .��5      ]� .+�5   
   �� $	��Top=�6     BorderStyle#��[ 6&   ._�5      �� $+��%Parallel.Invoke方法的基本用法2��6     
TitleStyle#��[ 67   5      �� 5   
   $��Bottom=�6   #��[ 6)   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��20��6O   .+�5      �� -   5        $��btnStart��5      $
��10 0q�6)   $'��!运行（请多次单击观察）��68   6N   5      5      � 5   
   -   5        $��grid1��5      
������� � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5!      �� 5"      �� 5#      �� 5$      �� 5%      5'      5(      5)      