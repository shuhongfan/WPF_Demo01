   M S B A M L   `   `   `  ����G  CMarketClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MarketClient.InksMarketClient.Inks  5      "    MarketClient.Manager.Monitor   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .myclr-namespace:MarketClient.Inks     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Monitor��5	      .��5
      � 
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��150 �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��监控查看��6   $��Center=�6   #   MarketClient.Inks.MyInkCanvas  5   
   -   5          Name$
 ink1��5      $��1��6'   ]� 5   
   $��2��6   
�������.+�5      �� $	��Top=�6   �� 5      $��0 5 0 5q�6    �� Orientation$ 
Horizontal=�6*   $��Center=�6C   $��Center=�6^    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��$ 5 0 5 0q�63   �� 5       � �� 68   Z� �� 5       � ��
 ��  �67   �� 5       � ��
 ���� 67   -   5        5      5      .+�5      �� $��单击查看大图：��6   $��Center=�6+   �� 5       $��一楼��6   
 � Tag$ 
Video1.wmv��6&   �� 5!      $��二楼��6   $ 
Video2.wmv��6&   �� 5"      $��三楼��6   $ 
Video3.wmv��6&   �� 5#      $��四楼��6   $ 
Video4.wmv��6&   5$      5%      r� 5&   
   -   5        $��media1��5&      $��3��6%   	 r� Stretch$	 Uniform=�62   
 r� Source$ 
 /Manager/Videos/Video2.wmv?�6D   5'      5(      