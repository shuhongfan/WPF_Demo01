   M S B A M L   `   `   `  ����  ch04    ch04.Examples.AesExample1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  AesExample1��5      .��5	      ]� .+�5
   
   �� $	��Top=�6      BorderStyle#��[  6&   ._�5      �� $+��%AES对称加密算法的基本用法1��6     
TitleStyle#��[ 6/   5      �� 5   
   $��Bottom=�6   
�������.+�5      � $��5q�6    � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��0��6%   $��被加密的字符串：��65   �� 5      $��1��6   $��0��6%   $��	密码：��65   $��Right=�6C   �� 5      -   5        $��textBox1��5   7   $��0��6   $��1��6'   $��Hello，你好。��6G   $��Center=�6X   G� 5      -   5        $��pwdBox1��5   ;   $��1��6   $��1��6+    G� Password$ 12345��6J   $��Center=�6[   �� 5      -   5        $
��btn1��5   :   $��2��6   $��2��6)   $��	10 0 10 0q�6F   $��	10 0 10 0q�6Y   $��加密解密��6m   $��Center=�6|   6�   5      5      �� 5   
   $��Bottom=�6   �� 5    
   .��5!      �� 
������� �� 
LineHeight$ 20��6,   .+�5"      �� -   5         ��Name$ 
textBlock1��5"      $��0 10 0 0q�60   $
��Wrap=�6B   5#      5$      5%      5&      