   M S B A M L   `   `   `  ����  HDFaceBasics-WPF    VideoPlayer.VideoPlayer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � .+�5   
   r� -   5        $��mediaElement1��5   5     r� Source$   ?�6   $��0,0,0,64q�6#    r� LoadedBehavior$ Manual=�6J    r� UnloadedBehavior$
 Stop=�6d    r� Stretch$
 Fill=�6|   6�   �� 5	   
   -   5         ��Name$
 Play��5	      $��开始播放��6   $��0,0,0,10q�6.   $��Bottom=�6@   $��27��6[   $��Center=�6{   $��68��6�   �� 5
   
   -   5        $��Volume��5
   0   $��27��6   $��	0,0,10,10q�6   $��Bottom=�6>   $
��Left=�6Y   $��Center=�6{    � Minimum$ 0Q�6�    � Maximum$ 1Q�6�    � Value$	 0.5Q�6�   $��80��6�   $��Right=�6�   �� 5   
   $��音量��6   $��	0,0,87,14q�6   $��25��60   $��Bottom=�6<   $��Right=�6W   $��35��6s    J� RenderTransformOrigin$ 2.829,-0.12&�6~   �� 5   
   -   5        $��TimeLine��5   1   $��23��6   $��
10,0,10,39q�6   $��Bottom=�6D   $ 60Q�6�   �� 5   
   -   5        	 ��Name$	 empty��5      $
��Left=�6    $	��569��6;   $��0,-3,0,0q�6H   $	��Top=�6Z   $	��792��6r   �� 5   
   -   5        $	 black��5      $��	0,-3,0,64q�6    ���	#FF0F0F0F63   �� 5   
   -   5        $ Restart��5      $��重新开始��6"   $��Bottom=�61   $��27��6L   $
��Left=�6o   $��68��6�   $��	26,0,0,10q�6�   �� 5   
   -   5        
 ��Name$

 over��5      $��播放结束��6   $��60��6-   $	��160��69   ���	#FFF9F6F6$��36(�6\   $��Center=�6j   $��Center=�6�   $��316,219,316,287q�6�   5      5      