   M S B A M L   `   `   `  ����G  CMarketClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MarketClient.InksMarketClient.Inks  5      "    MarketClient.Manager.InkPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .myclr-namespace:MarketClient.Inks     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  InkPage��5	      .��5
      � 
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  -   5          Name$ ribbon��5      $��0��6!    � 	Resources 5      6  0System.Windows.Controls.Ribbon.RibbonRadioButton �� 
TargetType �� Property �� Value��%(  '        �� 5      "  6   .�5      ��  �@ LargeImageSource$ /Manager/images/b1.png��69   �� 5       �@ SmallImageSource$ /Manager/images/b1.gif��69   �� 5       �@ CornerRadius$ 13��65   �� 5       � ��$ 5 0 0 0q�6/   -   5        5      5         ApplicationMenu 5      :  4System.Windows.Controls.Ribbon.RibbonApplicationMenu  5      $y�	Collapsed=�6(   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $��动态绘图��6   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  $��文件存取��6   .@�5           Label$ 加载��6(     5       $ 保存��6(     5!      $ 	另存为��6(   5"        5#      -   5        $��rgTool��5#      $��绘制工具��6,   	   GroupSizeDefinitions	 5%      >  8System.Windows.Controls.Ribbon.RibbonGroupSizeDefinition  
   ControlSizeDefinitions.
 5&      @  :System.Windows.Controls.Ribbon.RibbonControlSizeDefinition     	ImageSize$ Small=�6:     5'      $ Small=�6:     5(      $ Small=�6:     5)      $ Small=�6:     5*      $ Small=�6:     5+      $ Small=�6:   5,      .@�5.        $ 	文字块��6(    d� 	IsChecked$
 TrueY�64     5/      $ 箭头��6(     50      $ 通道��6(     51      $ 入口��6(     52      $ 出口��6(     53      $ 视频��6(   54        55      -   5          Name$ rgColor��55      $��笔尖颜色��6/   	 57        .
 58        $ Small=�6:     59      $ Small=�6:     5:      $ Small=�6:     5;      $ Small=�6:     5<      $ Small=�6:     5=      $ Small=�6:   5>      .@�5@        $ 蓝色��6(   $
 TrueY�63     5A      $ 红色��6(     5B      $ 绿色��6(     5C      $ 浅灰��6(     5D      $ 黑色��6(     5E      $ 天蓝��6(   5F        5G      $��笔尖大小��6   	 5I        .
 5J        $ Small=�6:     5K      $ Small=�6:     5L      $ Small=�6:     5M      $ Small=�6:     5N      $ Small=�6:     5O      $ Small=�6:   5P      .@�5R        $ 2��6(     5S      $ 4��6(   $
 TrueY�62     5T      $ 6��6(     5U      $ 10��6(     5V      $ 16��6(     5W      $ 22��6(   5X        5Y      -   5        $��rgOperation��5Y      $��操作选择��61   	 5[        .
 5\        $ Small=�6:     5]      $ Small=�6:     5^      $ Small=�6:     5_      $ Small=�6:     5`      $ Small=�6:     5a      $ Small=�6:   5b      .@�5d        $ 套索��6(     5e      $ 全选��6(     5f      $ 	全不选��6(     5g      $ 墨迹擦除��6(     5h      $ 笔画擦除��6(     5i      $ 全部删除��6(   5j      5k      5l      ]� 5m   
   -   5        $��dockPanelText��5m      $��1��6)   $y�Hidden=�66   .+�5n      �� $
��Left=�6   $T��N输入文字名称（名称为空表示只绘制轮廓，不绘制文字）：��6*   �� 5o      -   5        $��textBoxWenZi��5o      $
��Left=�6*   $��海尔冰箱��5p      5q      5r      ]� 5s   
   -   5        $��dockPanelVideo��5s      $��1��6*   .+�5t      �� $
��Left=�6   $'��!选择要绘制的视频文件：��6*   	� 5u      -	   5        $
��Left=�5u      $��
Video1.wmv��60   $
 TrueY�6E   $��Center=�6V   6q   	� 5v      -
   5        $
��Left=�5v      $��10 0 0 0q�60   $��
Video2.wmv��6B   $��Center=�6W   6r   	� 5w      -   5        $
��Left=�5w      $��10 0 0 0q�60   $��
Video3.wmv��6B   $��Center=�6W   6r   	� 5x      -   5        $
��Left=�5x      $��10 0 0 0q�60   $��
Video4.wmv��6B   $��Center=�6W   6r   5y      ]� 5z   
   $��2��6   .+�5{      �� $��Bottom=�6   .@�5|      �� -   5        $��labelStatus��5|      $����【操作类别：管理员】   提示：（1）选择后可按【Del】键删除。（2）绘制完成后，退出前请先将其保存到文件中，以便下次继续编辑。��5}      5~      #   MarketClient.Inks.MyInkCanvas  5      -   5          Name$
 ink1��5      5�      5�      5�      