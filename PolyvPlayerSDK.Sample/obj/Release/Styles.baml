   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CustomSliderForegroundBrush  �� Color  CustomSliderBackgroundBrush  CustomSliderHighlightBrush  ShadedBackgroundBrush �� 
StartPoint �� EndPoint � Offset � Color  PositionSliderThumbTemplate �� 
TargetType � UseLayoutRounding F�Name �� Stretch	 �� Triggers
 P� Property P� Value �� 
TargetName �� Value �� Property  PositionSliderTemplate ��Name J� SnapsToDevicePixels � RowDefinitions �� RadiusX �� RadiusY ]�Name ]� Thumb n�Name � OverridesDefaultStyle  PositionSliderStyle �� 
TargetType �� Path �� RelativeSource  CustomSliderThumbTemplate ��Name  CustomSliderTemplate �� Value �� Binding �� ElementName 	 CustomSliderStyle 
 NetworkProgressBarStyle  ModernToggleButtonStyle �� Triggers  �� Padding! ��Name  ModernButtonStyle  ControlsGridStyle�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      %p  &	        &	      &	 *     &	 ?     &	 �     7   7   7   7   &	 �    7   7  7  &	 �    7  &	 �    7   7  7  7   &	     7   7  7  7  &		 	    7  &	
 p    7  &	     7  &	 �    7  &	 B    �� $  	#FF1E91EA���� $  	#55ffffff���� $  	#FFEFEFEF���� $	 0,0&�$	 0,1&�.��� � $	 0.0Q�$ 	#00000000��� $	 0.1Q�$ 	#44000000��� $	 0.2Q�$ 	#66000000��� $	 0.4Q�$ 	#D6000000��� $	 0.4Q�$ 	#D6000000���� " n�.R�� $��Center=� . $��Center=�.+�F� $
 grip��)����        �@B @B � ��
 ��
 ��
	
������ $
 Fill=�8��  $	��2.5�� . $��Center=�$��-1,-1q�	 P� 
 �@n�
IsDragging . . ��� $
 grip��8   � ���� $
 grip��8   � ��P� 
 � {� .  . ��� $
 grip�� � y�$ Hidden=�P� 
 �@J�IsKeyboardFocused . . ��� $
 grip��8   � ���� " ��.R��� $ border��
������  . ._��  �� #��z��.+�]� ��. .+��� $ PART_SelectionRange��}�. $��3��$y�Visible=�._��� $ 1��$ 1��$��
10,0,-10,0q�8��  �� }�. $��3��$y�Visible=�._��� $ 1��$ 1��$��	20,0,10,0q�8�� ]� $ 
PART_Track�� n� $ Thumb��|�.  #��z��#��z�� . 8�� $��Center=��� " ��.���  �@��IsPressAndHoldEnabled .  ��  � ��
 ���� ��  � ��
 ���� ��  � ��$ 20����  � ��$ 20����  � ��$ 20����  �@�Minimum$ 0Q���  �@��AutoToolTipPlacement$
 None=���  �@��IsMoveToPointEnabled . ��  � ��$ Center=���  �@��SelectionStart$ 0Q���  �@��SelectionEnd �� $ Value� �� *Self+��  � ��8   �� " n�.R�� $��Center=� . $��Center=�.+��� $
 grip��8��  $��8��$��0�� . $
 Fill=�$ 3��$ 3������ $ Height� �� *TemplatedParent+	 P� 
 �@n�
IsDragging . . ��� $
 grip��8   � ��P� 
 � {� .  . ��� $
 grip��8   � ��P� 
 �@J�IsKeyboardFocused . . ��� $
 grip��8   � ���� " ��.R��� $ border��
������  . ._��  �� #��z��.+�]� ��. .+��� $ PART_SelectionRange��}�. $��3��$y�Visible=�._��� $ 1��$ 1��$��4,0,-4,0q�8��  �� }�. $��3��$y�Visible=�._��� $ PART_NegativeSelection��$ 1��$ 1��$��8,0,4,0q�8�� ]� $ 
PART_Track�� n� $ Thumb��|�.  $��8��#��z�� . 8�� $��Center=�	 �� $
 True�� �� $ Thumb��$ 
IsDragging�.a��� $ PART_NegativeSelection��8   � ���� " ��.���  �@��IsPressAndHoldEnabled .  ��  � ��
 ���� ��  � ��
 ���� ��  � ��$ 26����  � ��$ 26����  � ��$ 26����  �@�Minimum$ 0Q���  �@��IsSnapToTickEnabled . ��  �@�SmallChange$
 0.01Q���  �@�LargeChange$
 0.01Q���  �@��TickFrequency$
 0.01Q���  �@��AutoToolTipPlacement$
 None=���  �@��IsMoveToPointEnabled . ��  � ��$ Center=���  �@��SelectionStart �� $ Minimum� �� *Self+��  �@��SelectionEnd �� $ Value� �� *Self+��  � ��8   �� " �.���  � ��$ 0q���  � ��$ 3����  � ��$
 10,0q���  �@�Minimum$ 0Q���  �@�Maximum$ 1Q���  � ��8   �� " d� P� 
 �@J�IsMouseOver . . ���  � �� �	#22FFFFFFP� 
 � �� . . ���  � �� �	#44FFFFFFP� 
 �@d�	IsChecked$
 TrueY�. ���  � �� �	#44FFFFFF.���  � ��$ 0q���  � ��$ 45����  � ��$ 45����  � ��$ 12q���  � ��
 ���� ��  � ��8   ��  � �� �� " d�.R��� #��z��$��0q�#  z��._��� $! contentPresenter��$��Center=�$��Center=��� " �� P� 
 �@J�IsMouseOver . . ���  � �� �	#22FFFFFFP� 
 � �� . . ���  � �� �	#44FFFFFF.���  � ��$ 0q���  � ��$ 45����  � ��$ 45����  � ��$ 12q���  � ��
 ���� ��  � ��8   ��  � �� �� " ��.R��� #��z��$��0q�#  z��._��� $! contentPresenter��$��Center=�$��Center=��� " �.���  � ��$ Bottom=���  � ��$	 250����  � ��# �  