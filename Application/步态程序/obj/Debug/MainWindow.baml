   M S B A M L   `   `   `  ����  BodyBasics-WPF4    .Microsoft.Samples.Kinect.BodyBasics.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   -   5          � Title$   ��5   	   $	��800��5   	   $
��1200��6   $
��1200��5   &   $	��800��66   
������� � Icon$ Images/Kinect.ico��6Y    � 	Resources 5	         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush��%�   &	        &	 #     &	 F     �� 5
   
   $ 	#ff6e6e6e��62   �� 5   
   $ 	#ff52318f��64   �� 5   
   $ 	#ff00BCF2��62   5      .��5      � $��0,0,2,0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   (� -   5         (�Name$ viewbox��5      $��1��6#   $��Center=�60   .��5      �� $��UniformToFill=�63   ��6   �� *ImageSource+63   5      �� 5   
   -   5         ��Name$ canvas2��5      $��47��6"   $	��Top=�6.   $	��935��6F   $��Center=�6R   .+�5      �� -   5         ��Name$ btnStart��5      $��开始��6'   $��Right=�64   $��Center=�6P   $��80��6k   $��35��6v    �� Left$	 602��6�   
	 �� Top$	 6��6�   $��20(�6�   ���	#FFDDDDDD6�   � 5      -   5        
 �Name$
 
progessBar��5      $��35��6.   $	 371��6:   $	 6��6L   $	��177��6[    � SmallChange$ 1Q�6�    � Maximum$	 120Q�6�   ���	#FFE2DCDC���	#FF040404$��1q�6�   �� 5      -   5        $ btnSave��5      $��保存��6&   $	 692��63   $	 6��6E   $��80��6T   $��35��6_   $��20(�6|   �� 5      $��时间��6   $	 312��6!   $	 4��63   $��24(�6B   �� 5      -   5         ��Name$ 
labelguide��5      $��未捕捉到人体��6(   $
��Left=�69   $	��Top=�6T   $��41��6l   $	��157��6x   $��24(�6�   ���	#FFD13E3E$	 3��6�   $
��Bold"�6�   $ 31��6�   �� 5      -   5        $ back2��5      $��返回主界面��6$   $	 782��64   $	 6��6F   $	��115��6U   $��35��6a   $��20(�6�   5      5       5!      